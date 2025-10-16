using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CpFarmacia2024
{
    public partial class FrmPaciente : Form
    {
        private bool esNuevo = false;
        private List<Paciente> listaPacientes = new List<Paciente>();

        public FrmPaciente()
        {
            InitializeComponent();
            DesactivarCampos();
            Listar();
        }

        // Clase interna simulando paciente
        public class Paciente
        {
            public int id { get; set; }
            public string nombre { get; set; }
            public string apellido { get; set; }
            public string documento { get; set; }
            public int edad { get; set; }
        }

        private void Listar()
        {
            dgvPacientes.DataSource = null;
            dgvPacientes.DataSource = listaPacientes;

            if (listaPacientes.Count > 0)
            {
                dgvPacientes.Columns["id"].Visible = false;
            }

            btnEditar.Enabled = listaPacientes.Count > 0;
            btnEliminar.Enabled = listaPacientes.Count > 0;
        }

        private void DesactivarCampos()
        {
            txtNombre.Enabled = false;
            txtApellido.Enabled = false;
            txtDocumento.Enabled = false;
            nudEdad.Enabled = false;
        }

        private void HabilitarCampos()
        {
            txtNombre.Enabled = true;
            txtApellido.Enabled = true;
            txtDocumento.Enabled = true;
            nudEdad.Enabled = true;
        }

        private bool Validar()
        {
            bool esValido = true;
            erpNombre.SetError(txtNombre, "");
            erpApellido.SetError(txtApellido, "");
            erpDocumento.SetError(txtDocumento, "");

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                esValido = false;
                erpNombre.SetError(txtNombre, "Nombre obligatorio");
            }
            if (string.IsNullOrEmpty(txtApellido.Text))
            {
                esValido = false;
                erpApellido.SetError(txtApellido, "Apellido obligatorio");
            }
            if (string.IsNullOrEmpty(txtDocumento.Text))
            {
                esValido = false;
                erpDocumento.SetError(txtDocumento, "Documento obligatorio");
            }

            return esValido;
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDocumento.Text = "";
            nudEdad.Value = 0;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            HabilitarCampos();
            LimpiarCampos();
            txtNombre.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Validar()) return;

            if (esNuevo)
            {
                Paciente p = new Paciente()
                {
                    id = listaPacientes.Count > 0 ? listaPacientes.Max(x => x.id) + 1 : 1,
                    nombre = txtNombre.Text.Trim(),
                    apellido = txtApellido.Text.Trim(),
                    documento = txtDocumento.Text.Trim(),
                    edad = (int)nudEdad.Value
                };
                listaPacientes.Add(p);
            }
            else
            {
                int index = dgvPacientes.CurrentCell.RowIndex;
                int id = listaPacientes[index].id;
                listaPacientes[index].nombre = txtNombre.Text.Trim();
                listaPacientes[index].apellido = txtApellido.Text.Trim();
                listaPacientes[index].documento = txtDocumento.Text.Trim();
                listaPacientes[index].edad = (int)nudEdad.Value;
            }

            Listar();
            LimpiarCampos();
            DesactivarCampos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvPacientes.CurrentRow == null) return;

            esNuevo = false;
            int index = dgvPacientes.CurrentCell.RowIndex;
            var p = listaPacientes[index];

            txtNombre.Text = p.nombre;
            txtApellido.Text = p.apellido;
            txtDocumento.Text = p.documento;
            nudEdad.Value = p.edad;

            HabilitarCampos();
            txtNombre.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPacientes.CurrentRow == null) return;

            int index = dgvPacientes.CurrentCell.RowIndex;
            var p = listaPacientes[index];

            if (MessageBox.Show($"¿Desea eliminar al paciente {p.nombre} {p.apellido}?", "Confirmar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                listaPacientes.RemoveAt(index);
                Listar();
            }
        }
    }
}
