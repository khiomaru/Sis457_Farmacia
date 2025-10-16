using ClnFarmacia2024;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Windows.Forms;

namespace CpFarmacia2024
{
    public partial class FrmDetalleVenta : Form
    {
        public FrmDetalleVenta()
        {
            InitializeComponent();
            btnDescargarPdf.Click += btnDescargarPdf_Click;

            // Configurar el DataGridView
            dgvDetalles.Columns.Add("idMedicamento", "ID Medicamento");
            dgvDetalles.Columns.Add("codigo", "Código");
            dgvDetalles.Columns.Add("nombre", "Nombre");
            dgvDetalles.Columns.Add("precioCompra", "Precio de Compra");
            dgvDetalles.Columns.Add("precioVenta", "Precio de Venta");
            dgvDetalles.Columns.Add("cantidad", "Cantidad");
            dgvDetalles.Columns.Add("total", "Total");
            dgvDetalles.Columns["idMedicamento"].Visible = false;
        }

        private void btnBuscarNFactura_Click(object sender, EventArgs e)
        {
            string numeroDocumento = txtNFacturaBoleta.Text.Trim();
            if (string.IsNullOrEmpty(numeroDocumento))
            {
                MessageBox.Show("Por favor, ingrese un número de documento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var compra = CompraCln.ObtenerCompraPorNumeroDocumento(numeroDocumento);
            if (compra != null)
            {
                // Llenar los TextBox con la información de la compra
                txtTipoDocumento.Text = compra.tipoDocumento;
                txtUsuario.Text = compra.usuarioRegistro;
                txtFecha.Text = compra.fechaRegistro.ToString("dd/MM/yyyy");

                // Obtener los detalles de la compra
                var detalles = DetalleCompraCln.ObtenerDetallesPorIdCompra(compra.id);
                dgvDetalles.Rows.Clear();

                if (detalles.Count == 0)
                {
                    MessageBox.Show("No se encontraron detalles para esta compra.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    foreach (var detalle in detalles)
                    {
                        dgvDetalles.Rows.Add(detalle.idMedicamento, detalle.codigo, detalle.nombre, detalle.precioCompra, detalle.precioVenta, detalle.cantidad, detalle.total);
                    }
                }

                // Obtener y mostrar la razón social del proveedor
                var proveedor = ProveedorCln.ObtenerUno(compra.idProveedor);
                txtDocuProveedor.Text = proveedor?.documento ?? "Proveedor no encontrado";
                txtRazonSocial.Text = proveedor?.razonSocial ?? "Proveedor no encontrado";

                // Calcular y mostrar el total a pagar
                CalcularTotalPagar();
            }
            else
            {
                MessageBox.Show("Compra no encontrada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            HabilitarCampos();
        }

        private void CalcularTotalPagar()
        {
            decimal totalPagar = 0;
            foreach (DataGridViewRow row in dgvDetalles.Rows)
            {
                // Asegúrate de que la fila no sea una fila de nuevo
                if (row.Cells["total"].Value != null)
                {
                    totalPagar += Convert.ToDecimal(row.Cells["Total"].Value);
                }
            }

            // Mostrar el total en el TextBox
            txtTotalPagar.Text = totalPagar.ToString("0.00"); // Formato con dos decimales
        }

        private void DesactivarCampos()
        {
            txtFecha.Enabled = false;
            txtTipoDocumento.Enabled = false;
            txtUsuario.Enabled = false;
            txtDocuProveedor.Enabled = false;
            txtRazonSocial.Enabled = false;
        }

        private void HabilitarCampos()
        {
            txtFecha.Enabled = true;
            txtTipoDocumento.Enabled = true;
            txtUsuario.Enabled = true;
            txtDocuProveedor.Enabled = true;
            txtRazonSocial.Enabled = true;
        }

        private void btnDescargarPdf_Click(object sender, EventArgs e)
        {
            // Crear un nuevo documento
            Document doc = new Document();

            // Especificar la ruta del archivo PDF
            string ruta = "DetallesCompra_Formulario.pdf";

            try
            {
                // Inicializar el escritor
                PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.Create));
                doc.Open();

                // Agregar un título
                doc.Add(new Paragraph("Detalles de Compra"));
                doc.Add(new Paragraph("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy")));

                // Agregar la información de los TextBox y Label
                doc.Add(new Paragraph("Número de Factura: " + txtNFacturaBoleta.Text));
                doc.Add(new Paragraph("Tipo de Documento: " + txtTipoDocumento.Text));
                doc.Add(new Paragraph("Usuario: " + txtUsuario.Text));
                doc.Add(new Paragraph("Fecha de Registro: " + txtFecha.Text));
                doc.Add(new Paragraph("Proveedor: " + txtDocuProveedor.Text));
                doc.Add(new Paragraph("Razón Social: " + txtRazonSocial.Text));
                doc.Add(new Paragraph("Total a Pagar: " + txtTotalPagar.Text));

                // Agregar los detalles del DataGridView
                doc.Add(new Paragraph("\nDetalles de los Medicamentos Comprados:"));

                // Crear una tabla con el número de columnas
                PdfPTable table = new PdfPTable(dgvDetalles.Columns.Count);

                // Agregar encabezados
                foreach (DataGridViewColumn column in dgvDetalles.Columns)
                {
                    table.AddCell(column.HeaderText);
                }

                // Agregar filas del DataGridView
                foreach (DataGridViewRow row in dgvDetalles.Rows)
                {
                    if (row.Cells["idMedicamento"].Value != null) // Evitar filas vacías
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            table.AddCell(cell.Value?.ToString());
                        }
                    }
                }

                // Agregar la tabla al documento
                doc.Add(table);

                MessageBox.Show($"PDF exportado correctamente a {ruta}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar a PDF: {ex.Message}");
            }
            finally
            {
                // Cerrar el documento
                doc.Close();
            }
        }

        private void FrmDetalleVenta_Load(object sender, EventArgs e)
        {
            txtNFacturaBoleta.KeyPress += Util.onlyNumbers;
            DesactivarCampos();
            dgvDetalles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDetalles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnLimpiarNFactura_Click(object sender, EventArgs e)
        {
            limpiar();
            limpiarDatos();
            DesactivarCampos();
        }

        private void limpiar()
        {
            txtNFacturaBoleta.Text = string.Empty;
        }

        private void limpiarDatos()
        {
            txtFecha.Text = string.Empty;
            txtTipoDocumento.Text = string.Empty;
            txtUsuario.Text = string.Empty;
            txtDocuProveedor.Text = string.Empty;
            txtRazonSocial.Text = string.Empty;
        }
    }
}
