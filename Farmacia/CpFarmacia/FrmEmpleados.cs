using CadFarmacia;
using ClnFarmacia;
using System;
using System.Windows.Forms;

namespace CpFarmacia
{
    public partial class FrmEmpleados : Form
    {
        private int idEmpleadoSeleccionado = 0;
        private int idUsuarioExistente = 0;
        private bool esNuevo = false;

        public FrmEmpleados()
        {
            InitializeComponent();
        }

        private void FrmEmpleados_Load(object sender, EventArgs e)
        {
            CargarComboCargo();
            Listar();
            EstadoInicial();
        }

        private void CargarComboCargo()
        {
            cboCargo.Items.Clear();
            cboCargo.Items.Add("Farmacéutico");
            cboCargo.Items.Add("Técnico Farmacéutico");
            cboCargo.Items.Add("Cajero");
            cboCargo.Items.Add("Administrador");
            cboCargo.Items.Add("Almacenero");
            cboCargo.SelectedIndex = -1;
        }

        private void Listar()
        {
            dgvEmpleados.DataSource = EmpleadoCln.Listar(txtBuscar.Text.Trim());
            FormatearGrilla();
        }

        private void FormatearGrilla()
        {
            if (dgvEmpleados.Columns.Count > 0)
            {
                dgvEmpleados.Columns["id"].Visible = false;
                dgvEmpleados.Columns["cedulaIdentidad"].HeaderText = "CI";
                dgvEmpleados.Columns["nombres"].HeaderText = "Nombres";
                dgvEmpleados.Columns["primerApellido"].HeaderText = "Primer Apellido";
                dgvEmpleados.Columns["segundoApellido"].HeaderText = "Segundo Apellido";
                dgvEmpleados.Columns["direccion"].HeaderText = "Dirección";
                dgvEmpleados.Columns["celular"].HeaderText = "Celular";
                dgvEmpleados.Columns["cargo"].HeaderText = "Cargo";
                dgvEmpleados.Columns["usuarioRegistro"].Visible = false;
                dgvEmpleados.Columns["fechaRegistro"].Visible = false;
                dgvEmpleados.Columns["idUsuario"].Visible = false;
                dgvEmpleados.Columns["usuario"].HeaderText = "Usuario";
                dgvEmpleados.Columns["estado"].HeaderText = "Estado";
            }
        }

        private void EstadoInicial()
        {
            txtCI.Clear();
            txtNombres.Clear();
            txtPrimerApellido.Clear();
            txtSegundoApellido.Clear();
            txtDireccion.Clear();
            txtCelular.Clear();
            cboCargo.SelectedIndex = -1;
            txtUsuario.Clear();
            txtClaveUsuario.Clear();

            txtCI.Enabled = false;
            txtNombres.Enabled = false;
            txtPrimerApellido.Enabled = false;
            txtSegundoApellido.Enabled = false;
            txtDireccion.Enabled = false;
            txtCelular.Enabled = false;
            cboCargo.Enabled = false;
            txtUsuario.Enabled = false;
            txtClaveUsuario.Enabled = false;

            btnGuardar.Enabled = false;
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
            btnNuevo.Enabled = true;

            idEmpleadoSeleccionado = 0;
            idUsuarioExistente = 0;
            esNuevo = false;
        }

