using CadFarmacia;
using ClnFarmacia;
using System;
using System.Windows.Forms;

namespace CpFarmacia
{
    public partial class FrmCategorias : Form
    {
        private int idCategoriaSeleccionada = 0;
        private bool esNuevo = false;

        public FrmCategorias()
        {
            InitializeComponent();
        }

        private void FrmCategorias_Load(object sender, EventArgs e)
        {
            Listar();
            EstadoInicial();
        }

        private void Listar()
        {
            dgvCategorias.DataSource = CategoriaCln.Listar();
            FormatearGrilla();
        }

        private void FormatearGrilla()
        {
            if (dgvCategorias.Columns.Count > 0)
            {
                dgvCategorias.Columns["id"].Visible = false;
                dgvCategorias.Columns["nombre"].HeaderText = "Nombre";
                dgvCategorias.Columns["descripcion"].HeaderText = "Descripción";
                dgvCategorias.Columns["estado"].HeaderText = "Estado";
                dgvCategorias.Columns["Medicamento"].Visible = false;
            }
        }

        private void EstadoInicial()
        {
            txtNombre.Clear();
            txtDescripcion.Clear();

            txtNombre.Enabled = false;
            txtDescripcion.Enabled = false;

            btnGuardar.Enabled = false;
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
            btnNuevo.Enabled = true;

            idCategoriaSeleccionada = 0;
            esNuevo = false;
        }

        private void HabilitarCampos()
        {
            txtNombre.Enabled = true;
            txtDescripcion.Enabled = true;

            btnGuardar.Enabled = true;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;

            txtNombre.Focus();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Listar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            idCategoriaSeleccionada = 0;
            txtNombre.Clear();
            txtDescripcion.Clear();
            HabilitarCampos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese el nombre de la categoría", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            try
            {
                if (esNuevo)
                {
                    Categoria categoria = new Categoria
                    {
                        nombre = txtNombre.Text.Trim(),
                        descripcion = txtDescripcion.Text.Trim(),
                        estado = 1
                    };

                    CategoriaCln.Insertar(categoria);
                    MessageBox.Show("Categoría registrada correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Categoria categoria = CategoriaCln.ObtenerPorId(idCategoriaSeleccionada);
                    categoria.nombre = txtNombre.Text.Trim();
                    categoria.descripcion = txtDescripcion.Text.Trim();

                    CategoriaCln.Actualizar(categoria);
                    MessageBox.Show("Categoría actualizada correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Listar();
                EstadoInicial();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idCategoriaSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una categoría de la lista", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            esNuevo = false;
            HabilitarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idCategoriaSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una categoría de la lista", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Está seguro de eliminar esta categoría?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    CategoriaCln.Eliminar(idCategoriaSeleccionada);
                    MessageBox.Show("Categoría eliminada correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Listar();
                    EstadoInicial();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvCategorias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvCategorias.Rows[e.RowIndex];

                idCategoriaSeleccionada = Convert.ToInt32(fila.Cells["id"].Value);
                txtNombre.Text = fila.Cells["nombre"].Value.ToString();
                txtDescripcion.Text = fila.Cells["descripcion"].Value?.ToString() ?? "";
            }
        }
    }
}