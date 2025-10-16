namespace CpFarmacia2024
{
    partial class FrmPaciente
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.TextBox txtDocumento;
        private System.Windows.Forms.NumericUpDown nudEdad;
        private System.Windows.Forms.DataGridView dgvPacientes;
        private System.Windows.Forms.Button btnNuevo, btnGuardar, btnEditar, btnEliminar;
        private System.Windows.Forms.ErrorProvider erpNombre, erpApellido, erpDocumento;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.txtNombre = new System.Windows.Forms.TextBox() { Location = new System.Drawing.Point(20, 20), Width = 200 };
            this.txtApellido = new System.Windows.Forms.TextBox() { Location = new System.Drawing.Point(20, 60), Width = 200 };
            this.txtDocumento = new System.Windows.Forms.TextBox() { Location = new System.Drawing.Point(20, 100), Width = 200 };
            this.nudEdad = new System.Windows.Forms.NumericUpDown() { Location = new System.Drawing.Point(20, 140), Width = 80, Minimum = 0, Maximum = 120 };

            this.dgvPacientes = new System.Windows.Forms.DataGridView()
            {
                Location = new System.Drawing.Point(250, 20),
                Size = new System.Drawing.Size(500, 400),
                ReadOnly = true,
                SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            };

            this.btnNuevo = new System.Windows.Forms.Button() { Text = "Nuevo", Location = new System.Drawing.Point(20, 180) };
            this.btnGuardar = new System.Windows.Forms.Button() { Text = "Guardar", Location = new System.Drawing.Point(110, 180) };
            this.btnEditar = new System.Windows.Forms.Button() { Text = "Editar", Location = new System.Drawing.Point(20, 220) };
            this.btnEliminar = new System.Windows.Forms.Button() { Text = "Eliminar", Location = new System.Drawing.Point(110, 220) };

            this.erpNombre = new System.Windows.Forms.ErrorProvider(this.components);
            this.erpApellido = new System.Windows.Forms.ErrorProvider(this.components);
            this.erpDocumento = new System.Windows.Forms.ErrorProvider(this.components);

            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);

            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.txtApellido);
            this.Controls.Add(this.txtDocumento);
            this.Controls.Add(this.nudEdad);
            this.Controls.Add(this.dgvPacientes);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnEliminar);

            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Gestión de Pacientes";
        }
    }
}
