using CadFarmacia2024;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CpFarmacia2024
{
    public partial class FrmPrincipal : Form
    {
        private FrmLogin frmLogin;
        public FrmPrincipal(FrmLogin frmLogin)
        {
            InitializeComponent();
            this.frmLogin = frmLogin;
            // comprobar si hay usuario logueado
            if (Util.usuario != null)
            {
                // Obtener el empleado asociado con el usuario
                lblUsuario.Text = "" + Util.usuario.usuario1;
                using (var context = new Labsis457farmaciaEntities())
                {
                    var empleado = context.Empleado
                                          .FirstOrDefault(e => e.id == Util.usuario.idEmpleado);  // Obtenemos el empleado con el id del usuario

                    // Verificamos si el empleado existe y tiene el cargo "propietario"
                    if (empleado != null && empleado.cargo != "Propietario")
                    {
                        menuUsuario.Enabled = false;  // Deshabilitar men� si no es "propietario"

                    }
                }
            }
            this.frmLogin = frmLogin;

        }

        private void menuProveedores_Click(object sender, EventArgs e)
        {
            //CargarFormularioEnPanel(new FrmProveedor()); //cargar a panelcontenedor
            new FrmProveedor().ShowDialog();
        }

        private void menuPacientes_Click(object sender, EventArgs e)
        {

            //CargarFormularioEnPanel(new FrmPaciente()); //cargar a panelcontenedor
            new FrmPaciente().ShowDialog();
        }

        private void categoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //CargarFormularioEnPanel(new FrmCategoria()); //cargar a panelcontenedor
            new FrmCategoria().ShowDialog();
        }
        private bool isExitConfirmed = false;
        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Si el usuario no ha confirmado la salida
            if (!isExitConfirmed)
            {
                // Muestra un cuadro de mensaje de confirmaci�n
                DialogResult result = MessageBox.Show("�Desea salir de la aplicaci�n?", "Confirmaci�n", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Marcar que el usuario ha confirmado salir
                    isExitConfirmed = true;

                    // Llamar a Application.Exit() para asegurarse de que la aplicaci�n se cierre completamente
                    Application.Exit();
                }
                else
                {
                    // Si el usuario elige "No", cancelamos el cierre del formulario
                    e.Cancel = true;
                }
            }
            else
            {
                // Si el usuario ya ha confirmado salir, no hacemos nada m�s
                e.Cancel = false;
            }
        }

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //CargarFormularioEnPanel(new FrmEmpleado()); //cargar a panelcontenedor
            new FrmEmpleado().ShowDialog();
        }

        private void MedicamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //CargarFormularioEnPanel(new FrmMedicamento()); //cargar a panelcontenedor
            new FrmMedicamento().ShowDialog();
        }

        private void registrarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //CargarFormularioEnPanel(new FrmCompra()); //cargar a panelcontenedor
            new FrmCompra().ShowDialog();
        }

        private void negocioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //CargarFormularioEnPanel(new FrmDetalleNegocio()); //cargar a panelcontenedor
            new FrmDetalleNegocio().ShowDialog();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void registrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmVenta().ShowDialog();
        }
        private void reportesVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmRegistroVentas().ShowDialog();
        }

        private void verDetalleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new FrmDetalleCompra().ShowDialog();
        }

        private void reportesComprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmRegistroCompras().ShowDialog();
        }

        private void verDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmDetalleVenta().ShowDialog();
        }


    }
}









