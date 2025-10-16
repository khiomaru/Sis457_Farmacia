using CadFarmacia2024;
using ClnFarmacia2024;
using System;
using System.Data.Entity.Validation;
using System.Text;
using System.Windows.Forms;

namespace CpFarmacia2024
{

    public partial class FrmCompra : Form
    {

        public FrmCompra()
        {
            InitializeComponent();
        }

        private void FrmCompra_Load(object sender, EventArgs e)
        {

            txtNfactura.KeyPress += Util.onlyNumbers;
            txtPrecioCompra.KeyPress += Util.onlyDecimals;
            txtPrecioVenta.KeyPress += Util.onlyDecimals;
            txtNfactura.KeyPress += Util.onlyNumbers;


            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtIdMedicamento.Text = "0";
            txtIdProveedor.Text = "0";

            dgvCompras.Columns.Add("idMedicamento", "ID Medicamento");
            dgvCompras.Columns.Add("codigo", "Código");
            dgvCompras.Columns.Add("nombre", "Nombre");
            dgvCompras.Columns.Add("precioCompra", "Precio de Compra");
            dgvCompras.Columns.Add("precioVenta", "Precio de Venta");
            dgvCompras.Columns.Add("cantidad", "Cantidad");
            dgvCompras.Columns.Add("total", "Total");

            dgvCompras.Columns["idMedicamento"].Visible = false;
            dgvCompras.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DesactivarCamposMedicamentos();
            DesactivarCamposProveedor();
        }
        private void DesactivarCamposMedicamentos()
        {
            txtCodigoMedicamento.Enabled = false;
            txtMedicamento.Enabled = false;
        }
        private void HabilitarCamposMedicamentos()
        {
            txtCodigoMedicamento.Enabled = true;
            txtMedicamento.Enabled = true;
        }
        public void SetProveedorData(string idProveedor, string documento, string razonSocial)
        {
            txtIdProveedor.Text = idProveedor;  // Asumiendo que tienes este TextBox
            txtDocuProveedor.Text = documento; // Asumiendo que tienes este TextBox
            txtRazonSocial.Text = razonSocial;   // Asumiendo que tienes este TextBo

        }

