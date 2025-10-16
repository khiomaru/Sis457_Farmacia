using ClnFarmacia2024;
using System;
using System.Windows.Forms;

namespace CpFarmacia2024
{
    public partial class FrmPequeMedicamento : Form
    {
        private FrmCompra frmCompra;
        private FrmVenta frmVenta;

        public FrmPequeMedicamento(FrmCompra compra)
        {
            InitializeComponent();
            frmCompra = compra;
        }

        public FrmPequeMedicamento(FrmVenta frmVenta)
        {
            this.frmVenta = frmVenta;
        }

        public void listar()
        {
            var lista = MedicamentoCln.listaaPa(txtParametropequeño.Text);
            dgvLista.DataSource = lista;
            dgvLista.Columns["id"].Visible = false;
            // dgvLista.Columns["razonSocial"].HeaderText = "Razón Social";
            // dgvLista.Columns["documento"].HeaderText = "N° Documento";

            if (lista.Count > 0) dgvLista.CurrentCell = dgvLista.Rows[0].Cells["codigo"];
        }

        private void txtParametro_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void FrmPequeMedicamento_Load(object sender, EventArgs e)
        {
            dgvLista.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            listar();
        }
        private void limpiar()
        {
            txtParametropequeño.Text = string.Empty;
        }

        private void dgvLista_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Verifica que se haya hecho clic en una fila válida
            {
                // Obtener los datos de la fila
                string idMedicamento = dgvLista.Rows[e.RowIndex].Cells["id"].Value.ToString();
                string codigo = dgvLista.Rows[e.RowIndex].Cells["codigo"].Value.ToString();
                string nombre = dgvLista.Rows[e.RowIndex].Cells["nombre"].Value.ToString();


                // Llamar al método en el formulario de compra para establecer los datos
                frmCompra.SetMedicamentoData(idMedicamento, codigo, nombre);

                // Cerrar el segundo formulario
                this.Close();
            }
        }

        private void txtParametropequeño_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) listar();
        }

        private void lblBusqueda_Click(object sender, EventArgs e)
        {

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









