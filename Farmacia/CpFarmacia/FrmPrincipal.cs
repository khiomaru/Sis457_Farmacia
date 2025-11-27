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
            // IMPORTANTE: Asegúrate de que la propiedad "IsMdiContainer" de este Form
            // esté en TRUE desde el diseñador (Propiedades -> IsMdiContainer).

            // Mostrar nombre del usuario (ej: adolfo) en el título
            if (Util.usuario != null)
            {
                this.Text = $"Sistema de Farmacia - Usuario: {Util.usuario.usuario1}";

                // Si quieres mostrar también el cargo, puedes descomentar esto:
                // this.Text += $" ({Util.usuario.Empleado.cargo})";
            }
        }

        // Método para abrir formularios hijos
        private void AbrirFormulario(Form formulario)
        {
            // Si este formulario no es MDI Container, esto fallará.
            // Si da error, ve al Diseñador y pon IsMdiContainer = True
            formulario.MdiParent = this;
            formulario.Show();
        }

        // ==================== MENÚ ARCHIVO ====================
        private void cerrarSesiónToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            UsuarioCln.UsuarioLogueado = null;
            Util.usuario = null;
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.Show();
            this.Hide(); // Ocultamos el principal en lugar de cerrarlo para evitar cerrar la App
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // ==================== MENÚ MANTENIMIENTO ====================
        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // AbrirFormulario(new FrmClientes()); // Pendiente de crear
            MessageBox.Show("Módulo de Clientes en construcción");
        }

        private void medicamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // AbrirFormulario(new FrmMedicamentos()); // Pendiente de crear
            MessageBox.Show("Módulo de Medicamentos en construcción");
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // AbrirFormulario(new FrmCategorias()); // Pendiente de crear
            MessageBox.Show("Módulo de Categorías en construcción");
        }

        private void laboratoriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // AbrirFormulario(new FrmLaboratorios()); // Pendiente de crear
            MessageBox.Show("Módulo de Laboratorios en construcción");
        }

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // AbrirFormulario(new FrmEmpleados()); // Pendiente de crear
            MessageBox.Show("Módulo de Empleados en construcción");
        }

        // ==================== MENÚ OPERACIONES ====================
        private void nuevaVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // AbrirFormulario(new FrmVentas()); // Pendiente de crear
            MessageBox.Show("Módulo de Ventas en construcción");
        }

        private void listaDeVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // AbrirFormulario(new FrmListaVentas()); // Pendiente de crear
            MessageBox.Show("Módulo de Lista de Ventas en construcción");
        }

        private void FrmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}