namespace CpFarmacia2024
{
    partial class FrmVenta
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
            this.components = new System.ComponentModel.Container();
            this.dgvCompras = new System.Windows.Forms.DataGridView();
            this.erpTipoDocumento = new System.Windows.Forms.ErrorProvider(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.gbxInfoCompra = new System.Windows.Forms.GroupBox();
            this.txtNfactura = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxTipoDocumento = new System.Windows.Forms.ComboBox();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.gbxInfoMedicamento = new System.Windows.Forms.GroupBox();
            this.btnBuscarMedicamento = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.lblPrecioVenta = new System.Windows.Forms.Label();
            this.txtPrecioVenta = new System.Windows.Forms.TextBox();
            this.lblPrecioCompra = new System.Windows.Forms.Label();
            this.txtPrecioCompra = new System.Windows.Forms.TextBox();
            this.lblMedicamento = new System.Windows.Forms.Label();
            this.txtMedicamento = new System.Windows.Forms.TextBox();
            this.txtIdMedicamento = new System.Windows.Forms.TextBox();
            this.lblCodigoMedicamento = new System.Windows.Forms.Label();
            this.txtCodigoMedicamento = new System.Windows.Forms.TextBox();
            this.iBtnRegistrar = new FontAwesome.Sharp.IconButton();
            this.lblTotalPagar = new System.Windows.Forms.Label();
            this.txtTotalPagar = new System.Windows.Forms.TextBox();
            this.ibtnAgregar = new FontAwesome.Sharp.IconButton();
            this.iBtnQuitar = new FontAwesome.Sharp.IconButton();
            this.label4 = new System.Windows.Forms.Label();
            this.btnBuscarProveedor = new System.Windows.Forms.Button();
            this.gbxInformacionProveedor = new System.Windows.Forms.GroupBox();
            this.txtIdProveedor = new System.Windows.Forms.TextBox();
            this.lblRazonSocial = new System.Windows.Forms.Label();
            this.txtRazonSocial = new System.Windows.Forms.TextBox();
            this.txtDocuProveedor = new System.Windows.Forms.TextBox();
            this.erpCantidad = new System.Windows.Forms.ErrorProvider(this.components);
            this.erpDocuProveedor = new System.Windows.Forms.ErrorProvider(this.components);
            this.erpPrecioVenta = new System.Windows.Forms.ErrorProvider(this.components);
            this.erpPrecioCompra = new System.Windows.Forms.ErrorProvider(this.components);
            this.erpCodigoMedicamento = new System.Windows.Forms.ErrorProvider(this.components);
            this.erpNfactura = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpTipoDocumento)).BeginInit();
            this.gbxInfoCompra.SuspendLayout();
            this.gbxInfoMedicamento.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            this.gbxInformacionProveedor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erpCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpDocuProveedor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpPrecioVenta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpPrecioCompra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpCodigoMedicamento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpNfactura)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCompras
            // 
            this.dgvCompras.AllowUserToAddRows = false;
            this.dgvCompras.AllowUserToDeleteRows = false;
            this.dgvCompras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCompras.Location = new System.Drawing.Point(15, 271);
            this.dgvCompras.Name = "dgvCompras";
            this.dgvCompras.ReadOnly = true;
            this.dgvCompras.RowTemplate.Height = 28;
            this.dgvCompras.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCompras.Size = new System.Drawing.Size(772, 154);
            this.dgvCompras.TabIndex = 13;
            // 
            // erpTipoDocumento
            // 
            this.erpTipoDocumento.ContainerControl = this;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Lime;
            this.label2.Location = new System.Drawing.Point(11, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "Registrar Venta";
            // 
            // gbxInfoCompra
            // 
            this.gbxInfoCompra.BackColor = System.Drawing.Color.Transparent;
            this.gbxInfoCompra.Controls.Add(this.txtNfactura);
            this.gbxInfoCompra.Controls.Add(this.label6);
            this.gbxInfoCompra.Controls.Add(this.cbxTipoDocumento);
            this.gbxInfoCompra.Controls.Add(this.txtFecha);
            this.gbxInfoCompra.Controls.Add(this.label3);
            this.gbxInfoCompra.Controls.Add(this.lblFecha);
            this.gbxInfoCompra.ForeColor = System.Drawing.Color.Aqua;
            this.gbxInfoCompra.Location = new System.Drawing.Point(15, 45);
            this.gbxInfoCompra.Name = "gbxInfoCompra";
            this.gbxInfoCompra.Size = new System.Drawing.Size(382, 93);
            this.gbxInfoCompra.TabIndex = 3;
            this.gbxInfoCompra.TabStop = false;
            this.gbxInfoCompra.Text = "Información Venta";
            // 
            // txtNfactura
            // 
            this.txtNfactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNfactura.Location = new System.Drawing.Point(250, 48);
            this.txtNfactura.Name = "txtNfactura";
            this.txtNfactura.Size = new System.Drawing.Size(119, 29);
            this.txtNfactura.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(241, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 20);
            this.label6.TabIndex = 4;
            this.label6.Text = "N° Factura/Boleta";
            // 
            // cbxTipoDocumento
            // 
            this.cbxTipoDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxTipoDocumento.FormattingEnabled = true;
            this.cbxTipoDocumento.Items.AddRange(new object[] {
            "Factura",
            "Boleta"});
            this.cbxTipoDocumento.Location = new System.Drawing.Point(112, 45);
            this.cbxTipoDocumento.Name = "cbxTipoDocumento";
            this.cbxTipoDocumento.Size = new System.Drawing.Size(119, 32);
            this.cbxTipoDocumento.TabIndex = 3;
            // 
            // txtFecha
            // 
            this.txtFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFecha.Location = new System.Drawing.Point(6, 45);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(96, 29);
            this.txtFecha.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(107, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Tipo Documento:";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(6, 24);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(58, 20);
            this.lblFecha.TabIndex = 0;
            this.lblFecha.Text = "Fecha:";
            // 
            // gbxInfoMedicamento
            // 
            this.gbxInfoMedicamento.BackColor = System.Drawing.Color.Transparent;
            this.gbxInfoMedicamento.Controls.Add(this.btnBuscarMedicamento);
            this.gbxInfoMedicamento.Controls.Add(this.label5);
            this.gbxInfoMedicamento.Controls.Add(this.nudCantidad);
            this.gbxInfoMedicamento.Controls.Add(this.lblPrecioVenta);
            this.gbxInfoMedicamento.Controls.Add(this.txtPrecioVenta);
            this.gbxInfoMedicamento.Controls.Add(this.lblPrecioCompra);
            this.gbxInfoMedicamento.Controls.Add(this.txtPrecioCompra);
            this.gbxInfoMedicamento.Controls.Add(this.lblMedicamento);
            this.gbxInfoMedicamento.Controls.Add(this.txtMedicamento);
            this.gbxInfoMedicamento.Controls.Add(this.txtIdMedicamento);
            this.gbxInfoMedicamento.Controls.Add(this.lblCodigoMedicamento);
            this.gbxInfoMedicamento.Controls.Add(this.txtCodigoMedicamento);
            this.gbxInfoMedicamento.ForeColor = System.Drawing.Color.Fuchsia;
            this.gbxInfoMedicamento.Location = new System.Drawing.Point(15, 152);
            this.gbxInfoMedicamento.Name = "gbxInfoMedicamento";
            this.gbxInfoMedicamento.Size = new System.Drawing.Size(772, 108);
            this.gbxInfoMedicamento.TabIndex = 5;
            this.gbxInfoMedicamento.TabStop = false;
            this.gbxInfoMedicamento.Text = "Información de Medicamento";
            // 
            // btnBuscarMedicamento
            // 
            this.btnBuscarMedicamento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarMedicamento.ForeColor = System.Drawing.Color.Black;
            this.btnBuscarMedicamento.Image = global::CpFarmacia2024.Properties.Resources.find_search_locate_95724;
            this.btnBuscarMedicamento.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscarMedicamento.Location = new System.Drawing.Point(6, 35);
            this.btnBuscarMedicamento.Name = "btnBuscarMedicamento";
            this.btnBuscarMedicamento.Size = new System.Drawing.Size(122, 59);
            this.btnBuscarMedicamento.TabIndex = 18;
            this.btnBuscarMedicamento.Text = "Buscar:";
            this.btnBuscarMedicamento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscarMedicamento.UseVisualStyleBackColor = true;
            this.btnBuscarMedicamento.Click += new System.EventHandler(this.btnBuscarMedicamento_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(669, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 20);
            this.label5.TabIndex = 16;
            this.label5.Text = "Cantidad:";
            // 
            // nudCantidad
            // 
            this.nudCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudCantidad.Location = new System.Drawing.Point(671, 65);
            this.nudCantidad.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Size = new System.Drawing.Size(82, 29);
            this.nudCantidad.TabIndex = 15;
            // 
            // lblPrecioVenta
            // 
            this.lblPrecioVenta.AutoSize = true;
            this.lblPrecioVenta.Location = new System.Drawing.Point(550, 42);
            this.lblPrecioVenta.Name = "lblPrecioVenta";
            this.lblPrecioVenta.Size = new System.Drawing.Size(104, 20);
            this.lblPrecioVenta.TabIndex = 13;
            this.lblPrecioVenta.Text = "Precio Venta:";
            // 
            // txtPrecioVenta
            // 
            this.txtPrecioVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecioVenta.Location = new System.Drawing.Point(554, 64);
            this.txtPrecioVenta.Name = "txtPrecioVenta";
            this.txtPrecioVenta.Size = new System.Drawing.Size(98, 29);
            this.txtPrecioVenta.TabIndex = 14;
            // 
            // lblPrecioCompra
            // 
            this.lblPrecioCompra.AutoSize = true;
            this.lblPrecioCompra.Location = new System.Drawing.Point(435, 41);
            this.lblPrecioCompra.Name = "lblPrecioCompra";
            this.lblPrecioCompra.Size = new System.Drawing.Size(117, 20);
            this.lblPrecioCompra.TabIndex = 11;
            this.lblPrecioCompra.Text = "Precio Compra:";
            // 
            // txtPrecioCompra
            // 
            this.txtPrecioCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecioCompra.Location = new System.Drawing.Point(440, 65);
            this.txtPrecioCompra.Name = "txtPrecioCompra";
            this.txtPrecioCompra.Size = new System.Drawing.Size(96, 29);
            this.txtPrecioCompra.TabIndex = 12;
            // 
            // lblMedicamento
            // 
            this.lblMedicamento.AutoSize = true;
            this.lblMedicamento.ForeColor = System.Drawing.Color.Magenta;
            this.lblMedicamento.Location = new System.Drawing.Point(226, 42);
            this.lblMedicamento.Name = "lblMedicamento";
            this.lblMedicamento.Size = new System.Drawing.Size(109, 20);
            this.lblMedicamento.TabIndex = 10;
            this.lblMedicamento.Text = "Medicamento:";
            // 
            // txtMedicamento
            // 
            this.txtMedicamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMedicamento.Location = new System.Drawing.Point(228, 65);
            this.txtMedicamento.Name = "txtMedicamento";
            this.txtMedicamento.Size = new System.Drawing.Size(206, 29);
            this.txtMedicamento.TabIndex = 10;
            // 
            // txtIdMedicamento
            // 
            this.txtIdMedicamento.Enabled = false;
            this.txtIdMedicamento.Location = new System.Drawing.Point(394, 35);
            this.txtIdMedicamento.Name = "txtIdMedicamento";
            this.txtIdMedicamento.Size = new System.Drawing.Size(40, 26);
            this.txtIdMedicamento.TabIndex = 10;
            // 
            // lblCodigoMedicamento
            // 
            this.lblCodigoMedicamento.AutoSize = true;
            this.lblCodigoMedicamento.ForeColor = System.Drawing.Color.Magenta;
            this.lblCodigoMedicamento.Location = new System.Drawing.Point(135, 41);
            this.lblCodigoMedicamento.Name = "lblCodigoMedicamento";
            this.lblCodigoMedicamento.Size = new System.Drawing.Size(63, 20);
            this.lblCodigoMedicamento.TabIndex = 10;
            this.lblCodigoMedicamento.Text = "Codigo:";
            // 
            // txtCodigoMedicamento
            // 
            this.txtCodigoMedicamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoMedicamento.Location = new System.Drawing.Point(135, 65);
            this.txtCodigoMedicamento.Name = "txtCodigoMedicamento";
            this.txtCodigoMedicamento.Size = new System.Drawing.Size(83, 29);
            this.txtCodigoMedicamento.TabIndex = 10;
            // 
            // iBtnRegistrar
            // 
            this.iBtnRegistrar.IconChar = FontAwesome.Sharp.IconChar.Store;
            this.iBtnRegistrar.IconColor = System.Drawing.Color.MediumTurquoise;
            this.iBtnRegistrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iBtnRegistrar.IconSize = 30;
            this.iBtnRegistrar.Location = new System.Drawing.Point(793, 370);
            this.iBtnRegistrar.Name = "iBtnRegistrar";
            this.iBtnRegistrar.Size = new System.Drawing.Size(122, 55);
            this.iBtnRegistrar.TabIndex = 12;
            this.iBtnRegistrar.Text = "Regístrar";
            this.iBtnRegistrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iBtnRegistrar.UseVisualStyleBackColor = true;
            this.iBtnRegistrar.Click += new System.EventHandler(this.iBtnRegistrar_Click);
            // 
            // lblTotalPagar
            // 
            this.lblTotalPagar.AutoSize = true;
            this.lblTotalPagar.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalPagar.ForeColor = System.Drawing.Color.Fuchsia;
            this.lblTotalPagar.Location = new System.Drawing.Point(799, 310);
            this.lblTotalPagar.Name = "lblTotalPagar";
            this.lblTotalPagar.Size = new System.Drawing.Size(107, 20);
            this.lblTotalPagar.TabIndex = 10;
            this.lblTotalPagar.Text = "Total a Pagar:";
            this.lblTotalPagar.Click += new System.EventHandler(this.lblTotalPagar_Click);
            // 
            // txtTotalPagar
            // 
            this.txtTotalPagar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalPagar.Location = new System.Drawing.Point(811, 334);
            this.txtTotalPagar.Name = "txtTotalPagar";
            this.txtTotalPagar.Size = new System.Drawing.Size(87, 29);
            this.txtTotalPagar.TabIndex = 11;
            this.txtTotalPagar.TextChanged += new System.EventHandler(this.txtTotalPagar_TextChanged);
            // 
            // ibtnAgregar
            // 
            this.ibtnAgregar.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.ibtnAgregar.IconColor = System.Drawing.Color.ForestGreen;
            this.ibtnAgregar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ibtnAgregar.Location = new System.Drawing.Point(811, 156);
            this.ibtnAgregar.Name = "ibtnAgregar";
            this.ibtnAgregar.Size = new System.Drawing.Size(102, 85);
            this.ibtnAgregar.TabIndex = 7;
            this.ibtnAgregar.Text = "Agregar";
            this.ibtnAgregar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ibtnAgregar.UseVisualStyleBackColor = true;
            this.ibtnAgregar.Click += new System.EventHandler(this.ibtnAgregar_Click);
            // 
            // iBtnQuitar
            // 
            this.iBtnQuitar.IconChar = FontAwesome.Sharp.IconChar.Remove;
            this.iBtnQuitar.IconColor = System.Drawing.Color.Crimson;
            this.iBtnQuitar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iBtnQuitar.Location = new System.Drawing.Point(811, 247);
            this.iBtnQuitar.Name = "iBtnQuitar";
            this.iBtnQuitar.Size = new System.Drawing.Size(103, 60);
            this.iBtnQuitar.TabIndex = 14;
            this.iBtnQuitar.Text = "Quitar";
            this.iBtnQuitar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.iBtnQuitar.UseVisualStyleBackColor = true;
            this.iBtnQuitar.Click += new System.EventHandler(this.iBtnQuitar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(129, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "N° Documento:";
            // 
            // btnBuscarProveedor
            // 
            this.btnBuscarProveedor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarProveedor.ForeColor = System.Drawing.Color.Black;
            this.btnBuscarProveedor.Image = global::CpFarmacia2024.Properties.Resources.find_search_locate_95725;
            this.btnBuscarProveedor.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscarProveedor.Location = new System.Drawing.Point(6, 26);
            this.btnBuscarProveedor.Name = "btnBuscarProveedor";
            this.btnBuscarProveedor.Size = new System.Drawing.Size(117, 60);
            this.btnBuscarProveedor.TabIndex = 17;
            this.btnBuscarProveedor.Text = "Buscar:";
            this.btnBuscarProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscarProveedor.UseVisualStyleBackColor = true;
            this.btnBuscarProveedor.Click += new System.EventHandler(this.btnBuscarProveedor_Click);
            // 
            // gbxInformacionProveedor
            // 
            this.gbxInformacionProveedor.BackColor = System.Drawing.Color.Transparent;
            this.gbxInformacionProveedor.Controls.Add(this.btnBuscarProveedor);
            this.gbxInformacionProveedor.Controls.Add(this.txtIdProveedor);
            this.gbxInformacionProveedor.Controls.Add(this.lblRazonSocial);
            this.gbxInformacionProveedor.Controls.Add(this.txtRazonSocial);
            this.gbxInformacionProveedor.Controls.Add(this.txtDocuProveedor);
            this.gbxInformacionProveedor.Controls.Add(this.label4);
            this.gbxInformacionProveedor.ForeColor = System.Drawing.Color.Lime;
            this.gbxInformacionProveedor.Location = new System.Drawing.Point(409, 45);
            this.gbxInformacionProveedor.Name = "gbxInformacionProveedor";
            this.gbxInformacionProveedor.Size = new System.Drawing.Size(505, 93);
            this.gbxInformacionProveedor.TabIndex = 4;
            this.gbxInformacionProveedor.TabStop = false;
            this.gbxInformacionProveedor.Text = "Información Proveedor";
            // 
            // txtIdProveedor
            // 
            this.txtIdProveedor.Enabled = false;
            this.txtIdProveedor.Location = new System.Drawing.Point(459, 16);
            this.txtIdProveedor.Name = "txtIdProveedor";
            this.txtIdProveedor.Size = new System.Drawing.Size(38, 26);
            this.txtIdProveedor.TabIndex = 9;
            // 
            // lblRazonSocial
            // 
            this.lblRazonSocial.AutoSize = true;
            this.lblRazonSocial.Location = new System.Drawing.Point(265, 26);
            this.lblRazonSocial.Name = "lblRazonSocial";
            this.lblRazonSocial.Size = new System.Drawing.Size(107, 20);
            this.lblRazonSocial.TabIndex = 8;
            this.lblRazonSocial.Text = "Razon Social:";
            // 
            // txtRazonSocial
            // 
            this.txtRazonSocial.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRazonSocial.Location = new System.Drawing.Point(268, 51);
            this.txtRazonSocial.Name = "txtRazonSocial";
            this.txtRazonSocial.Size = new System.Drawing.Size(229, 29);
            this.txtRazonSocial.TabIndex = 7;
            // 
            // txtDocuProveedor
            // 
            this.txtDocuProveedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDocuProveedor.Location = new System.Drawing.Point(130, 52);
            this.txtDocuProveedor.Name = "txtDocuProveedor";
            this.txtDocuProveedor.Size = new System.Drawing.Size(112, 29);
            this.txtDocuProveedor.TabIndex = 5;
            // 
            // erpCantidad
            // 
            this.erpCantidad.ContainerControl = this;
            // 
            // erpDocuProveedor
            // 
            this.erpDocuProveedor.ContainerControl = this;
            // 
            // erpPrecioVenta
            // 
            this.erpPrecioVenta.ContainerControl = this;
            // 
            // erpPrecioCompra
            // 
            this.erpPrecioCompra.ContainerControl = this;
            // 
            // erpCodigoMedicamento
            // 
            this.erpCodigoMedicamento.ContainerControl = this;
            // 
            // erpNfactura
            // 
            this.erpNfactura.ContainerControl = this;
            // 
            // FrmVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::CpFarmacia2024.Properties.Resources.descargar__1_1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(926, 435);
            this.Controls.Add(this.iBtnQuitar);
            this.Controls.Add(this.dgvCompras);
            this.Controls.Add(this.iBtnRegistrar);
            this.Controls.Add(this.txtTotalPagar);
            this.Controls.Add(this.ibtnAgregar);
            this.Controls.Add(this.lblTotalPagar);
            this.Controls.Add(this.gbxInfoMedicamento);
            this.Controls.Add(this.gbxInformacionProveedor);
            this.Controls.Add(this.gbxInfoCompra);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmVenta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "::: Farmacia - Venta :::";
            this.Load += new System.EventHandler(this.FrmVenta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpTipoDocumento)).EndInit();
            this.gbxInfoCompra.ResumeLayout(false);
            this.gbxInfoCompra.PerformLayout();
            this.gbxInfoMedicamento.ResumeLayout(false);
            this.gbxInfoMedicamento.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            this.gbxInformacionProveedor.ResumeLayout(false);
            this.gbxInformacionProveedor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erpCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpDocuProveedor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpPrecioVenta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpPrecioCompra)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpCodigoMedicamento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpNfactura)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCompras;
        private System.Windows.Forms.ErrorProvider erpTipoDocumento;
        private FontAwesome.Sharp.IconButton iBtnQuitar;
        private FontAwesome.Sharp.IconButton iBtnRegistrar;
        private System.Windows.Forms.TextBox txtTotalPagar;
        private FontAwesome.Sharp.IconButton ibtnAgregar;
        private System.Windows.Forms.Label lblTotalPagar;
        private System.Windows.Forms.GroupBox gbxInfoMedicamento;
        private System.Windows.Forms.Button btnBuscarMedicamento;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.Label lblPrecioVenta;
        private System.Windows.Forms.TextBox txtPrecioVenta;
        private System.Windows.Forms.Label lblPrecioCompra;
        private System.Windows.Forms.TextBox txtPrecioCompra;
        private System.Windows.Forms.Label lblMedicamento;
        private System.Windows.Forms.TextBox txtMedicamento;
        private System.Windows.Forms.TextBox txtIdMedicamento;
        private System.Windows.Forms.Label lblCodigoMedicamento;
        private System.Windows.Forms.TextBox txtCodigoMedicamento;
        private System.Windows.Forms.GroupBox gbxInformacionProveedor;
        private System.Windows.Forms.Button btnBuscarProveedor;
        private System.Windows.Forms.TextBox txtIdProveedor;
        private System.Windows.Forms.Label lblRazonSocial;
        private System.Windows.Forms.TextBox txtRazonSocial;
        private System.Windows.Forms.TextBox txtDocuProveedor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbxInfoCompra;
        private System.Windows.Forms.TextBox txtNfactura;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxTipoDocumento;
        private System.Windows.Forms.TextBox txtFecha;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider erpCantidad;
        private System.Windows.Forms.ErrorProvider erpDocuProveedor;
        private System.Windows.Forms.ErrorProvider erpPrecioVenta;
        private System.Windows.Forms.ErrorProvider erpPrecioCompra;
        private System.Windows.Forms.ErrorProvider erpCodigoMedicamento;
        private System.Windows.Forms.ErrorProvider erpNfactura;
    }
}