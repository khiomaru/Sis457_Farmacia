using CadFarmacia;
using ClnFarmacia;
using System;
using System.Windows.Forms;

namespace CpFarmacia
{
    public partial class FrmLaboratorios : Form
    {
        private int idLaboratorioSeleccionado = 0;
        private bool esNuevo = false;

        public FrmLaboratorios()
        {
            InitializeComponent();
        }

        private void FrmLaboratorios_Load(object sender, EventArgs e)
        {
            Listar();
            EstadoInicial();
        }

        private void Listar()
        {
            dgvLaboratorios.DataSource = LaboratorioCln.Listar(txtBuscar.Text.Trim());
            FormatearGrilla();
        }

        private void FormatearGrilla()
        {
            if (dgvLaboratorios.Columns.Count > 0)
            {
                dgvLaboratorios.Columns["id"].Visible = false;
                dgvLaboratorios.Columns["nombre"].HeaderText = "Nombre";
                dgvLaboratorios.Columns["pais"].HeaderText = "País";
                dgvLaboratorios.Columns["estado"].HeaderText = "Estado";
            }
        }

        private void EstadoInicial()
        {
            txtNombre.Clear();
            txtPais.Clear();

            txtNombre.Enabled = false;
            txtPais.Enabled = false;

            btnGuardar.Enabled = false;
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
            btnNuevo.Enabled = true;

            idLaboratorioSeleccionado = 0;
            esNuevo = false;
        }

        private void HabilitarCampos()
        {
            txtNombre.Enabled = true;
            txtPais.Enabled = true;

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
            idLaboratorioSeleccionado = 0;
            txtNombre.Clear();
            txtPais.Clear();
            HabilitarCampos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese el nombre del laboratorio", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            try
            {
                if (esNuevo)
                {
                    Laboratorio laboratorio = new Laboratorio
                    {
                        nombre = txtNombre.Text.Trim(),
                        pais = txtPais.Text.Trim(),
                        estado = 1
                    };

                    LaboratorioCln.Insertar(laboratorio);
                    MessageBox.Show("Laboratorio registrado correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Laboratorio laboratorio = LaboratorioCln.ObtenerPorId(idLaboratorioSeleccionado);
                    laboratorio.nombre = txtNombre.Text.Trim();
                    laboratorio.pais = txtPais.Text.Trim();

                    LaboratorioCln.Actualizar(laboratorio);
                    MessageBox.Show("Laboratorio actualizado correctamente", "Éxito",
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
            if (idLaboratorioSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un laboratorio de la lista", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            esNuevo = false;
            HabilitarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idLaboratorioSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un laboratorio de la lista", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Está seguro de eliminar este laboratorio?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    LaboratorioCln.Eliminar(idLaboratorioSeleccionado);
                    MessageBox.Show("Laboratorio eliminado correctamente", "Éxito",
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

        private void dgvLaboratorios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvLaboratorios.Rows[e.RowIndex];

                idLaboratorioSeleccionado = Convert.ToInt32(fila.Cells["id"].Value);
                txtNombre.Text = fila.Cells["nombre"].Value.ToString();
                txtPais.Text = fila.Cells["pais"].Value?.ToString() ?? "";
            }
        }
    }
}