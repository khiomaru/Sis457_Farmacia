using CadFarmacia;
using ClnFarmacia;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Windows.Forms;

namespace CpFarmacia
{
    public partial class FrmMedicamentos : Form
    {
        private int idMedicamentoSeleccionado = 0;
        private bool esNuevo = false;

        public FrmMedicamentos()
        {
            InitializeComponent();
        }

        private void FrmMedicamentos_Load(object sender, EventArgs e)
        {
            CargarCombos();
            Listar();
            EstadoInicial();
        }

        private void CargarCombos()
        {
            // Cargar Categorías
            cboCategoria.DataSource = CategoriaCln.Listar();
            cboCategoria.DisplayMember = "nombre";
            cboCategoria.ValueMember = "id";
            cboCategoria.SelectedIndex = -1;

            // Cargar Laboratorios
            cboLaboratorio.DataSource = LaboratorioCln.ListarActivos();
            cboLaboratorio.DisplayMember = "nombre";
            cboLaboratorio.ValueMember = "id";
            cboLaboratorio.SelectedIndex = -1;
        }

        private void Listar()
        {
            dgvMedicamentos.DataSource = MedicamentoCln.Listar(txtBuscar.Text.Trim());
            FormatearGrilla();
        }

        private void FormatearGrilla()
        {
            if (dgvMedicamentos.Columns.Count > 0)
            {
                dgvMedicamentos.Columns["id"].Visible = false;
                dgvMedicamentos.Columns["codigo"].HeaderText = "Código";
                dgvMedicamentos.Columns["nombre"].HeaderText = "Nombre";
                dgvMedicamentos.Columns["descripcion"].HeaderText = "Descripción";
                dgvMedicamentos.Columns["composicion"].Visible = false;
                dgvMedicamentos.Columns["Categoria"].HeaderText = "Categoría";
                dgvMedicamentos.Columns["Laboratorio"].HeaderText = "Laboratorio";
                dgvMedicamentos.Columns["fechaVencimiento"].HeaderText = "Fecha Venc.";
                dgvMedicamentos.Columns["stock"].HeaderText = "Stock";
                dgvMedicamentos.Columns["precioVenta"].HeaderText = "Precio";
                dgvMedicamentos.Columns["requiereReceta"].HeaderText = "Receta";
                dgvMedicamentos.Columns["usuarioRegistro"].Visible = false;
                dgvMedicamentos.Columns["fechaRegistro"].Visible = false;
                dgvMedicamentos.Columns["estado"].HeaderText = "Estado";
                dgvMedicamentos.Columns["idCategoria"].Visible = false;
                dgvMedicamentos.Columns["idLaboratorio"].Visible = false;
            }
        }

        private void EstadoInicial()
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtComposicion.Clear();
            txtPrecioVenta.Clear();
            txtStock.Clear();
            cboCategoria.SelectedIndex = -1;
            cboLaboratorio.SelectedIndex = -1;
            dtpFechaVencimiento.Value = DateTime.Now.AddYears(1);
            chkRequiereReceta.Checked = false;

            txtCodigo.Enabled = false;
            txtNombre.Enabled = false;
            txtDescripcion.Enabled = false;
            txtComposicion.Enabled = false;
            txtPrecioVenta.Enabled = false;
            txtStock.Enabled = false;
            cboCategoria.Enabled = false;
            cboLaboratorio.Enabled = false;
            dtpFechaVencimiento.Enabled = false;
            chkRequiereReceta.Enabled = false;

            btnGuardar.Enabled = false;
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
            btnNuevo.Enabled = true;

            idMedicamentoSeleccionado = 0;
            esNuevo = false;
        }

