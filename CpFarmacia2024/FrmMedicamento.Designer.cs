using System.Windows.Forms;

namespace CpFarmacia2024
{
    partial class FrmMedicamento
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtParametroMedicamento, txtCodigo, txtNombre, txtDescripcion;
        private ComboBox cbxCategoria;
        private DataGridView dgvListaMedicamento;
        private Button btnNuevo, btnGuardar, btnEditar, btnEliminar, btnCerrar, btnBuscar, btnLimpiar;
        private ErrorProvider erpCodigo, erpNombre, erpDescripcion;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtParametroMedicamento = new TextBox();
            this.txtCodigo = new TextBox();
            this.txtNombre = new TextBox();
            this.txtDescripcion = new TextBox();
            this.cbxCategoria = new ComboBox();
            this.dgvListaMedicamento = new DataGridView();
            this.btnNuevo = new Button();
            this.btnGuardar = new Button();
            this.btnEditar = new Button();
            this.btnEliminar = new Button();
            this.btnCerrar = new Button();
            this.btnBuscar = new Button();
            this.btnLimpiar = new Button();
            this.erpCodigo = new ErrorProvider(this.components);
            this.erpNombre = new ErrorProvider(this.components);
            this.erpDescripcion = new ErrorProvider(this.components);

            // Form
            this.Text = "Gestión de Medicamentos";
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Aquí puedes definir la ubicación y tamaño de los controles si quieres ajustarlos manualmente
            // Ejemplo:
            txtParametroMedicamento.Location = new System.Drawing.Point(20, 20);
            txtParametroMedicamento.Width = 200;
            btnBuscar.Location = new System.Drawing.Point(230, 18);
            btnBuscar.Text = "Buscar";
            btnLimpiar.Location = new System.Drawing.Point(320, 18);
            btnLimpiar.Text = "Limpiar";

            // Botones principales
            btnNuevo.Text = "Nuevo"; btnNuevo.Location = new System.Drawing.Point(20, 250);
            btnGuardar.Text = "Guardar"; btnGuardar.Location = new System.Drawing.Point(110, 250);
            btnEditar.Text = "Editar"; btnEditar.Location = new System.Drawing.Point(20, 290);
            btnEliminar.Text = "Eliminar"; btnEliminar.Location = new System.Drawing.Point(110, 290);
            btnCerrar.Text = "Cerrar"; btnCerrar.Location = new System.Drawing.Point(20, 330);
            dgvListaMedicamento.Location = new System.Drawing.Point(270, 55);
            dgvListaMedicamento.Size = new System.Drawing.Size(600, 450);
            dgvListaMedicamento.ReadOnly = true;
            dgvListaMedicamento.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvListaMedicamento.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Agregar controles al formulario
            this.Controls.Add(txtParametroMedicamento);
            this.Controls.Add(txtCodigo);
            this.Controls.Add(txtNombre);
            this.Controls.Add(txtDescripcion);
            this.Controls.Add(cbxCategoria);
            this.Controls.Add(dgvListaMedicamento);
            this.Controls.Add(btnNuevo);
            this.Controls.Add(btnGuardar);
            this.Controls.Add(btnEditar);
            this.Controls.Add(btnEliminar);
            this.Controls.Add(btnCerrar);
            this.Controls.Add(btnBuscar);
            this.Controls.Add(btnLimpiar);
        }
    }
}
