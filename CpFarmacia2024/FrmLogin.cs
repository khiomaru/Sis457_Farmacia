using ClnFarmacia2024;
using System;
using System.Windows.Forms;

namespace CpFarmacia2024
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.Dpi;

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private bool validar()
        {
            bool esValido = true;
            erpUsuario.SetError(txtUsuario, "");
            erpClave.SetError(txtClave, "");
            if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                esValido = false;
                erpUsuario.SetError(txtUsuario, "El campo Usuario es obligatorio");
            }
            if (string.IsNullOrEmpty(txtClave.Text))
            {
                esValido = false;
                erpClave.SetError(txtClave, "El campo clave es obligatorio");
            }
            if (!rdbUsuario.Checked && !rdbPropietario.Checked)
            {
                esValido = false;
                MessageBox.Show("Debe seleccionar un rol", "::: Farmacia - Mensaje :::", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return esValido;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                string rolSeleccionado = rdbUsuario.Checked ? "Usuario" : "Propietario";
                var usuario = UsuarioCln.validar(txtUsuario.Text, txtClave.Text, rolSeleccionado);
                if (usuario != null)
                {
                    Util.usuario = usuario;
                    txtClave.Text = string.Empty;
                    txtUsuario.Focus();
                    txtUsuario.SelectAll();

                    this.Hide();
                    FrmPrincipal frmPrincipal = new FrmPrincipal(this);
                    frmPrincipal.ShowDialog();
                }
                else MessageBox.Show("Usuario y/o contrasea incorrecto o rol no coincide", " ::: Farmacia - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) btnIngresar.PerformClick();
        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtClave.Focus();
                e.SuppressKeyPress = true;

            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
