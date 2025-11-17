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
        // ====================================================================================
        // VARIABLES Y CONSTANTES
        // ====================================================================================
        private bool esNuevo = false;
        private readonly Size CompactSize = new Size(900, 390);
        private readonly Size ExpandedSize = new Size(900, 654);
        private BindingSource bindingSource; // Para simplificar binding de datos

        // Nuevo: ErrorProvider para fecha de caducidad (declarado aquí para usarlo globalmente)
        private ErrorProvider erpFechaCaducidad = new ErrorProvider();


        // Nota: Asumiendo que las propiedades erpCodigo, erpDescripcion, erpMarca, erpCategoria, 
        // erpPrecioVenta y los controles UI (txtCodigo, txtNombre, cboMarca, etc.) están 
        // definidos en el archivo FrmMedicamento.Designer.cs.

        // ====================================================================================
        // CONSTRUCTOR Y CONFIGURACIÓN INICIAL
        // ====================================================================================
        public FrmMedicamento()
        {
            InitializeComponent();
            InitializeBindingSource(); // Inicializar binding
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
            // Eventos para el formulario
            txtParametro.KeyPress += TxtParametro_KeyPress;
            dgvListaMedicamentos.SelectionChanged += DgvListaMedicamentos_SelectionChanged;
            dgvListaMedicamentos.CellDoubleClick += DgvListaMedicamentos_CellDoubleClick;
            // Evitar recargas innecesarias en TextChanged
            txtParametro.TextChanged += (s, e) => { /* Implementar búsqueda en tiempo real si es necesario */ };
        }

        private void TryLoadCategorias()
        {
            try
            {
                // Asumiendo que CategoriaCln.listar() devuelve List<Categoria>
                var categorias = CategoriaCln.listar() ?? new List<Categoria>();
                cbxCategoria.DataSource = categorias;
                cbxCategoria.DisplayMember = "descripcion";
                cbxCategoria.ValueMember = "id";
                cbxCategoria.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar categorías: {ex.Message}");
                MessageBox.Show("Error al cargar categorías. Verifique la conexión a la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbxCategoria.DataSource = null;
            }

            // Asumiendo que cbxEstado tiene "Activo" y "Inactivo"
            cbxEstado.SelectedIndex = 0; // Activo por defecto
        }

        // ====================================================================================
        // OPERACIONES DE DATOS (LISTAR, BUSCAR)
        // ====================================================================================

        private void Listar()
        {
            try
            {
                var parametro = txtParametro.Text?.Trim() ?? string.Empty;
                // Asumiendo que MedicamentoCln.listaaPa() devuelve List<paMedicamentoListar_Result>
                var listaPa = MedicamentoCln.listaaPa(parametro) ?? new List<paMedicamentoListar_Result>();
                bindingSource.DataSource = listaPa; // Usar bindingSource

                if (listaPa.Any())
                {
                    // Ocultar columnas internas/de gestión
                    foreach (var col in new[] { "estado", "fechaRegistro", "usuarioRegistro", "id", "idCategoria" })
                        if (dgvListaMedicamentos.Columns.Contains(col))
                            dgvListaMedicamentos.Columns[col].Visible = false;

                    // Renombrar columnas para la UI
                    var headers = new Dictionary<string, string>
                    {
                        { "codigo", "Código" },
                        { "descripcion", "Descripción" },
                        { "Categoria", "Categoría" }, // Asumiendo que el SP devuelve 'Categoria' con la descripción
                        { "precioVenta", "Precio de Venta" }
                    };

                    foreach (var kv in headers)
                        if (dgvListaMedicamentos.Columns.Contains(kv.Key))
                            dgvListaMedicamentos.Columns[kv.Key].HeaderText = kv.Value;

                    // Seleccionar el primer registro si existe
                    if (dgvListaMedicamentos.Rows.Count > 0)
                    {
                        dgvListaMedicamentos.Rows[0].Selected = true;
                    }
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

        // ====================================================================================
        // MANEJO DE LA INTERFAZ (UI)
        // ====================================================================================

        private void ToggleFormMode(bool editMode)
        {
            // Ajustar tamaño de la ventana
            this.Size = editMode ? ExpandedSize : CompactSize;
            this.MinimizeBox = !editMode;
            this.MaximizeBox = !editMode;

            // Mostrar/Ocultar panel de datos
            gbxDatos.Visible = editMode;

            // Habilitar/Deshabilitar panel de acciones y búsqueda
            pnlAcciones.Enabled = !editMode;
            txtParametro.Enabled = !editMode;
            btnBuscar.Enabled = !editMode;

            // Re-evaluar botones de acción (Editar, Eliminar)
            EnableActionButtons();

            AdjustSizeForMode(); // Asegura que el ClientSize coincida
        }

        private void AdjustSizeForMode()
        {
            // Nota: Se usa ClientSize para asegurar que el área interior sea correcta
            this.ClientSize = gbxDatos.Visible ? ExpandedSize : CompactSize;
        }

        private void EnableActionButtons()
        {
            bool hasSelection = bindingSource.Current != null; // Usar bindingSource
            btnEditar.Enabled = hasSelection;
            btnEliminar.Enabled = hasSelection;
        }

        private void ClearForm()
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            cboMarca.Text = string.Empty;
            cboPresentacion.Text = string.Empty;
            nudStockActual.Value = 0;
            dtpFechaCaducidad.Checked = false;
            // Asegurar que solo se intenta manipular si hay datos
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

        // ====================================================================================
        // VALIDACIÓN Y CONVERSIÓN DE DATOS
        // ====================================================================================

        private bool ValidateForm()
        {
            ClearErrors();
            bool esValido = true;

            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                esValido = false;
                erpCodigo.SetError(txtCodigo, "El campo código es obligatorio.");
            }
            // Aunque la entidad tiene 'nombre', la UI usa 'txtNombre' para 'descripcion'
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                esValido = false;
                erpDescripcion.SetError(txtNombre, "El campo descripción es obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(cboMarca.Text))
            {
                esValido = false;
                erpMarca.SetError(cboMarca, "El campo marca es obligatorio.");
            }
            if (cbxCategoria.SelectedValue == null || Convert.ToInt32(cbxCategoria.SelectedValue) <= 0)
            {
                esValido = false;
                erpCategoria.SetError(cbxCategoria, "El campo categoría es obligatorio.");
            }
            if (nudPrecioVenta.Value <= 0)
            {
                esValido = false;
                erpPrecioVenta.SetError(nudPrecioVenta, "El precio de venta debe ser mayor que cero.");
            }

            // Validar fecha de caducidad: no puede ser anterior a hoy si está marcada
            if (dtpFechaCaducidad.Checked && dtpFechaCaducidad.Value.Date < DateTime.Today)
            {
                esValido = false;
                erpFechaCaducidad.SetError(dtpFechaCaducidad, "La fecha de caducidad no puede ser anterior a hoy.");
            }

            // Validar unicidad del código solo en modo "Nuevo"
            if (esValido && esNuevo)
            {
                try
                {
                    // Asumiendo que MedicamentoCln.ExisteCodigo() está disponible
                    if (MedicamentoCln.ExisteCodigo(txtCodigo.Text.Trim()))
                    {
                        esValido = false;
                        erpCodigo.SetError(txtCodigo, "El código ya existe.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error al verificar código: {ex.Message}");
                    // Si falla la BD, se asume error de validación
                    esValido = false;
                    erpCodigo.SetError(txtCodigo, "Error al verificar código único.");
                }
            }

            return esValido;
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

        // ====================================================================================
        // POBLAR ENTIDAD <-> UI
        // ====================================================================================

        // Poblar el formulario desde la entidad
        private void PopulateFormFromEntity(Medicamento medicamento)
        {
            txtCodigo.Text = medicamento.codigo ?? string.Empty;
            txtNombre.Text = medicamento.descripcion ?? string.Empty; // Usar descripcion para txtNombre
            cboMarca.Text = medicamento.marca ?? string.Empty;
            cboPresentacion.Text = medicamento.presentacion ?? string.Empty;
            nudStockActual.Value = medicamento.stockActual;

            // Manejar la fecha de caducidad opcional
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

        // Crear entidad desde UI (para inserción)
        private Medicamento CreateMedicamentoFromUi()
        {
            // Nota: nombre y descripcion se mapean al mismo control (txtNombre) en la UI
            return new Medicamento
            {
                codigo = txtCodigo.Text.Trim(),
                nombre = txtNombre.Text.Trim(),
                descripcion = txtNombre.Text.Trim(),
                marca = cboMarca.Text.Trim(),
                presentacion = cboPresentacion.Text.Trim(),
                stockActual = (int)nudStockActual.Value,
                fechaCaducidad = dtpFechaCaducidad.Checked ? dtpFechaCaducidad.Value.Date : (DateTime?)null, // Guardar solo la fecha
                tipoUnidad = "Unidad", // Valor por defecto
                precioVenta = nudPrecioVenta.Value,
                // Util.usuario.usuario1 debe contener el nombre de usuario
                usuarioRegistro = Util.usuario.usuario1 ?? "Usuario Desconocido",
                fechaRegistro = DateTime.Now,
                estado = cbxEstado.SelectedIndex == 0 ? (short)1 : (short)0,
                idCategoria = Convert.ToInt32(cbxCategoria.SelectedValue)
            };
        }

        // Actualizar entidad existente desde UI
        private void UpdateEntityFromUi(Medicamento existente)
        {
            existente.codigo = txtCodigo.Text.Trim();
            existente.nombre = txtNombre.Text.Trim();
            existente.descripcion = txtNombre.Text.Trim();
            existente.marca = cboMarca.Text.Trim();
            existente.presentacion = cboPresentacion.Text.Trim();
            existente.stockActual = (int)nudStockActual.Value;
            existente.fechaCaducidad = dtpFechaCaducidad.Checked ? dtpFechaCaducidad.Value.Date : (DateTime?)null; // Guardar solo la fecha
            existente.tipoUnidad = "Unidad";
            existente.precioVenta = nudPrecioVenta.Value;
            existente.estado = cbxEstado.SelectedIndex == 0 ? (short)1 : (short)0;
            existente.idCategoria = Convert.ToInt32(cbxCategoria.SelectedValue);
            // No actualizar usuarioRegistro y fechaRegistro
        }

        // ====================================================================================
        // EVENT HANDLERS
        // ====================================================================================

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

        private void btnBuscar_Click_1(object sender, EventArgs e) => Listar();

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
                // Asumiendo que MedicamentoCln.obtenerUno() está disponible
                var medicamento = MedicamentoCln.obtenerUno(id);
                if (medicamento == null)
                {
                    MessageBox.Show("Registro no encontrado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ToggleFormMode(false); // Volver al modo lista
                    return;
                }

                // UI <- entidad
                PopulateFormFromEntity(medicamento);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar registro: {ex.Message}");
                MessageBox.Show("Error al cargar registro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ToggleFormMode(false); // Volver al modo lista
            }
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            // Confirmación antes de guardar
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
                        MessageBox.Show("Registro no encontrado. No se puede actualizar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    UpdateEntityFromUi(existente);
                    MedicamentoCln.actualizar(existente);
                }

                Listar();
                btnCancelar.PerformClick(); // Cancelar oculta el gbxDatos
                MessageBox.Show("Medicamento guardado correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al guardar: {ex.Message}");
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (!GetSelectedId(out int id)) return;

            string codigo = dgvListaMedicamentos.CurrentRow?.Cells["codigo"].Value?.ToString() ?? string.Empty;
            var dialog = MessageBox.Show($"¿Está seguro que desea eliminar (cambiar a estado inactivo) el medicamento con el código {codigo}?",
                "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialog != DialogResult.Yes) return;

            try
            {
                // Asumiendo que MedicamentoCln.eliminar() está disponible y pone estado=0
                MedicamentoCln.eliminar(id, Util.usuario.usuario1 ?? "Usuario Desconocido");
                Listar();
                MessageBox.Show("Medicamento eliminado (inactivado) correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al eliminar: {ex.Message}");
                MessageBox.Show("Error al eliminar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ToggleFormMode(false);
            ClearForm();
        }

        private void btnCerrar_Click(object sender, EventArgs e) => Close();

        // ====================================================================================
        // MÉTODOS DEL DISEÑADOR (Mantener vacíos si no se usan)
        // ====================================================================================

        private void FrmMedicamento_Load(object sender, EventArgs e) { }
        private void gbxDatos_Enter(object sender, EventArgs e) { }
        private void txtParametro_TextChanged(object sender, EventArgs e) { }
        private void dgvListaMedicamentos_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void pnlAcciones_Paint(object sender, PaintEventArgs e) { }
        private void lblPrincipal_Click(object sender, EventArgs e) { }
        private void lblBusqueda_Click(object sender, EventArgs e) { }
        private void txtCodigo_TextChanged(object sender, EventArgs e) { }
        private void txtDescripcion_TextChanged(object sender, EventArgs e) { }
        private void cboMarca_TextChanged(object sender, EventArgs e) { }
        private void nudPrecioVenta_ValueChanged(object sender, EventArgs e) { }
    }
}