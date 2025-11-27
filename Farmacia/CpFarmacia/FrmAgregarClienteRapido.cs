using CadFarmacia;
using ClnFarmacia;
using System;
using System.Windows.Forms;

namespace CpFarmacia
{
    public partial class FrmAgregarClienteRapido : Form
    {
        public int IdClienteCreado { get; private set; }

        public FrmAgregarClienteRapido()
        {
            InitializeComponent();
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

            if (string.IsNullOrWhiteSpace(txtApellidos.Text))
            {
                MessageBox.Show("Ingrese los apellidos", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellidos.Focus();
                return;
            }

            try
            {
                Cliente cliente = new Cliente
                {
                    cedulaIdentidad = txtCI.Text.Trim(),
                    nombres = txtNombres.Text.Trim(),
                    apellidos = txtApellidos.Text.Trim(),
                    telefono = string.IsNullOrEmpty(txtTelefono.Text) ? (long?)null : long.Parse(txtTelefono.Text),
                    estado = 1
                };

                IdClienteCreado = ClienteCln.Insertar(cliente);

                MessageBox.Show("Cliente registrado correctamente", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}