        private void HabilitarCampos()
        {
            txtCodigo.Enabled = true;
            txtNombre.Enabled = true;
            txtDescripcion.Enabled = true;
            txtComposicion.Enabled = true;
            txtPrecioVenta.Enabled = true;
            txtStock.Enabled = true;
            cboCategoria.Enabled = true;
            cboLaboratorio.Enabled = true;
            dtpFechaVencimiento.Enabled = true;
            chkRequiereReceta.Enabled = true;

            btnGuardar.Enabled = true;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;

            txtCodigo.Focus();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Listar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            idMedicamentoSeleccionado = 0;

            txtCodigo.Clear();
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtComposicion.Clear();
            txtPrecioVenta.Clear();
            txtStock.Clear();
            cboCategoria.SelectedIndex = -1;
            cboLaboratorio.SelectedIndex = -1;
            dtpFechaVencimiento.Value = DateTime.Now.AddYears(1);
            chkRequiereReceta.Checked = false;

            HabilitarCampos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("Ingrese el código", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodigo.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese el nombre", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            if (cboCategoria.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una categoría", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCategoria.Focus();
                return;
            }

            if (cboLaboratorio.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un laboratorio", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLaboratorio.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPrecioVenta.Text))
            {
                MessageBox.Show("Ingrese el precio de venta", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecioVenta.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtStock.Text))
            {
                MessageBox.Show("Ingrese el stock", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStock.Focus();
                return;
            }

            // Validar fecha de vencimiento (no puede ser en el pasado)
            if (dtpFechaVencimiento.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("La fecha de vencimiento no puede ser en el pasado", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpFechaVencimiento.Focus();
                return;
            }

            try
            {
                if (esNuevo)
                {
                    Medicamento medicamento = new Medicamento
                    {
                        codigo = txtCodigo.Text.Trim(),
                        nombre = txtNombre.Text.Trim(),
                        descripcion = txtDescripcion.Text.Trim(),
                        composicion = txtComposicion.Text.Trim(),
                        idCategoria = Convert.ToInt32(cboCategoria.SelectedValue),
                        idLaboratorio = Convert.ToInt32(cboLaboratorio.SelectedValue),
                        precioVenta = Convert.ToDecimal(txtPrecioVenta.Text),
                        stock = Convert.ToInt32(txtStock.Text),
                        fechaVencimiento = dtpFechaVencimiento.Value,
                        requiereReceta = chkRequiereReceta.Checked,
                        estado = 1,
                        usuarioRegistro = Util.usuario?.usuario1 ?? "Sistema",
                        fechaRegistro = DateTime.Now
                    };

                    MedicamentoCln.Insertar(medicamento);
                    MessageBox.Show("Medicamento registrado correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Medicamento medicamento = MedicamentoCln.ObtenerPorId(idMedicamentoSeleccionado);
                    medicamento.codigo = txtCodigo.Text.Trim();
                    medicamento.nombre = txtNombre.Text.Trim();
                    medicamento.descripcion = txtDescripcion.Text.Trim();
                    medicamento.composicion = txtComposicion.Text.Trim();
                    medicamento.idCategoria = Convert.ToInt32(cboCategoria.SelectedValue);
                    medicamento.idLaboratorio = Convert.ToInt32(cboLaboratorio.SelectedValue);
                    medicamento.precioVenta = Convert.ToDecimal(txtPrecioVenta.Text);
                    medicamento.stock = Convert.ToInt32(txtStock.Text);
                    medicamento.fechaVencimiento = dtpFechaVencimiento.Value;
                    medicamento.requiereReceta = chkRequiereReceta.Checked;
                    // No se actualiza usuarioRegistro ni fechaRegistro para una edición

                    MedicamentoCln.Actualizar(medicamento);
                    MessageBox.Show("Medicamento actualizado correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Listar();
                EstadoInicial();
            }
            catch (DbEntityValidationException ex)
            {
                string errorMessage = "Errores de validación:\n";
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += $"- Propiedad: {validationError.PropertyName}, Error: {validationError.ErrorMessage}\n";
                    }
                }
                MessageBox.Show(errorMessage, "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DbUpdateException ex)
            {
                string innerMessage = ex.InnerException?.Message ?? "Sin detalles adicionales";
                if (ex.InnerException?.InnerException != null)
                {
                    innerMessage += "\n\nDetalles internos: " + ex.InnerException.InnerException.Message;
                }
                MessageBox.Show("Error al actualizar la base de datos: " + innerMessage, "Error de Base de Datos",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idMedicamentoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un medicamento de la lista", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            esNuevo = false;
            HabilitarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idMedicamentoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un medicamento de la lista", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Está seguro de eliminar este medicamento?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    MedicamentoCln.Eliminar(idMedicamentoSeleccionado);
                    MessageBox.Show("Medicamento eliminado correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Listar();
                    EstadoInicial();
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show("Error al eliminar el medicamento. Es posible que existan registros relacionados que impidan la eliminación: " + ex.InnerException?.Message, "Error de Base de Datos",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvMedicamentos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvMedicamentos.Rows[e.RowIndex];

                idMedicamentoSeleccionado = Convert.ToInt32(fila.Cells["id"].Value);
                txtCodigo.Text = fila.Cells["codigo"].Value.ToString();
                txtNombre.Text = fila.Cells["nombre"].Value.ToString();
                txtDescripcion.Text = fila.Cells["descripcion"].Value?.ToString() ?? "";
                txtComposicion.Text = fila.Cells["composicion"].Value?.ToString() ?? "";
                txtPrecioVenta.Text = fila.Cells["precioVenta"].Value.ToString();
                txtStock.Text = fila.Cells["stock"].Value.ToString();

                // Seleccionar en ComboBox
                cboCategoria.SelectedValue = Convert.ToInt32(fila.Cells["idCategoria"].Value);
                cboLaboratorio.SelectedValue = Convert.ToInt32(fila.Cells["idLaboratorio"].Value);

                // Fecha y checkbox
                dtpFechaVencimiento.Value = Convert.ToDateTime(fila.Cells["fechaVencimiento"].Value);
                chkRequiereReceta.Checked = Convert.ToBoolean(fila.Cells["requiereReceta"].Value);
            }
        }
    }
}