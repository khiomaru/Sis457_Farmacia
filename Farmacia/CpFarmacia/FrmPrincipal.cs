using ClnFarmacia;
using System;
using System.Windows.Forms;

namespace CpFarmacia
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            // Mostrar nombre del usuario en el título
            if (UsuarioCln.UsuarioLogueado != null && UsuarioCln.UsuarioLogueado.Empleado != null)
            {
                this.Text = "Sistema de Farmacia - " +
                           UsuarioCln.UsuarioLogueado.Empleado.nombres + " " +
                           UsuarioCln.UsuarioLogueado.Empleado.primerApellido;
            }
        }

        // Método para abrir formularios hijos
        private void AbrirFormulario(Form formulario)
        {
            formulario.MdiParent = this;
            formulario.Show();
        }

        // ==================== MENÚ ARCHIVO ====================
        private void cerrarSesiónToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            UsuarioCln.UsuarioLogueado = null;
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.Show();
            this.Close();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // ==================== MENÚ MANTENIMIENTO ====================
        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmClientes());
        }

        private void medicamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmMedicamentos());
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmCategorias());
        }

        private void laboratoriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmLaboratorios());
        }

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmEmpleados());
        }

        // ==================== MENÚ OPERACIONES ====================
        private void nuevaVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmVentas());
        }

        private void listaDeVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmListaVentas());
        }

        private void FrmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}