using CadFarmacia;
using ClnFarmacia;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows.Forms;

namespace CpFarmacia
{
    public partial class FrmClientes : Form
    {
        private int idClienteSeleccionado = 0;
        private bool esNuevo = false;

        public FrmClientes()
        {
            InitializeComponent();
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            Listar();
            EstadoInicial();
        }

        private void Listar()
        {
            dgvClientes.DataSource = ClienteCln.Listar(txtBuscar.Text.Trim());
            FormatearGrilla();
        }

        private void FormatearGrilla()
        {
            if (dgvClientes.Columns.Count > 0)
            {
                dgvClientes.Columns["id"].Visible = false;
                dgvClientes.Columns["cedulaIdentidad"].HeaderText = "CI";
                dgvClientes.Columns["nombres"].HeaderText = "Nombres";
                dgvClientes.Columns["apellidos"].HeaderText = "Apellidos";
                dgvClientes.Columns["telefono"].HeaderText = "Teléfono";
                dgvClientes.Columns["direccion"].HeaderText = "Dirección";
                dgvClientes.Columns["usuarioRegistro"].Visible = false;
                dgvClientes.Columns["fechaRegistro"].Visible = false;
                dgvClientes.Columns["estado"].HeaderText = "Estado";
            }
        }

        private void EstadoInicial()
        {
            txtCI.Clear();
            txtNombres.Clear();
            txtApellidos.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();

            txtCI.Enabled = false;
            txtNombres.Enabled = false;
            txtApellidos.Enabled = false;
            txtTelefono.Enabled = false;
            txtDireccion.Enabled = false;

            btnGuardar.Enabled = false;
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
            btnNuevo.Enabled = true;

            idClienteSeleccionado = 0;
            esNuevo = false;
        }

        private void HabilitarCampos()
        {
            txtCI.Enabled = true;
            txtNombres.Enabled = true;
            txtApellidos.Enabled = true;
            txtTelefono.Enabled = true;
            txtDireccion.Enabled = true;

            btnGuardar.Enabled = true;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;

            txtCI.Focus();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Listar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            idClienteSeleccionado = 0;
            txtCI.Clear();
            txtNombres.Clear();
            txtApellidos.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
            HabilitarCampos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(txtCI.Text))
            {
                MessageBox.Show("Ingrese la cédula de identidad", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCI.Focus();
                return;
            }

            // Validar formato de CI (solo números, longitud típica)
            string ci = txtCI.Text.Trim();
            if (!ci.All(char.IsDigit) || ci.Length < 7 || ci.Length > 10)
            {
                MessageBox.Show("Ingrese una cédula de identidad válida (solo números, 7-10 dígitos)", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCI.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombres.Text))
            {
                MessageBox.Show("Ingrese los nombres", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombres.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtApellidos.Text))
            {
                MessageBox.Show("Ingrese los apellidos", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellidos.Focus();
                return;
            }

            try
            {
                if (esNuevo)
                {
                    // Insertar nuevo cliente
                    Cliente cliente = new Cliente
                    {
                        cedulaIdentidad = txtCI.Text.Trim(),
                        nombres = txtNombres.Text.Trim(),
                        apellidos = txtApellidos.Text.Trim(),
                        telefono = string.IsNullOrEmpty(txtTelefono.Text) ? (long?)null : long.Parse(txtTelefono.Text),
                        direccion = txtDireccion.Text.Trim(),
                        usuarioRegistro = Util.usuario.usuario1,
                        fechaRegistro = DateTime.Now,
                        estado = 1
                    };

                    ClienteCln.Insertar(cliente);
                    MessageBox.Show("Cliente registrado correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Actualizar cliente existente
                    Cliente cliente = ClienteCln.ObtenerPorId(idClienteSeleccionado);
                    cliente.cedulaIdentidad = txtCI.Text.Trim();
                    cliente.nombres = txtNombres.Text.Trim();
                    cliente.apellidos = txtApellidos.Text.Trim();
                    cliente.telefono = string.IsNullOrEmpty(txtTelefono.Text) ? (long?)null : long.Parse(txtTelefono.Text);
                    cliente.direccion = txtDireccion.Text.Trim();

                    ClienteCln.Actualizar(cliente);
                    MessageBox.Show("Cliente actualizado correctamente", "Éxito",
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
                MessageBox.Show("Error al actualizar la base de datos: " + ex.InnerException?.Message, "Error de Base de Datos",
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
            if (idClienteSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un cliente de la lista", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            esNuevo = false;
            HabilitarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idClienteSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un cliente de la lista", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Está seguro de eliminar este cliente?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    ClienteCln.Eliminar(idClienteSeleccionado);
                    MessageBox.Show("Cliente eliminado correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Listar();
                    EstadoInicial();
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show("Error al eliminar el cliente. Es posible que existan registros relacionados que impidan la eliminación: " + ex.InnerException?.Message, "Error de Base de Datos",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvClientes.Rows[e.RowIndex];

                idClienteSeleccionado = Convert.ToInt32(fila.Cells["id"].Value);
                txtCI.Text = fila.Cells["cedulaIdentidad"].Value.ToString();
                txtNombres.Text = fila.Cells["nombres"].Value.ToString();
                txtApellidos.Text = fila.Cells["apellidos"].Value.ToString();
                txtTelefono.Text = fila.Cells["telefono"].Value?.ToString() ?? "";
                txtDireccion.Text = fila.Cells["direccion"].Value?.ToString() ?? "";
            }
        }
    }
}