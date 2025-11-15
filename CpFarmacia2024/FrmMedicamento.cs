using CadFarmacia2024;
using ClnFarmacia2024;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CpFarmacia2024
{
    public partial class FrmMedicamento : Form
    {
        private bool esNuevo = false;

        public FrmMedicamento()
        {
            InitializeComponent();
            CargarEstados();
            DesactivarCampos();
            Listar();
        }

        private void CargarEstados()
        {
            cboEstado.Items.Clear();
            cboEstado.Items.Add("Activo");
            cboEstado.Items.Add("Inactivo");
            cboEstado.SelectedIndex = 0;
        }

        private void Listar()
        {
            var filtro = txtBuscar.Text.Trim();
            var lista = MedicamentoCln.listar(filtro) ?? new List<Medicamento>();

            dgvMedicamentos.DataSource = lista;

            if (lista.Count > 0)
            {
                if (dgvMedicamentos.Columns.Contains("id")) dgvMedicamentos.Columns["id"].Visible = false;
                if (dgvMedicamentos.Columns.Contains("estado")) dgvMedicamentos.Columns["estado"].Visible = false;
                if (dgvMedicamentos.Columns.Contains("fechaRegistro")) dgvMedicamentos.Columns["fechaRegistro"].Visible = false;
                if (dgvMedicamentos.Columns.Contains("usuarioRegistro")) dgvMedicamentos.Columns["usuarioRegistro"].Visible = false;
            }

            btnEditar.Enabled = btnEliminar.Enabled = lista.Count > 0;
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            cboEstado.SelectedIndex = 0;
        }

        private void DesactivarCampos()
        {
            txtNombre.Enabled = false;
            txtDescripcion.Enabled = false;
            txtPrecio.Enabled = false;
            txtStock.Enabled = false;
            cboEstado.Enabled = false;
        }

        private void HabilitarCampos()
        {
            txtNombre.Enabled = true;
            txtDescripcion.Enabled = true;
            txtPrecio.Enabled = true;
            txtStock.Enabled = true;
            cboEstado.Enabled = true;
        }

        private bool Validar()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPrecio.Text, out _))
            {
                MessageBox.Show("El precio debe ser un número válido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Focus();
                return false;
            }

            if (!int.TryParse(txtStock.Text, out _))
            {
                MessageBox.Show("El stock debe ser un número entero.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStock.Focus();
                return false;
            }

            return true;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            LimpiarCampos();
            HabilitarCampos();
            txtNombre.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Validar()) return;

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                MessageBox.Show("Precio inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtStock.Text, out int stock))
            {
                MessageBox.Show("Stock inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var obj = new Medicamento
            {
                nombre = txtNombre.Text.Trim(),
                descripcion = txtDescripcion.Text.Trim(),
                precioVenta = precio,
                stock = stock,
                estado = (short)EstadoToInt(cboEstado.Text),
                fechaRegistro = DateTime.Now,
                usuarioRegistro = "admin"
            };

            try
            {
                if (esNuevo)
                {
                    MedicamentoCln.insertar(obj);
                    MessageBox.Show("Medicamento registrado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (dgvMedicamentos.CurrentRow == null)
                    {
                        MessageBox.Show("Seleccione un registro para actualizar.", "Atención",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int id = Convert.ToInt32(dgvMedicamentos.CurrentRow.Cells["id"].Value);
                    obj.id = id;

                    MedicamentoCln.actualizar(obj);
                    MessageBox.Show("Medicamento actualizado.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Listar();
            LimpiarCampos();
            DesactivarCampos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvMedicamentos.CurrentRow == null) return;

            esNuevo = false;
            HabilitarCampos();

            var row = dgvMedicamentos.CurrentRow;
            txtNombre.Text = row.Cells["nombre"].Value?.ToString() ?? string.Empty;
            txtDescripcion.Text = row.Cells["descripcion"].Value?.ToString() ?? string.Empty;
            txtPrecio.Text = row.Cells["precioVenta"].Value?.ToString() ?? string.Empty;
            txtStock.Text = row.Cells["stock"].Value?.ToString() ?? string.Empty;

            cboEstado.SelectedItem = IntToEstado(row.Cells["estado"].Value);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvMedicamentos.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvMedicamentos.CurrentRow.Cells["id"].Value);

            if (MessageBox.Show("¿Realmente deseas eliminar este medicamento?",
                "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    MedicamentoCln.eliminar(id, "admin");
                    Listar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Listar();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Listar();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private int EstadoToInt(string estado)
        {
            return string.Equals(estado, "Activo", StringComparison.OrdinalIgnoreCase) ? 1 : 0;
        }

        private string IntToEstado(object value)
        {
            if (value == null) return "Inactivo";
            int v;
            if (int.TryParse(value.ToString(), out v))
            {
                return v == 1 ? "Activo" : "Inactivo";
            }
            // si viene como texto
            return value.ToString() == "1" ? "Activo" : "Inactivo";
        }
    }
}
