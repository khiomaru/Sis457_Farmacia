using CadFarmacia2024;
using ClnFarmacia2024;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CpFarmacia2024
{
    public partial class FrmMedicamento : Form
    {
        private bool esNuevo = false;
        private readonly Size CompactSize = new Size(900, 390);
        private readonly Size ExpandedSize = new Size(900, 654);
        private BindingSource bindingSource; // Para simplificar binding de datos

        public FrmMedicamento()
        {
            InitializeComponent();
            InitializeBindingSource(); // Nueva: Inicializar binding
            TryLoadCategorias();
            WireEvents();
            Listar();
            ToggleFormMode(false);
        }

        private void InitializeBindingSource()
        {
            bindingSource = new BindingSource();
            dgvListaMedicamentos.DataSource = bindingSource;
        }

        private void WireEvents()
        {
            txtParametro.KeyPress += TxtParametro_KeyPress;
            dgvListaMedicamentos.SelectionChanged += DgvListaMedicamentos_SelectionChanged;
            dgvListaMedicamentos.CellDoubleClick += DgvListaMedicamentos_CellDoubleClick;
            // Nueva: Evitar recargas innecesarias en TextChanged
            txtParametro.TextChanged += (s, e) => { /* Opcional: Implementar búsqueda en tiempo real si es necesario */ };
        }

        private void TryLoadCategorias()
        {
            try
            {
                var categorias = CategoriaCln.listar() ?? new List<Categoria>();
                cbxCategoria.DataSource = categorias;
                cbxCategoria.DisplayMember = "descripcion";
                cbxCategoria.ValueMember = "id";
                cbxCategoria.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                // Mejorado: Loggear error en lugar de ignorarlo
                Debug.WriteLine($"Error al cargar categorías: {ex.Message}");
                MessageBox.Show("Error al cargar categorías. Verifique la conexión a la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbxCategoria.DataSource = null;
            }

            cbxEstado.SelectedIndex = 0; // Activo por defecto
        }

        // Nuevo: ErrorProvider para fecha de caducidad
        private ErrorProvider erpFechaCaducidad = new ErrorProvider();

        private void Listar()
        {
            try
            {
                var parametro = txtParametro.Text?.Trim() ?? string.Empty;
                var listaPa = MedicamentoCln.listaaPa(parametro) ?? new List<paMedicamentoListar_Result>();
                bindingSource.DataSource = listaPa; // Usar bindingSource para mejor control

                if (listaPa.Any())
                {
                    foreach (var col in new[] { "estado", "fechaRegistro", "usuarioRegistro" })
                        if (dgvListaMedicamentos.Columns.Contains(col))
                            dgvListaMedicamentos.Columns[col].Visible = false;

                    var headers = new Dictionary<string, string>
                    {
                        { "codigo", "Código" },
                        { "descripcion", "Descripción" },
                        { "Categoria", "Categoría" },
                        { "precioVenta", "Precio de Venta" }
                    };

                    foreach (var kv in headers)
                        if (dgvListaMedicamentos.Columns.Contains(kv.Key))
                            dgvListaMedicamentos.Columns[kv.Key].HeaderText = kv.Value;

                    dgvListaMedicamentos.Rows[0].Selected = true;
                }

                EnableActionButtons();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al listar medicamentos: {ex.Message}");
                MessageBox.Show("Error al listar medicamentos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            AdjustSizeForMode();
        }

        private void TxtParametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Listar();
                e.Handled = true;
            }
        }

        private void DgvListaMedicamentos_SelectionChanged(object sender, EventArgs e) => EnableActionButtons();
        private void DgvListaMedicamentos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) btnEditar_Click_1(sender, EventArgs.Empty);
        }

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {
            esNuevo = true;
            ClearForm();
            ToggleFormMode(true);
            txtCodigo.Focus();
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            if (!GetSelectedId(out int id)) return;

            esNuevo = false;
            ToggleFormMode(true);

            try
            {
                var medicamento = MedicamentoCln.obtenerUno(id);
                if (medicamento == null)
                {
                    MessageBox.Show("Registro no encontrado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // UI <- entidad (mejorado: usar un método helper)
                PopulateFormFromEntity(medicamento);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar registro: {ex.Message}");
                MessageBox.Show("Error al cargar registro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            AdjustSizeForMode();
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (!GetSelectedId(out int id)) return;

            string codigo = dgvListaMedicamentos.CurrentRow?.Cells["codigo"].Value?.ToString() ?? string.Empty;
            var dialog = MessageBox.Show($"¿Está seguro que desea eliminar el medicamento con el código {codigo}?",
                "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialog != DialogResult.Yes) return;

            try
            {
                MedicamentoCln.eliminar(id, Util.usuario.usuario1 ?? "Usuario Desconocido");
                Listar();
                MessageBox.Show("Medicamento eliminado correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al eliminar: {ex.Message}");
                MessageBox.Show("Error al eliminar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e) => Close();
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ToggleFormMode(false);
            ClearForm();
        }

        private void btnBuscar_Click_1(object sender, EventArgs e) => Listar();

        private void ClearForm()
        {
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtMarca.Clear();
            txtPresentacion.Clear();
            nudStockActual.Value = 0;
            dtpFechaCaducidad.Checked = false;
            if (cbxCategoria.DataSource != null) cbxCategoria.SelectedIndex = -1;
            nudPrecioVenta.Value = 0;
            cbxEstado.SelectedIndex = 0;
            ClearErrors();
        }

        private void ClearErrors()
        {
            erpCodigo.Clear();
            erpDescripcion.Clear();
            erpMarca.Clear();
            erpCategoria.Clear();
            erpPrecioVenta.Clear();
            erpFechaCaducidad.Clear();
        }

        private bool ValidateForm()
        {
            ClearErrors();
            bool esValido = true;

            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                esValido = false;
                erpCodigo.SetError(txtCodigo, "El campo código es obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                esValido = false;
                erpDescripcion.SetError(txtDescripcion, "El campo descripción es obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(txtMarca.Text))
            {
                esValido = false;
                erpMarca.SetError(txtMarca, "El campo marca es obligatorio.");
            }
            if (cbxCategoria.SelectedIndex < 0)
            {
                esValido = false;
                erpCategoria.SetError(cbxCategoria, "El campo categoría es obligatorio.");
            }
            if (nudPrecioVenta.Value <= 0)
            {
                esValido = false;
                erpPrecioVenta.SetError(nudPrecioVenta, "El precio de venta debe ser mayor que cero.");
            }
            // Nueva: Validar fecha de caducidad
            if (dtpFechaCaducidad.Checked && dtpFechaCaducidad.Value < DateTime.Today)
            {
                esValido = false;
                erpFechaCaducidad.SetError(dtpFechaCaducidad, "La fecha de caducidad no puede ser anterior a hoy.");
            }

            if (esValido && esNuevo)
            {
                try
                {
                    if (MedicamentoCln.ExisteCodigo(txtCodigo.Text.Trim()))
                    {
                        esValido = false;
                        erpCodigo.SetError(txtCodigo, "El código ya existe.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error al verificar código: {ex.Message}");
                    esValido = false;
                    erpCodigo.SetError(txtCodigo, "Error al verificar código único.");
                }
            }

            return esValido;
        }

        // Refactorizado: Método helper para poblar el formulario desde la entidad
        private void PopulateFormFromEntity(Medicamento medicamento)
        {
            txtCodigo.Text = medicamento.codigo ?? string.Empty;
            txtDescripcion.Text = medicamento.descripcion ?? string.Empty;
            txtMarca.Text = medicamento.marca ?? string.Empty;
            txtPresentacion.Text = medicamento.presentacion ?? string.Empty;
            nudStockActual.Value = medicamento.stockActual;
            dtpFechaCaducidad.Value = medicamento.fechaCaducidad ?? DateTime.Now;
            dtpFechaCaducidad.Checked = medicamento.fechaCaducidad.HasValue;
            nudPrecioVenta.Value = medicamento.precioVenta;
            cbxEstado.SelectedIndex = medicamento.estado == 1 ? 0 : 1;
            try
            {
                cbxCategoria.SelectedValue = medicamento.idCategoria;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al seleccionar categoría: {ex.Message}");
                cbxCategoria.SelectedIndex = -1; // Seleccionar ninguno si falla
            }
        }

        // Refactorizado: Método helper para crear entidad desde UI
        private Medicamento CreateMedicamentoFromUi()
        {
            return new Medicamento
            {
                codigo = txtCodigo.Text.Trim(),
                nombre = txtDescripcion.Text.Trim(),
                descripcion = txtDescripcion.Text.Trim(),
                marca = txtMarca.Text.Trim(),
                presentacion = txtPresentacion.Text.Trim(),
                stockActual = (int)nudStockActual.Value,
                fechaCaducidad = dtpFechaCaducidad.Checked ? dtpFechaCaducidad.Value : (DateTime?)null,
                tipoUnidad = "Unidad", // Corregido: Establecer un valor por defecto razonable
                precioVenta = nudPrecioVenta.Value,
                usuarioRegistro = Util.usuario.usuario1 ?? "Usuario Desconocido",
                fechaRegistro = DateTime.Now,
                estado = cbxEstado.SelectedIndex == 0 ? (short)1 : (short)0,
                idCategoria = Convert.ToInt32(cbxCategoria.SelectedValue)
            };
        }

        // Refactorizado: Método helper para actualizar entidad
        private void UpdateEntityFromUi(Medicamento existente)
        {
            existente.codigo = txtCodigo.Text.Trim();
            existente.nombre = txtDescripcion.Text.Trim();
            existente.descripcion = txtDescripcion.Text.Trim();
            existente.marca = txtMarca.Text.Trim();
            existente.presentacion = txtPresentacion.Text.Trim();
            existente.stockActual = (int)nudStockActual.Value;
            existente.fechaCaducidad = dtpFechaCaducidad.Checked ? dtpFechaCaducidad.Value : (DateTime?)null;
            existente.tipoUnidad = "Unidad"; // Corregido: Establecer un valor por defecto razonable
            existente.precioVenta = nudPrecioVenta.Value;
            existente.estado = cbxEstado.SelectedIndex == 0 ? (short)1 : (short)0;
            existente.idCategoria = Convert.ToInt32(cbxCategoria.SelectedValue);
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            // Nueva: Confirmación antes de guardar
            if (MessageBox.Show("¿Guardar los cambios?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                if (esNuevo)
                {
                    MedicamentoCln.insertar(CreateMedicamentoFromUi());
                }
                else
                {
                    if (!GetSelectedId(out int id)) return;
                    var existente = MedicamentoCln.obtenerUno(id);
                    if (existente == null)
                    {
                        MessageBox.Show("Registro no encontrado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    UpdateEntityFromUi(existente);
                    MedicamentoCln.actualizar(existente);
                }

                Listar();
                btnCancelar.PerformClick();
                MessageBox.Show("Medicamento guardado correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al guardar: {ex.Message}");
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool GetSelectedId(out int id)
        {
            id = 0;
            try
            {
                if (dgvListaMedicamentos.CurrentRow == null) return false;
                var cell = dgvListaMedicamentos.CurrentRow.Cells["id"];
                if (cell?.Value == null) return false;
                id = Convert.ToInt32(cell.Value);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al obtener ID seleccionado: {ex.Message}");
                return false;
            }
        }

        private void EnableActionButtons()
        {
            bool hasSelection = bindingSource.Current != null; // Mejorado: Usar bindingSource
            btnEditar.Enabled = hasSelection;
            btnEliminar.Enabled = hasSelection;
        }

        private void ToggleFormMode(bool editMode)
        {
            this.Size = editMode ? ExpandedSize : CompactSize;
            gbxDatos.Visible = editMode;
            pnlAcciones.Enabled = !editMode;
            txtParametro.Enabled = !editMode;
            btnBuscar.Enabled = !editMode;
            EnableActionButtons();
            AdjustSizeForMode();
        }

        private void AdjustSizeForMode()
        {
            this.ClientSize = gbxDatos.Visible ? ExpandedSize : CompactSize;
        }

        private void FrmMedicamento_Load(object sender, EventArgs e) { }

        // Métodos vacíos referenciados por el diseñador
        private void gbxDatos_Enter(object sender, EventArgs e) { }
        private void txtParametro_TextChanged(object sender, EventArgs e) { }
        private void dgvListaMedicamentos_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void pnlAcciones_Paint(object sender, PaintEventArgs e) { }
        private void lblPrincipal_Click(object sender, EventArgs e) { }
        private void lblBusqueda_Click(object sender, EventArgs e) { }
        private void txtCodigo_TextChanged(object sender, EventArgs e) { }
        private void txtDescripcion_TextChanged(object sender, EventArgs e) { }
        private void txtMarca_TextChanged(object sender, EventArgs e) { }
        private void nudPrecioVenta_ValueChanged(object sender, EventArgs e) { }
    }
}
