using ClnFarmacia2024;
using System;
using System.Windows.Forms;

namespace CpFarmacia2024
{
    public partial class FrmDetalleNegocio : Form
    {
        public FrmDetalleNegocio()
        {
            InitializeComponent();
        }
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtDireccion.Clear();
            txtNit.Clear();
        }

        private void btnMostrarDatos_Click(object sender, EventArgs e)
        {

            var detalle = DetalleNegocioCln.Obtener();

            if (detalle != null)
            {
                txtNombre.Text = detalle.nombre;
                txtDireccion.Text = detalle.direccion;
                txtNit.Text = detalle.nit;

            }
            else
            {
                MessageBox.Show("No se encontraron datos del negocio.");
            }
        }

        private void FrmDetalleNegocio_Load(object sender, EventArgs e)
        {

        }
    }
}