        private bool validar()
        {
            bool esValido = true;
            erpTipoDocumento.SetError(cbxTipoDocumento, "");
            erpNfactura.SetError(txtNfactura, "");
            erpDocuProveedor.SetError(txtDocuProveedor, "");
            erpCodigoMedicamento.SetError(txtCodigoMedicamento, "");
            erpPrecioCompra.SetError(txtPrecioCompra, "");
            erpPrecioVenta.SetError(txtPrecioVenta, "");
            erpCantidad.SetError(nudCantidad, "");

            if (string.IsNullOrEmpty(cbxTipoDocumento.Text))
            {
                esValido = false;
                erpTipoDocumento.SetError(cbxTipoDocumento, "El campo Tipo Documento es obligatorio");
            }
            if (string.IsNullOrEmpty(txtNfactura.Text))
            {
                esValido = false;
                erpNfactura.SetError(txtNfactura, "Este campo es obligatorio");
            }
            if (string.IsNullOrEmpty(txtDocuProveedor.Text))
            {
                esValido = false;
                erpDocuProveedor.SetError(txtDocuProveedor, "Este campo no debe estar vacio");
            }
            if (string.IsNullOrEmpty(txtCodigoMedicamento.Text))
            {
                esValido = false;
                erpCodigoMedicamento.SetError(txtCodigoMedicamento, "Este campo no debe estar vacio");
            }
            if (string.IsNullOrEmpty(txtPrecioCompra.Text))
            {
                esValido = false;
                erpPrecioCompra.SetError(txtPrecioCompra, "Este campo precio de compra es obligatorio");
            }
            if (string.IsNullOrEmpty(txtPrecioVenta.Text))
            {
                esValido = false;
                erpPrecioVenta.SetError(txtPrecioVenta, "El campo precio de venta es obligatorio");
            }
            if (decimal.TryParse(txtPrecioCompra.Text, out decimal precioCompra) &&
         decimal.TryParse(txtPrecioVenta.Text, out decimal precioVenta))
            {
                if (precioCompra > precioVenta)
                {
                    esValido = false;
                    erpPrecioVenta.SetError(txtPrecioVenta, "El precio de venta debe ser mayor que el precio de compra.");
                    MessageBox.Show("El precio de venta debe ser mayor que el precio de compra.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            if (nudCantidad.Value < 0)
            {
                esValido = false;
                erpCantidad.SetError(nudCantidad, "El campo Cantidad no debe ser negativo");
            }
            return esValido;
        }

        public void SetMedicamentoData(string idMedicamento, string codigo, string nombre)
        {
            txtIdMedicamento.Text = idMedicamento;
            txtCodigoMedicamento.Text = codigo;
            txtMedicamento.Text = nombre;
        }
        private void ibtnAgregar_Click(object sender, EventArgs e)
        {
            if (!validar())
            {
                // Si no es válido, muestra un mensaje y no continúa
                MessageBox.Show("Por favor, corrige los errores antes de agregar el Medicamento.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Detiene la ejecución si la validación falla
            }
            var idMedicamento = txtIdMedicamento.Text;
            var codigo = txtCodigoMedicamento.Text;
            var nombre = txtMedicamento.Text;
            var precioCompra = decimal.Parse(txtPrecioCompra.Text);
            var precioVenta = decimal.Parse(txtPrecioVenta.Text);
            var cantidad = int.Parse(nudCantidad.Text);
            var total = precioCompra * cantidad;

            // Agregar una nueva fila al DataGridView
            dgvCompras.Rows.Add(idMedicamento, codigo, nombre, precioCompra, precioVenta, cantidad, total);

            // Limpiar los campos después de agregar
            LimpiarCampos();
            CalcularTotalPagar();
        }
        private void CalcularTotalPagar()
        {
            decimal totalPagar = 0;

            foreach (DataGridViewRow row in dgvCompras.Rows)
            {
                // Asegurarse de que la fila no sea una fila de nuevo
                if (row.Cells["Total"].Value != null)
                {
                    totalPagar += Convert.ToDecimal(row.Cells["Total"].Value);
                }
            }

            // Mostrar el total en el TextBox
            txtTotalPagar.Text = totalPagar.ToString("0.00"); // Formato con dos decimales
        }

        private void LimpiarCampos()
        {
            txtIdMedicamento.Text = string.Empty;
            txtCodigoMedicamento.Text = string.Empty;
            txtMedicamento.Text = string.Empty;
            txtPrecioCompra.Text = string.Empty;
            txtPrecioVenta.Text = string.Empty;
            nudCantidad.Text = string.Empty;
        }
        private void LimpiarTodo()
        {
            txtIdMedicamento.Text = string.Empty;
            txtCodigoMedicamento.Text = string.Empty;
            txtMedicamento.Text = string.Empty;
            txtPrecioCompra.Text = string.Empty;
            txtPrecioVenta.Text = string.Empty;
            nudCantidad.Text = string.Empty;
            cbxTipoDocumento.Text = string.Empty;
            txtNfactura.Text = string.Empty;
            txtDocuProveedor.Text = string.Empty;
            txtRazonSocial.Text = string.Empty;
            txtIdProveedor.Text = string.Empty;
        }
        private void iBtnRegistrar_Click(object sender, EventArgs e)
        {


            // Verifica que haya al menos un Medicamento en el DataGridView
            if (dgvCompras.Rows.Count == 0)
            {
                MessageBox.Show("No hay Medicamentos para registrar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string numeroDocumento = txtNfactura.Text.Trim();

            // Verifica si el número de factura ya existe
            if (CompraCln.ExisteNumeroFactura(numeroDocumento))
            {
                MessageBox.Show("El número de factura ya existe. Por favor, ingrese un número único.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //// Verifica que el tipo de documento seleccionado sea válido
            if (cbxTipoDocumento.SelectedItem == null || !cbxTipoDocumento.Items.Contains(cbxTipoDocumento.SelectedItem))
            {
                MessageBox.Show("Por favor, seleccione un tipo de documento válido.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //obtener informacion de la compra
            var compra = CrearCompra(numeroDocumento);

            if (compra == null)
            {
                return; // Se generó un error en la creación de la compra
            }

            try
            {
                // Registrar la compra en la base de datos
                int idCompraRegistrada = CompraCln.RegistrarCompra(compra);
                RegistrarDetallesCompra(idCompraRegistrada);
                ActualizarStockYPrecios();

                MessageBox.Show("Compra registrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ManejarErrorRegistro(ex, "Error al registrar la compra");
            }

            // Limpiar el DataGridView y el total
            dgvCompras.Rows.Clear();
            txtTotalPagar.Text = "0.00";
            LimpiarTodo();
        }


        private Compra CrearCompra(string numeroDocumento)
        {
            string tipoDocumento = cbxTipoDocumento.SelectedItem.ToString();
            decimal montoTotal = decimal.Parse(txtTotalPagar.Text);
            int idProveedor = Convert.ToInt32(txtIdProveedor.Text);
            string usuarioRegistro = Util.usuario.usuario1 ?? "Usuario Desconocido";

            return new Compra
            {
                idProveedor = idProveedor,
                tipoDocumento = tipoDocumento,
                numeroDocumento = numeroDocumento,
                montoTotal = montoTotal,
                fechaRegistro = DateTime.Now,
                usuarioRegistro = usuarioRegistro,
                estado = 1
            };
        }

        private void RegistrarDetallesCompra(int idCompraRegistrada)
        {
            foreach (DataGridViewRow row in dgvCompras.Rows)
            {
                if (row.Cells["IdMedicamento"].Value != null)
                {
                    if (!ValidarDetallesCompra(row, idCompraRegistrada, out var detalleCompra))
                    {
                        return; // Si hay un error de validación, detenemos la ejecución
                    }

                    // Aquí es donde debes inicializar detalleCompra
                    detalleCompra = new DetalleCompra
                    {
                        idCompra = idCompraRegistrada,
                        idMedicamento = Convert.ToInt32(row.Cells["IdMedicamento"].Value),
                        precioCompra = Convert.ToDecimal(row.Cells["PrecioCompra"].Value),
                        precioVenta = Convert.ToDecimal(row.Cells["PrecioVenta"].Value),
                        cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value),
                        total = Convert.ToDecimal(row.Cells["Total"].Value),
                        usuarioRegistro = Util.usuario.usuario1 ?? "Usuario Desconocido",
                        fechaRegistro = DateTime.Now,
                        estado = 1
                    };

                    try
                    {
                        DetalleCompraCln.RegistrarDetalleCompra(detalleCompra);



                    }
                    catch (DbEntityValidationException validationEx)
                    {
                        ManejarErrorValidacion(validationEx, "Error al registrar detalle");
                    }
                    catch (Exception ex)
                    {
                        ManejarErrorRegistro(ex, "Error al registrar detalle de compra");
                    }
                }
            }

        }

        private bool ValidarDetallesCompra(DataGridViewRow row, int idCompraRegistrada, out DetalleCompra detalleCompra)
        {
            var idMedicamento = Convert.ToInt32(row.Cells["idMedicamento"].Value);
            var precioCompra = Convert.ToDecimal(row.Cells["precioCompra"].Value);
            var precioVenta = Convert.ToDecimal(row.Cells["precioVenta"].Value);
            var cantidad = Convert.ToInt32(row.Cells["cantidad"].Value);

            // Validar que los precios y la cantidad sean válidos
            if (precioCompra < 0 || precioVenta < 0 || cantidad <= 0)
            {
                MessageBox.Show("Los valores de precio y cantidad deben ser positivos.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                detalleCompra = null; // Asignar null ya que hay un error
                return false; // Indicar que la validación falló
            }

            detalleCompra = new DetalleCompra
            {
                idCompra = idCompraRegistrada, // ID de la compra registrada
                idMedicamento = idMedicamento,
                precioCompra = precioCompra,
                precioVenta = precioVenta,
                cantidad = cantidad
            };
            Console.WriteLine($"DetalleCompra: {detalleCompra.ToString()}");
            return true; // Indicar que la validación fue exitosa
        }

        private void ManejarErrorValidacion(DbEntityValidationException validationEx, string mensaje)
        {
            StringBuilder errorDetails = new StringBuilder();
            foreach (var validationErrors in validationEx.EntityValidationErrors)
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    errorDetails.AppendLine($"{mensaje}: Propiedad: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                }
            }
            MessageBox.Show(errorDetails.ToString(), "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ManejarErrorRegistro(Exception ex, string mensaje)
        {
            MessageBox.Show($"{mensaje}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ActualizarStockYPrecios()
        {
            foreach (DataGridViewRow row in dgvCompras.Rows)
            {
                if (row.Cells["IdMedicamento"].Value != null)
                {
                    var idMedicamento = Convert.ToInt32(row.Cells["IdMedicamento"].Value);
                    var cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value);
                    var precioCompra = Convert.ToDecimal(row.Cells["PrecioCompra"].Value);
                    var precioVenta = Convert.ToDecimal(row.Cells["PrecioVenta"].Value);

                    try
                    {
                        // Llama al método ActualizarStock con los parámetros correctos
                        bool actualizado = MedicamentoCln.ActualizarStock(idMedicamento, precioCompra, precioVenta, cantidad);

                        if (!actualizado)
                        {
                            MessageBox.Show($"No se pudo actualizar el stock para el Medicamento ID {idMedicamento}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al actualizar el Medicamento ID {idMedicamento}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void lblTotalPagar_Click(object sender, EventArgs e)
        {

        }

        private void txtTotalPagar_TextChanged(object sender, EventArgs e)
        {

        }

        private void iBtnQuitar_Click(object sender, EventArgs e)
        {
            // Verificar si hay una fila seleccionada
            if (dgvCompras.SelectedRows.Count > 0)
            {
                // Eliminar la fila seleccionada
                foreach (DataGridViewRow row in dgvCompras.SelectedRows)
                {
                    dgvCompras.Rows.RemoveAt(row.Index);
                }

                // Calcular el nuevo total a pagar después de eliminar el Medicamento
                CalcularTotalPagar();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un Medicamento para quitar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            FrmPequeProveedor proveedorForm = new FrmPequeProveedor(this);
            proveedorForm.ShowDialog();
            HabilitarCamposProveedor();
        }

        private void btnBuscarMedicamento_Click(object sender, EventArgs e)
        {
            FrmPequeMedicamento MedicamentoFrm = new FrmPequeMedicamento(this);
            MedicamentoFrm.ShowDialog();
            HabilitarCamposMedicamentos();
        }

        private void DesactivarCamposProveedor()
        {
            txtDocuProveedor.Enabled = false;
            txtRazonSocial.Enabled = false;
        }
        private void HabilitarCamposProveedor()
        {
            txtDocuProveedor.Enabled = true;
            txtRazonSocial.Enabled = true;
        }
    }
}
