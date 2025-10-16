using ClnFarmacia2024;
using System;
using System.Windows.Forms;

namespace CpFarmacia2024
{
    public partial class FrmPequePaciente : Form
    {
        private FrmVenta frmVenta;
        public FrmPequePaciente(FrmVenta venta)
        {
            InitializeComponent();
            frmVenta = venta;
        }

        public void listar()
        {
            var lista = PacienteCln.listarPa(txtParametro.Text);
            dgvLista.DataSource = lista;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["estado"].Visible = false;
            dgvLista.Columns["email"].Visible = false;
            dgvLista.Columns["telefono"].Visible = false;
            dgvLista.Columns["nombreCompleto"].HeaderText = "Nombre Completo";
            dgvLista.Columns["documento"].HeaderText = "N° Documento";

            if (lista.Count > 0) dgvLista.CurrentCell = dgvLista.Rows[0].Cells["documento"];
        }

        private void txtParametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) listar();
        }

        private void FrmPequePaciente_Load(object sender, EventArgs e)
        {
            dgvLista.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            listar();
        }

        private void limpiar()
        {
            txtParametro.Text = string.Empty;
        }

        private void dgvLista_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Verifica que se haya hecho clic en una fila válida
            {
                // Obtener los datos de la fila
                string idPaciente = dgvLista.Rows[e.RowIndex].Cells["id"].Value.ToString();
                string documento = dgvLista.Rows[e.RowIndex].Cells["documento"].Value.ToString();
                string nombreCompleto = dgvLista.Rows[e.RowIndex].Cells["nombreCompleto"].Value.ToString();

                // Llamar al método en el formulario de venta para establecer los datos
                frmVenta.SetPacienteData(idPaciente, documento, nombreCompleto);

                // Cerrar el segundo formulario
                this.Close();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            listar();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
    }
}