        private void HabilitarCampos()
        {
            txtCI.Enabled = true;
            txtNombres.Enabled = true;
            txtPrimerApellido.Enabled = true;
            txtSegundoApellido.Enabled = true;
            txtDireccion.Enabled = true;
            txtCelular.Enabled = true;
            cboCargo.Enabled = true;
            txtUsuario.Enabled = true;
            txtClaveUsuario.Enabled = true;

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
            idEmpleadoSeleccionado = 0;
            idUsuarioExistente = 0;

            txtCI.Clear();
            txtNombres.Clear();
            txtPrimerApellido.Clear();
            txtSegundoApellido.Clear();
            txtDireccion.Clear();
            txtCelular.Clear();
            cboCargo.SelectedIndex = -1;
            txtUsuario.Clear();
            txtClaveUsuario.Clear();

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

            if (string.IsNullOrWhiteSpace(txtNombres.Text))
            {
                MessageBox.Show("Ingrese los nombres", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombres.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MessageBox.Show("Ingrese la dirección", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDireccion.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCelular.Text))
            {
                MessageBox.Show("Ingrese el celular", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCelular.Focus();
                return;
            }

            if (cboCargo.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un cargo", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCargo.Focus();
                return;
            }

            // Validar usuario si se ingresa
            if (!string.IsNullOrWhiteSpace(txtUsuario.Text) && string.IsNullOrWhiteSpace(txtClaveUsuario.Text))
            {
                MessageBox.Show("Si ingresa usuario, debe ingresar una clave", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtClaveUsuario.Focus();
                return;
            }

            try
            {
                if (esNuevo)
                {
                    // Insertar nuevo empleado
                    Empleado empleado = new Empleado
                    {
                        cedulaIdentidad = txtCI.Text.Trim(),
                        nombres = txtNombres.Text.Trim(),
                        primerApellido = txtPrimerApellido.Text.Trim(),
                        segundoApellido = txtSegundoApellido.Text.Trim(),
                        direccion = txtDireccion.Text.Trim(),
                        celular = long.Parse(txtCelular.Text),
                        cargo = cboCargo.SelectedItem.ToString(),
                        estado = 1
                    };

                    int idEmpleado = EmpleadoCln.Insertar(empleado);

                    // Crear usuario si se ingresaron datos
                    if (!string.IsNullOrWhiteSpace(txtUsuario.Text) && !string.IsNullOrWhiteSpace(txtClaveUsuario.Text))
                    {
                        EmpleadoCln.CrearUsuario(idEmpleado, txtUsuario.Text.Trim(), txtClaveUsuario.Text);
                    }

                    MessageBox.Show("Empleado registrado correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Actualizar empleado existente
                    Empleado empleado = EmpleadoCln.ObtenerPorId(idEmpleadoSeleccionado);
                    empleado.cedulaIdentidad = txtCI.Text.Trim();
                    empleado.nombres = txtNombres.Text.Trim();
                    empleado.primerApellido = txtPrimerApellido.Text.Trim();
                    empleado.segundoApellido = txtSegundoApellido.Text.Trim();
                    empleado.direccion = txtDireccion.Text.Trim();
                    empleado.celular = long.Parse(txtCelular.Text);
                    empleado.cargo = cboCargo.SelectedItem.ToString();

                    EmpleadoCln.Actualizar(empleado);

                    // Crear usuario si no tiene y se ingresaron datos
                    if (idUsuarioExistente == 0 && !string.IsNullOrWhiteSpace(txtUsuario.Text) && !string.IsNullOrWhiteSpace(txtClaveUsuario.Text))
                    {
                        EmpleadoCln.CrearUsuario(idEmpleadoSeleccionado, txtUsuario.Text.Trim(), txtClaveUsuario.Text);
                    }

                    MessageBox.Show("Empleado actualizado correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Listar();
                EstadoInicial();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idEmpleadoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un empleado de la lista", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            esNuevo = false;
            HabilitarCampos();

            // Deshabilitar usuario si ya tiene uno
            if (idUsuarioExistente > 0)
            {
                txtUsuario.Enabled = false;
                txtClaveUsuario.Enabled = false;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idEmpleadoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un empleado de la lista", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Está seguro de eliminar este empleado?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    EmpleadoCln.Eliminar(idEmpleadoSeleccionado);
                    MessageBox.Show("Empleado eliminado correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Listar();
                    EstadoInicial();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvEmpleados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvEmpleados.Rows[e.RowIndex];

                idEmpleadoSeleccionado = Convert.ToInt32(fila.Cells["id"].Value);
                idUsuarioExistente = Convert.ToInt32(fila.Cells["idUsuario"].Value);

                txtCI.Text = fila.Cells["cedulaIdentidad"].Value.ToString();
                txtNombres.Text = fila.Cells["nombres"].Value.ToString();
                txtPrimerApellido.Text = fila.Cells["primerApellido"].Value?.ToString() ?? "";
                txtSegundoApellido.Text = fila.Cells["segundoApellido"].Value?.ToString() ?? "";
                txtDireccion.Text = fila.Cells["direccion"].Value.ToString();
                txtCelular.Text = fila.Cells["celular"].Value.ToString();

                // Seleccionar cargo en ComboBox
                string cargo = fila.Cells["cargo"].Value.ToString();
                cboCargo.SelectedItem = cargo;

                // Mostrar usuario si existe
                txtUsuario.Text = fila.Cells["usuario"].Value?.ToString() ?? "";
                txtClaveUsuario.Clear();
            }
        }
    }
}