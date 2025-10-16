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
            CargarCategorias();
            Listar();
            DesactivarCampos();
        }

        private void CargarCategorias()
        {
            var categorias = CategoriaCln.listar() ?? new List<Categoria>();
            cbxCategoria.DataSource = categorias;
            cbxCategoria.DisplayMember = "descripcion";
            cbxCategoria.ValueMember = "id";
        }

        private void Listar()
        {
            var lista = MedicamentoCln.listaaPa(txtParametroMedicamento.Text) ?? new List<paMedicamentoListar_Result>();
            dgvListaMedicamento.DataSource = lista;

            if (lista.Count > 0)
            {
                dgvListaMedicamento.Columns["id"].Visible = false;
                dgvListaMedicamento.Columns["estado"].Visible = false;
                dgvListaMedicamento.Columns["codigo"].HeaderText = "Código";
                dgvListaMedicamento.Columns["nombre"].HeaderText = "Nombre";
                dgvListaMedicamento.Columns["descripcion"].HeaderText = "Descripción";
                dgvListaMedicamento.Columns["Categoria"].HeaderText = "Categoría";
                dgvListaMedicamento.Columns["stock"].HeaderText = "Stock";
                dgvListaMedicamento.Columns["precioCompra"].HeaderText = "Precio Compra";
                dgvListaMedicamento.Columns["precioVenta"].HeaderText = "Precio Venta";
            }

            btnEditar.Enabled = btnEliminar.Enabled = lista.Count > 0;
        }

        private bool Validar()
        {
            bool valido = true;
            erpCodigo.SetError(txtCodigo, "");
            erpNombre.SetError(txtNombre, "");
            erpDescripcion.SetError(txtDescripcion, "");

            if (string.IsNullOrWhiteSpace(txtCodigo.Text)) { erpCodigo.SetError(txtCodigo, "Código obligatorio"); valido = false; }
            if (string.IsNullOrWhiteSpace(txtNombre.Text)) { erpNombre.SetError(txtNombre, "Nombre obligatorio"); valido = false; }
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text)) { erpDescripcion.SetError(txtDescripcion, "Descripción obligatoria"); valido = false; }

            return valido;
        }

        private void LimpiarCampos()
        {
            txtCodigo.Text = txtNombre.Text = txtDescripcion.Text = "";
        }

        private void DesactivarCampos()
        {
            txtCodigo.Enabled = txtNombre.Enabled = txtDescripcion.Enabled = cbxCategoria.Enabled = false;
        }

        private void HabilitarCampos()
        {
            txtCodigo.Enabled = txtNombre.Enabled = txtDescripcion.Enabled = cbxCategoria.Enabled = true;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            HabilitarCampos();
            LimpiarCampos();
            txtCodigo.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Validar()) return;

            var medicamento = new Medicamento
            {
                codigo = txtCodigo.Text.Trim(),
                nombre = txtNombre.Text.Trim(),
                descripcion = txtDescripcion.Text.Trim(),
                idCategoria = (int)(cbxCategoria.SelectedValue ?? 0),
                stock = 0,
                precioCompra = 0,
                precioVenta = 0,
                usuarioRegistro = Util.usuario?.usuario1 ?? "Desconocido",
                fechaRegistro = DateTime.Now,
                estado = 1
            };

            try
            {
                if (esNuevo)
                {
                    if (MedicamentoCln.ExisteCodigo(medicamento.codigo))
                    {
                        MessageBox.Show("Código ya existente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    MedicamentoCln.insertar(medicamento);
                }
                else
                {
                    if (dgvListaMedicamento.CurrentCell == null) return;
                    int index = dgvListaMedicamento.CurrentCell.RowIndex;
                    int id = Convert.ToInt32(dgvListaMedicamento.Rows[index].Cells["id"].Value);
                    medicamento.id = id;
                    MedicamentoCln.actualizar(medicamento);
                }

                Listar();
                LimpiarCampos();
                DesactivarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvListaMedicamento.CurrentCell == null) return;

            esNuevo = false;
            int index = dgvListaMedicamento.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvListaMedicamento.Rows[index].Cells["id"].Value);
            var medicamento = MedicamentoCln.obtenerUno(id);

            txtCodigo.Text = medicamento.codigo;
            txtNombre.Text = medicamento.nombre;
            txtDescripcion.Text = medicamento.descripcion;
            cbxCategoria.SelectedValue = medicamento.idCategoria;

            HabilitarCampos();
            txtCodigo.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvListaMedicamento.CurrentCell == null) return;

            int index = dgvListaMedicamento.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvListaMedicamento.Rows[index].Cells["id"].Value);
            string codigo = dgvListaMedicamento.Rows[index].Cells["codigo"].Value.ToString();

            if (MessageBox.Show($"¿Dar de baja el medicamento {codigo}?", "Confirmar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                MedicamentoCln.eliminar(id, Util.usuario?.usuario1 ?? "Desconocido");
                Listar();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e) => Listar();
        private void btnLimpiar_Click(object sender, EventArgs e) { txtParametroMedicamento.Text = ""; Listar(); }
        private void btnCerrar_Click(object sender, EventArgs e) => this.Close();
    }
}
