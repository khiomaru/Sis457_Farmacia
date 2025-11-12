using ClnFarmacia2024;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CpFarmacia2024
{
    public partial class FrmLogin : Form
    {
        #region 🔹 Win32 (Permitir mover formulario sin borde)
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        #endregion

        #region 🔹 Constructor
        public FrmLogin()
        {
            InitializeComponent();
        }
        #endregion

        #region 🔹 Eventos del formulario
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            AplicarBordesRedondeados(25);
            CenterToScreen(); // Centra el formulario al abrir
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea salir del sistema?", "Confirmación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        #endregion

        #region 🔹 Métodos privados de apoyo
        /// <summary>
        /// Aplica esquinas redondeadas al formulario.
        /// </summary>
        /// <param name="radio">Radio de curvatura en píxeles.</param>
        private void AplicarBordesRedondeados(int radio)
        {
            using (var path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                path.StartFigure();
                path.AddArc(0, 0, radio, radio, 180, 90);
                path.AddArc(Width - radio, 0, radio, radio, 270, 90);
                path.AddArc(Width - radio, Height - radio, radio, radio, 0, 90);
                path.AddArc(0, Height - radio, radio, radio, 90, 90);
                path.CloseFigure();
                Region = new Region(path);
            }
        }

        /// <summary>
        /// Valida que los campos obligatorios estén completos.
        /// </summary>
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(tbusername.Text))
            {
                MostrarMensaje("Debe ingresar el nombre de usuario.", MessageBoxIcon.Warning);
                tbusername.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(tbpassword.Text))
            {
                MostrarMensaje("Debe ingresar la contraseña.", MessageBoxIcon.Warning);
                tbpassword.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Muestra un mensaje uniforme.
        /// </summary>
        private static void MostrarMensaje(string mensaje, MessageBoxIcon icono = MessageBoxIcon.Information)
        {
            MessageBox.Show(mensaje, "::: Farmacia - Mensaje :::", MessageBoxButtons.OK, icono);
        }
        #endregion

        #region 🔹 Lógica del botón Ingresar
        private void button2_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            string rolSeleccionado = "Usuario"; // Valor por defecto

            try
            {
                var usuario = UsuarioCln.validar(tbusername.Text.Trim(), tbpassword.Text.Trim(), rolSeleccionado);

                if (usuario != null)
                {
                    Util.usuario = usuario;
                    MostrarMensaje($"Bienvenido {usuario.usuario1}");

                    Hide();
                    using (var frmPrincipal = new FrmPrincipal(this))
                    {
                        frmPrincipal.ShowDialog();
                    }

                    // Cuando cierra el principal, mostrar nuevamente login
                    Show();
                    tbpassword.Clear();
                    tbusername.Focus();
                }
                else
                {
                    MostrarMensaje("Usuario y/o contraseña incorrectos o rol no coincide.", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al intentar iniciar sesión:\n{ex.Message}", MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
