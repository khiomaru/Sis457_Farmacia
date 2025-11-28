using ClnFarmacia;
using System;
using System.Windows.Forms;

namespace CpFarmacia
{
    public partial class FrmLogin : Form
    {
        private int intentosFallidos = 0;
        private const int maxIntentos = 3;

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            // 1. Validar usuario vacío
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MessageBox.Show("Ingrese el usuario", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return;
            }

            // 2. Validar contraseña vacía
            if (string.IsNullOrWhiteSpace(txtClave.Text))
            {
                MessageBox.Show("Ingrese la contraseña", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtClave.Focus();
                return;
            }

            // 3. Consultar BD (SIN ENCRIPTAR)
            // Usamos Trim() para quitar espacios en blanco accidentales
            string usuarioTrim = txtUsuario.Text.Trim();
            string claveTrim = txtClave.Text.Trim();

            var usuario = UsuarioCln.ValidarAcceso(usuarioTrim, claveTrim);

            if (usuario != null)
            {
                // Login exitoso
                UsuarioCln.UsuarioLogueado = usuario; // Guardar en Capa Lógica
                Util.usuario = usuario;               // Guardar en Utilidades (Capa Presentación)

                // --- MODIFICADO AQUÍ ---
                // Ahora mostramos usuario.usuario1 (que es 'adolfo')
                MessageBox.Show($"Bienvenido: {usuario.usuario1}",
                                "Farmacia", MessageBoxButtons.OK, MessageBoxIcon.Information);

                FrmPrincipal frmPrincipal = new FrmPrincipal();
                frmPrincipal.Show();
                this.Hide();
            }
            else
            {
                // Login fallido
                intentosFallidos++;
                if (intentosFallidos >= maxIntentos)
                {
                    MessageBox.Show("Demasiados intentos fallidos. La aplicación se cerrará.", "Bloqueo de seguridad",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show($"Usuario o contraseña incorrectos. Intentos restantes: {maxIntentos - intentosFallidos}", "Error de acceso",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtClave.Clear();
                    txtClave.Focus();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}