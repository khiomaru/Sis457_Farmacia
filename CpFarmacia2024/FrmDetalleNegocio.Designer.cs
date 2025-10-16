namespace CpFarmacia2024
{
    partial class FrmDetalleNegocio
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDetalleNegocio));
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblNombreNegocio = new System.Windows.Forms.Label();
            this.lblNit = new System.Windows.Forms.Label();
            this.txtNit = new System.Windows.Forms.TextBox();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.btnMostrarDatos = new System.Windows.Forms.Button();
            this.gbxDetalleNegocio = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbxDetalleNegocio.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNombre
            // 
            this.txtNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.Location = new System.Drawing.Point(186, 65);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(217, 29);
            this.txtNombre.TabIndex = 23;
            // 
            // lblNombreNegocio
            // 
            this.lblNombreNegocio.AutoSize = true;
            this.lblNombreNegocio.BackColor = System.Drawing.Color.DimGray;
            this.lblNombreNegocio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreNegocio.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblNombreNegocio.Location = new System.Drawing.Point(183, 43);
            this.lblNombreNegocio.Name = "lblNombreNegocio";
            this.lblNombreNegocio.Size = new System.Drawing.Size(153, 20);
            this.lblNombreNegocio.TabIndex = 22;
            this.lblNombreNegocio.Text = "Nombre de Negocio:";
            // 
            // lblNit
            // 
            this.lblNit.AutoSize = true;
            this.lblNit.BackColor = System.Drawing.Color.DimGray;
            this.lblNit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNit.ForeColor = System.Drawing.Color.Black;
            this.lblNit.Location = new System.Drawing.Point(183, 100);
            this.lblNit.Name = "lblNit";
            this.lblNit.Size = new System.Drawing.Size(32, 20);
            this.lblNit.TabIndex = 24;
            this.lblNit.Text = "Nit:";
            // 
            // txtNit
            // 
            this.txtNit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNit.Location = new System.Drawing.Point(186, 122);
            this.txtNit.Name = "txtNit";
            this.txtNit.Size = new System.Drawing.Size(211, 29);
            this.txtNit.TabIndex = 29;
            // 
            // txtDireccion
            // 
            this.txtDireccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDireccion.Location = new System.Drawing.Point(190, 184);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(295, 29);
            this.txtDireccion.TabIndex = 30;
            // 
            // btnMostrarDatos
            // 
            this.btnMostrarDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMostrarDatos.ForeColor = System.Drawing.Color.Black;
            this.btnMostrarDatos.Location = new System.Drawing.Point(257, 229);
            this.btnMostrarDatos.Name = "btnMostrarDatos";
            this.btnMostrarDatos.Size = new System.Drawing.Size(143, 29);
            this.btnMostrarDatos.TabIndex = 31;
            this.btnMostrarDatos.Text = "Mostrar Datos";
            this.btnMostrarDatos.UseVisualStyleBackColor = true;
            this.btnMostrarDatos.Click += new System.EventHandler(this.btnMostrarDatos_Click);
            // 
            // gbxDetalleNegocio
            // 
            this.gbxDetalleNegocio.BackColor = System.Drawing.Color.Transparent;
            this.gbxDetalleNegocio.Controls.Add(this.label1);
            this.gbxDetalleNegocio.Controls.Add(this.btnMostrarDatos);
            this.gbxDetalleNegocio.Controls.Add(this.txtDireccion);
            this.gbxDetalleNegocio.Controls.Add(this.txtNit);
            this.gbxDetalleNegocio.Controls.Add(this.lblNit);
            this.gbxDetalleNegocio.Controls.Add(this.lblNombreNegocio);
            this.gbxDetalleNegocio.Controls.Add(this.txtNombre);
            this.gbxDetalleNegocio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxDetalleNegocio.ForeColor = System.Drawing.Color.Black;
            this.gbxDetalleNegocio.Location = new System.Drawing.Point(12, 12);
            this.gbxDetalleNegocio.Name = "gbxDetalleNegocio";
            this.gbxDetalleNegocio.Size = new System.Drawing.Size(538, 325);
            this.gbxDetalleNegocio.TabIndex = 1;
            this.gbxDetalleNegocio.TabStop = false;
            this.gbxDetalleNegocio.Text = "Detalle del Negocio";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Gray;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(186, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 32;
            this.label1.Text = "Dirección:";
            // 
            // FrmDetalleNegocio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImage = global::CpFarmacia2024.Properties.Resources.descargar3;
            this.ClientSize = new System.Drawing.Size(566, 347);
            this.Controls.Add(this.gbxDetalleNegocio);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmDetalleNegocio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "::: Farmacia - DetalleNegocio :::";
            this.Load += new System.EventHandler(this.FrmDetalleNegocio_Load);
            this.gbxDetalleNegocio.ResumeLayout(false);
            this.gbxDetalleNegocio.PerformLayout();
            this.ResumeLayout(false);

        }

		#endregion
		private System.Windows.Forms.TextBox txtNombre;
		private System.Windows.Forms.Label lblNombreNegocio;
		private System.Windows.Forms.Label lblNit;
		private System.Windows.Forms.TextBox txtNit;
		private System.Windows.Forms.TextBox txtDireccion;
		private System.Windows.Forms.Button btnMostrarDatos;
		private System.Windows.Forms.GroupBox gbxDetalleNegocio;
		private System.Windows.Forms.Label label1;
	}
}








