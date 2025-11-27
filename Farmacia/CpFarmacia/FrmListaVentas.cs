using CadFarmacia;
using ClnFarmacia;
using System;
using System.Windows.Forms;

namespace CpFarmacia
{
    public partial class FrmListaVentas : Form
    {
        private int idVentaSeleccionada = 0;

        public FrmListaVentas()
        {
            InitializeComponent();
        }

        private void FrmListaVentas_Load(object sender, EventArgs e)
        {
            Listar();
        }

        private void Listar()
        {
            dgvVentas.DataSource = VentaCln.Listar(txtBuscar.Text.Trim());
            FormatearGrillaVentas();
            LimpiarDetalle();
        }

        private void FormatearGrillaVentas()
        {
            if (dgvVentas.Columns.Count > 0)
            {
                dgvVentas.Columns["id"].Visible = false;
                dgvVentas.Columns["numeroFactura"].HeaderText = "Nro. Factura";
                dgvVentas.Columns["numeroFactura"].Width = 120;
                dgvVentas.Columns["Cliente"].HeaderText = "Cliente";
                dgvVentas.Columns["Cliente"].Width = 200;
                dgvVentas.Columns["Usuario"].HeaderText = "Usuario";
                dgvVentas.Columns["Usuario"].Width = 120;
                dgvVentas.Columns["total"].HeaderText = "Total";
                dgvVentas.Columns["total"].Width = 100;
                dgvVentas.Columns["total"].DefaultCellStyle.Format = "N2";
                dgvVentas.Columns["fechaVenta"].HeaderText = "Fecha";
                dgvVentas.Columns["fechaVenta"].Width = 150;
                dgvVentas.Columns["estado"].HeaderText = "Estado";
                dgvVentas.Columns["estado"].Width = 80;
            }
        }

        private void LimpiarDetalle()
        {
            dgvDetalle.DataSource = null;
            idVentaSeleccionada = 0;
        }

        private void CargarDetalle(int idVenta)
        {
            var detalles = DetalleVentaCln.ObtenerPorVenta(idVenta);

            var listaDetalle = new System.Collections.Generic.List<object>();
            foreach (var d in detalles)
            {
                listaDetalle.Add(new
                {
                    codigo = d.Medicamento.codigo,
                    medicamento = d.Medicamento.nombre,
                    precioUnitario = d.precioUnitario,
                    cantidad = d.cantidad,
                    subtotal = d.subtotal
                });
            }

            dgvDetalle.DataSource = listaDetalle;
            FormatearGrillaDetalle();
        }

        private void FormatearGrillaDetalle()
        {
            if (dgvDetalle.Columns.Count > 0)
            {
                dgvDetalle.Columns["codigo"].HeaderText = "Código";
                dgvDetalle.Columns["codigo"].Width = 100;
                dgvDetalle.Columns["medicamento"].HeaderText = "Medicamento";
                dgvDetalle.Columns["medicamento"].Width = 300;
                dgvDetalle.Columns["precioUnitario"].HeaderText = "Precio Unit.";
                dgvDetalle.Columns["precioUnitario"].Width = 100;
                dgvDetalle.Columns["precioUnitario"].DefaultCellStyle.Format = "N2";
                dgvDetalle.Columns["cantidad"].HeaderText = "Cantidad";
                dgvDetalle.Columns["cantidad"].Width = 80;
                dgvDetalle.Columns["subtotal"].HeaderText = "Subtotal";
                dgvDetalle.Columns["subtotal"].Width = 120;
                dgvDetalle.Columns["subtotal"].DefaultCellStyle.Format = "N2";
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Listar();
        }

        private void dgvVentas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvVentas.Rows[e.RowIndex];
                idVentaSeleccionada = Convert.ToInt32(fila.Cells["id"].Value);
                CargarDetalle(idVentaSeleccionada);
            }
        }

        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            if (idVentaSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una venta de la lista", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CargarDetalle(idVentaSeleccionada);
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (idVentaSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una venta de la lista", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar que la venta no esté ya anulada
            if (dgvVentas.CurrentRow != null)
            {
                int estado = Convert.ToInt32(dgvVentas.CurrentRow.Cells["estado"].Value);
                if (estado == 0)
                {
                    MessageBox.Show("Esta venta ya está anulada", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            DialogResult result = MessageBox.Show(
                "¿Está seguro de anular esta venta?\n\nEsta acción devolverá el stock de los medicamentos.",
                "Confirmar anulación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    VentaCln.Anular(idVentaSeleccionada);

                    MessageBox.Show("Venta anulada correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Listar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al anular la venta: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}