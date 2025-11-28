using CadFarmacia;
using ClnFarmacia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CpFarmacia
{
    public partial class FrmVentas : Form
    {
        private class MedicamentoComboItem
        {
            public int Id { get; set; }
            public string Descripcion { get; set; }
        }

        private DataTable dtDetalle;
        private decimal totalVenta = 0;

        public FrmVentas()
        {
            InitializeComponent();
        }

        private void FrmVentas_Load(object sender, EventArgs e)
        {
            CargarCombos();
            InicializarGrillaDetalle();
            MostrarUsuario();
            dtpFecha.Value = DateTime.Now;
        }

        private void MostrarUsuario()
        {
            if (UsuarioCln.UsuarioLogueado != null && UsuarioCln.UsuarioLogueado.Empleado != null)
            {
                lblUsuario.Text = UsuarioCln.UsuarioLogueado.Empleado.nombres + " " +
                                  UsuarioCln.UsuarioLogueado.Empleado.primerApellido;
            }
        }

        private void CargarCombos()
        {
            // Cargar Clientes
            var clientes = ClienteCln.ListarActivos();
            cboCliente.DataSource = clientes;
            cboCliente.DisplayMember = "nombres";
            cboCliente.ValueMember = "id";

            // Mostrar nombre completo
            cboCliente.DataSource = clientes.Select(c => new {
                id = c.id,
                nombreCompleto = c.nombres + " " + c.apellidos
            }).ToList();
            cboCliente.DisplayMember = "nombreCompleto";
            cboCliente.ValueMember = "id";
            cboCliente.SelectedIndex = -1;

            // Cargar Medicamentos
            CargarMedicamentos();
        }

        private void CargarMedicamentos(string parametro = null)
        {
            List<Medicamento> medicamentos;
            if (string.IsNullOrEmpty(parametro))
            {
                medicamentos = MedicamentoCln.ListarActivos();
            }
            else
            {
                medicamentos = MedicamentoCln.Listar(parametro);
            }

            cboMedicamento.DataSource = medicamentos.Select(m => new MedicamentoComboItem
            {
                Id = m.id,
                Descripcion = m.codigo + " - " + m.nombre + " (Stock: " + m.stock + ")"
            }).ToList();
            cboMedicamento.DisplayMember = "Descripcion";
            cboMedicamento.ValueMember = "Id";
            cboMedicamento.SelectedIndex = -1;

            txtPrecio.Clear();
            lblStock.Text = "0";
            chkReceta.Checked = false;
        }

        private void InicializarGrillaDetalle()
        {
            dtDetalle = new DataTable();
            dtDetalle.Columns.Add("idMedicamento", typeof(int));
            dtDetalle.Columns.Add("codigo", typeof(string));
            dtDetalle.Columns.Add("nombre", typeof(string));
            dtDetalle.Columns.Add("precioUnitario", typeof(decimal));
            dtDetalle.Columns.Add("cantidad", typeof(int));
            dtDetalle.Columns.Add("subtotal", typeof(decimal));

            dgvDetalle.DataSource = dtDetalle;
            FormatearGrillaDetalle();
        }

        private void FormatearGrillaDetalle()
        {
            if (dgvDetalle.Columns.Count > 0)
            {
                dgvDetalle.Columns["idMedicamento"].Visible = false;
                dgvDetalle.Columns["codigo"].HeaderText = "Código";
                dgvDetalle.Columns["codigo"].Width = 100;
                dgvDetalle.Columns["nombre"].HeaderText = "Medicamento";
                dgvDetalle.Columns["nombre"].Width = 300;
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

        private void cboMedicamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMedicamento.SelectedIndex >= 0 && cboMedicamento.SelectedItem != null)
            {
                MedicamentoComboItem selectedItem = (MedicamentoComboItem)cboMedicamento.SelectedItem;
                int idMedicamento = selectedItem.Id;
                var medicamento = MedicamentoCln.ObtenerPorId(idMedicamento);

                if (medicamento != null)
                {
                    txtPrecio.Text = medicamento.precioVenta.ToString("N2");
                    lblStock.Text = medicamento.stock.ToString();
                    chkReceta.Checked = medicamento.requiereReceta;
                    nudCantidad.Maximum = medicamento.stock;
                    nudCantidad.Value = 1;
                }
            }
            else
            {
                txtPrecio.Clear();
                lblStock.Text = "0";
                chkReceta.Checked = false;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (cboMedicamento.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un medicamento", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MedicamentoComboItem selectedItem = (MedicamentoComboItem)cboMedicamento.SelectedItem;
            int idMedicamento = selectedItem.Id;
            int cantidad = Convert.ToInt32(nudCantidad.Value);

            // Verificar si ya está en el detalle
            foreach (DataRow row in dtDetalle.Rows)
            {
                if (Convert.ToInt32(row["idMedicamento"]) == idMedicamento)
                {
                    MessageBox.Show("Este medicamento ya está agregado. Elimínelo primero si desea cambiar la cantidad.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Verificar stock
            var medicamento = MedicamentoCln.ObtenerPorId(idMedicamento);
            if (medicamento == null)
            {
                MessageBox.Show("Medicamento no encontrado", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Calcular stock disponible (restando lo ya agregado)
            int cantidadYaAgregada = 0;
            foreach (DataRow row in dtDetalle.Rows)
            {
                if (Convert.ToInt32(row["idMedicamento"]) == idMedicamento)
                {
                    cantidadYaAgregada += Convert.ToInt32(row["cantidad"]);
                }
            }

            if (cantidad > (medicamento.stock - cantidadYaAgregada))
            {
                MessageBox.Show("Stock insuficiente. Disponible: " + (medicamento.stock - cantidadYaAgregada),
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Agregar al detalle
            decimal precioUnitario = medicamento.precioVenta;
            decimal subtotal = precioUnitario * cantidad;

            DataRow nuevaFila = dtDetalle.NewRow();
            nuevaFila["idMedicamento"] = idMedicamento;
            nuevaFila["codigo"] = medicamento.codigo;
            nuevaFila["nombre"] = medicamento.nombre;
            nuevaFila["precioUnitario"] = precioUnitario;
            nuevaFila["cantidad"] = cantidad;
            nuevaFila["subtotal"] = subtotal;
            dtDetalle.Rows.Add(nuevaFila);

            // Actualizar total
            CalcularTotal();

            // Limpiar selección
            cboMedicamento.SelectedIndex = -1;
            nudCantidad.Value = 1;
        }

        private void CalcularTotal()
        {
            totalVenta = 0;
            foreach (DataRow row in dtDetalle.Rows)
            {
                totalVenta += Convert.ToDecimal(row["subtotal"]);
            }
            lblTotal.Text = "Bs. " + totalVenta.ToString("N2");
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un item para quitar", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Está seguro de quitar este item?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int index = dgvDetalle.CurrentRow.Index;
                dtDetalle.Rows.RemoveAt(index);
                CalcularTotal();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro de limpiar todo el detalle?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                dtDetalle.Rows.Clear();
                CalcularTotal();
                cboCliente.SelectedIndex = -1;
                cboMedicamento.SelectedIndex = -1;
            }
        }

        private void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            FrmAgregarClienteRapido frm = new FrmAgregarClienteRapido();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                // Recargar combo de clientes
                var clientes = ClienteCln.ListarActivos();
                cboCliente.DataSource = clientes.Select(c => new {
                    id = c.id,
                    nombreCompleto = c.nombres + " " + c.apellidos
                }).ToList();
                cboCliente.DisplayMember = "nombreCompleto";
                cboCliente.ValueMember = "id";

                // Seleccionar el nuevo cliente
                cboCliente.SelectedValue = frm.IdClienteCreado;
            }
        }

        private void btnGuardarVenta_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (cboCliente.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un cliente", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtDetalle.Rows.Count == 0)
            {
                MessageBox.Show("Agregue al menos un medicamento", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Está seguro de guardar esta venta?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Crear venta
                    Venta venta = new Venta
                    {
                        idCliente = Convert.ToInt32(cboCliente.SelectedValue),
                        idUsuario = UsuarioCln.UsuarioLogueado.id,
                        total = totalVenta,
                        fechaVenta = DateTime.Now,
                        estado = 1
                    };

                    // Crear lista de detalles
                    List<DetalleVenta> detalles = new List<DetalleVenta>();
                    foreach (DataRow row in dtDetalle.Rows)
                    {
                        DetalleVenta detalle = new DetalleVenta
                        {
                            idMedicamento = Convert.ToInt32(row["idMedicamento"]),
                            cantidad = Convert.ToInt32(row["cantidad"]),
                            precioUnitario = Convert.ToDecimal(row["precioUnitario"]),
                            estado = 1
                        };
                        detalles.Add(detalle);
                    }

                    // Guardar venta con detalles
                    int idVenta = VentaCln.Insertar(venta, detalles);

                    MessageBox.Show("Venta registrada correctamente.\nNúmero: FAC-" + idVenta,
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar formulario
                    dtDetalle.Rows.Clear();
                    CalcularTotal();
                    cboCliente.SelectedIndex = -1;
                    cboMedicamento.SelectedIndex = -1;
                    CargarMedicamentos(); // Recargar para actualizar stock

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar la venta: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}