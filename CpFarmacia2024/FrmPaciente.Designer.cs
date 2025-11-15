namespace CpFarmacia2024
{
    partial class FrmPaciente
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmbFILTRO_OBRA_SOCIAL = new System.Windows.Forms.ComboBox();
            this.txtBUSCAR = new System.Windows.Forms.TextBox();
            this.cmbPLAN = new System.Windows.Forms.ComboBox();
            this.lblPLAN = new System.Windows.Forms.Label();
            this.txtEMAIL = new System.Windows.Forms.TextBox();
            this.lblEMAIL = new System.Windows.Forms.Label();
            this.txtNOMBRE = new System.Windows.Forms.TextBox();
            this.lblNOMBRE = new System.Windows.Forms.Label();
            this.cmbOBRA_SOCIAL = new System.Windows.Forms.ComboBox();
            this.btnELIMINAR = new System.Windows.Forms.Button();
            this.btnCONSULTAR = new System.Windows.Forms.Button();
            this.btnMODIFICAR = new System.Windows.Forms.Button();
            this.lblOBRA_SOCIAL = new System.Windows.Forms.Label();
            this.lblAPELLIDO = new System.Windows.Forms.Label();
            this.txtCONTACTO = new System.Windows.Forms.TextBox();
            this.btnCERRAR = new System.Windows.Forms.Button();
            this.lblCONTACTO = new System.Windows.Forms.Label();
            this.dgvLISTA_PACIENTES = new System.Windows.Forms.DataGridView();
            this.txtAPELLIDO = new System.Windows.Forms.TextBox();
            this.btnBUSCAR = new System.Windows.Forms.Button();
            this.gbLISTA_PACIENTES = new System.Windows.Forms.GroupBox();
            this.lblFILTRO_GRUPO = new System.Windows.Forms.Label();
            this.btnAGREGAR = new System.Windows.Forms.Button();
            this.lblEDAD_DESDE = new System.Windows.Forms.Label();
            this.lblEDAD_HASTA = new System.Windows.Forms.Label();
            this.btnCANCELAR = new System.Windows.Forms.Button();
            this.btnGUARDAR = new System.Windows.Forms.Button();
            this.gbDATOS_PACIENTE = new System.Windows.Forms.GroupBox();
            this.lblEDAD = new System.Windows.Forms.Label();
            this.nudEdad = new System.Windows.Forms.NumericUpDown();
            this.erpNombre = new System.Windows.Forms.ErrorProvider(this.components);
            this.erpApellido = new System.Windows.Forms.ErrorProvider(this.components);
            this.erpDocumento = new System.Windows.Forms.ErrorProvider(this.components);
            this.erpEMAIL = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLISTA_PACIENTES)).BeginInit();
            this.gbLISTA_PACIENTES.SuspendLayout();
            this.gbDATOS_PACIENTE.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEdad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpNombre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpApellido)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpDocumento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpEMAIL)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbFILTRO_OBRA_SOCIAL
            // 
            this.cmbFILTRO_OBRA_SOCIAL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFILTRO_OBRA_SOCIAL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFILTRO_OBRA_SOCIAL.FormattingEnabled = true;
            this.cmbFILTRO_OBRA_SOCIAL.Location = new System.Drawing.Point(503, 26);
            this.cmbFILTRO_OBRA_SOCIAL.Name = "cmbFILTRO_OBRA_SOCIAL";
            this.cmbFILTRO_OBRA_SOCIAL.Size = new System.Drawing.Size(168, 21);
            this.cmbFILTRO_OBRA_SOCIAL.TabIndex = 70;
            this.cmbFILTRO_OBRA_SOCIAL.SelectedIndexChanged += new System.EventHandler(this.cmbFILTRO_OBRA_SOCIAL_SelectedIndexChanged);
            // 
            // txtBUSCAR
            // 
            this.txtBUSCAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBUSCAR.Location = new System.Drawing.Point(345, 26);
            this.txtBUSCAR.Name = "txtBUSCAR";
            this.txtBUSCAR.Size = new System.Drawing.Size(150, 20);
            this.txtBUSCAR.TabIndex = 69;
            this.txtBUSCAR.TextChanged += new System.EventHandler(this.txtBUSCAR_TextChanged);
            // 
            // cmbPLAN
            // 
            this.cmbPLAN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPLAN.Enabled = false;
            this.cmbPLAN.FormattingEnabled = true;
            this.cmbPLAN.Location = new System.Drawing.Point(10, 320);
            this.cmbPLAN.Name = "cmbPLAN";
            this.cmbPLAN.Size = new System.Drawing.Size(252, 21);
            this.cmbPLAN.TabIndex = 77;
            this.cmbPLAN.SelectedIndexChanged += new System.EventHandler(this.cmbPLAN_SelectedIndexChanged);
            // 
            // lblPLAN
            // 
            this.lblPLAN.AutoSize = true;
            this.lblPLAN.Enabled = false;
            this.lblPLAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.lblPLAN.Location = new System.Drawing.Point(120, 300);
            this.lblPLAN.Name = "lblPLAN";
            this.lblPLAN.Size = new System.Drawing.Size(44, 17);
            this.lblPLAN.TabIndex = 76;
            this.lblPLAN.Text = "PLAN";
            this.lblPLAN.Click += new System.EventHandler(this.lblPLAN_Click);
            // 
            // txtEMAIL
            // 
            this.txtEMAIL.Enabled = false;
            this.txtEMAIL.Location = new System.Drawing.Point(99, 205);
            this.txtEMAIL.Name = "txtEMAIL";
            this.txtEMAIL.Size = new System.Drawing.Size(166, 20);
            this.txtEMAIL.TabIndex = 75;
            this.txtEMAIL.TextChanged += new System.EventHandler(this.txtEMAIL_TextChanged);
            // 
            // lblEMAIL
            // 
            this.lblEMAIL.AutoSize = true;
            this.lblEMAIL.Enabled = false;
            this.lblEMAIL.Location = new System.Drawing.Point(9, 208);
            this.lblEMAIL.Name = "lblEMAIL";
            this.lblEMAIL.Size = new System.Drawing.Size(42, 13);
            this.lblEMAIL.TabIndex = 74;
            this.lblEMAIL.Text = "EMAIL:";
            // 
            // txtNOMBRE
            // 
            this.txtNOMBRE.Enabled = false;
            this.txtNOMBRE.Location = new System.Drawing.Point(99, 31);
            this.txtNOMBRE.Name = "txtNOMBRE";
            this.txtNOMBRE.Size = new System.Drawing.Size(166, 20);
            this.txtNOMBRE.TabIndex = 68;
            this.txtNOMBRE.TextChanged += new System.EventHandler(this.txtNOMBRE_TextChanged);
            // 
            // lblNOMBRE
            // 
            this.lblNOMBRE.AutoSize = true;
            this.lblNOMBRE.Enabled = false;
            this.lblNOMBRE.Location = new System.Drawing.Point(9, 34);
            this.lblNOMBRE.Name = "lblNOMBRE";
            this.lblNOMBRE.Size = new System.Drawing.Size(57, 13);
            this.lblNOMBRE.TabIndex = 64;
            this.lblNOMBRE.Text = "NOMBRE:";
            this.lblNOMBRE.Click += new System.EventHandler(this.lblNOMBRE_Click);
            // 
            // cmbOBRA_SOCIAL
            // 
            this.cmbOBRA_SOCIAL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOBRA_SOCIAL.Enabled = false;
            this.cmbOBRA_SOCIAL.FormattingEnabled = true;
            this.cmbOBRA_SOCIAL.Location = new System.Drawing.Point(10, 264);
            this.cmbOBRA_SOCIAL.Name = "cmbOBRA_SOCIAL";
            this.cmbOBRA_SOCIAL.Size = new System.Drawing.Size(252, 21);
            this.cmbOBRA_SOCIAL.TabIndex = 73;
            this.cmbOBRA_SOCIAL.SelectedIndexChanged += new System.EventHandler(this.cmbOBRA_SOCIAL_SelectedIndexChanged);
            // 
            // btnELIMINAR
            // 
            this.btnELIMINAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnELIMINAR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(66)))), ((int)(((byte)(82)))));
            this.btnELIMINAR.FlatAppearance.BorderSize = 0;
            this.btnELIMINAR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnELIMINAR.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnELIMINAR.ForeColor = System.Drawing.Color.White;
            this.btnELIMINAR.Location = new System.Drawing.Point(272, 381);
            this.btnELIMINAR.Name = "btnELIMINAR";
            this.btnELIMINAR.Size = new System.Drawing.Size(83, 30);
            this.btnELIMINAR.TabIndex = 15;
            this.btnELIMINAR.Text = "Eliminar";
            this.btnELIMINAR.UseVisualStyleBackColor = false;
            this.btnELIMINAR.Click += new System.EventHandler(this.btnELIMINAR_Click);
            // 
            // btnCONSULTAR
            // 
            this.btnCONSULTAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCONSULTAR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(66)))), ((int)(((byte)(82)))));
            this.btnCONSULTAR.FlatAppearance.BorderSize = 0;
            this.btnCONSULTAR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCONSULTAR.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnCONSULTAR.ForeColor = System.Drawing.Color.White;
            this.btnCONSULTAR.Location = new System.Drawing.Point(183, 381);
            this.btnCONSULTAR.Name = "btnCONSULTAR";
            this.btnCONSULTAR.Size = new System.Drawing.Size(83, 30);
            this.btnCONSULTAR.TabIndex = 14;
            this.btnCONSULTAR.Text = "Consultar";
            this.btnCONSULTAR.UseVisualStyleBackColor = false;
            this.btnCONSULTAR.Click += new System.EventHandler(this.btnCONSULTAR_Click);
            // 
            // btnMODIFICAR
            // 
            this.btnMODIFICAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMODIFICAR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(66)))), ((int)(((byte)(82)))));
            this.btnMODIFICAR.FlatAppearance.BorderSize = 0;
            this.btnMODIFICAR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMODIFICAR.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnMODIFICAR.ForeColor = System.Drawing.Color.White;
            this.btnMODIFICAR.Location = new System.Drawing.Point(94, 381);
            this.btnMODIFICAR.Name = "btnMODIFICAR";
            this.btnMODIFICAR.Size = new System.Drawing.Size(83, 30);
            this.btnMODIFICAR.TabIndex = 13;
            this.btnMODIFICAR.Text = "Modificar";
            this.btnMODIFICAR.UseVisualStyleBackColor = false;
            this.btnMODIFICAR.Click += new System.EventHandler(this.btnMODIFICAR_Click);
            // 
            // lblOBRA_SOCIAL
            // 
            this.lblOBRA_SOCIAL.AutoSize = true;
            this.lblOBRA_SOCIAL.Enabled = false;
            this.lblOBRA_SOCIAL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.lblOBRA_SOCIAL.Location = new System.Drawing.Point(96, 244);
            this.lblOBRA_SOCIAL.Name = "lblOBRA_SOCIAL";
            this.lblOBRA_SOCIAL.Size = new System.Drawing.Size(100, 17);
            this.lblOBRA_SOCIAL.TabIndex = 72;
            this.lblOBRA_SOCIAL.Text = "OBRA SOCIAL";
            this.lblOBRA_SOCIAL.Click += new System.EventHandler(this.lblOBRA_SOCIAL_Click);
            // 
            // lblAPELLIDO
            // 
            this.lblAPELLIDO.AutoSize = true;
            this.lblAPELLIDO.Enabled = false;
            this.lblAPELLIDO.Location = new System.Drawing.Point(9, 92);
            this.lblAPELLIDO.Name = "lblAPELLIDO";
            this.lblAPELLIDO.Size = new System.Drawing.Size(62, 13);
            this.lblAPELLIDO.TabIndex = 66;
            this.lblAPELLIDO.Text = "APELLIDO:";
            this.lblAPELLIDO.Click += new System.EventHandler(this.lblAPELLIDO_Click);
            // 
            // txtCONTACTO
            // 
            this.txtCONTACTO.Enabled = false;
            this.txtCONTACTO.Location = new System.Drawing.Point(99, 144);
            this.txtCONTACTO.Name = "txtCONTACTO";
            this.txtCONTACTO.Size = new System.Drawing.Size(166, 20);
            this.txtCONTACTO.TabIndex = 71;
            this.txtCONTACTO.TextChanged += new System.EventHandler(this.txtCONTACTO_TextChanged);
            // 
            // btnCERRAR
            // 
            this.btnCERRAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCERRAR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(66)))), ((int)(((byte)(82)))));
            this.btnCERRAR.FlatAppearance.BorderSize = 0;
            this.btnCERRAR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCERRAR.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnCERRAR.ForeColor = System.Drawing.Color.White;
            this.btnCERRAR.Location = new System.Drawing.Point(694, 381);
            this.btnCERRAR.Name = "btnCERRAR";
            this.btnCERRAR.Size = new System.Drawing.Size(83, 30);
            this.btnCERRAR.TabIndex = 16;
            this.btnCERRAR.Text = "Cerrar";
            this.btnCERRAR.UseVisualStyleBackColor = false;
            this.btnCERRAR.Click += new System.EventHandler(this.btnCERRAR_Click);
            // 
            // lblCONTACTO
            // 
            this.lblCONTACTO.AutoSize = true;
            this.lblCONTACTO.Enabled = false;
            this.lblCONTACTO.Location = new System.Drawing.Point(9, 147);
            this.lblCONTACTO.Name = "lblCONTACTO";
            this.lblCONTACTO.Size = new System.Drawing.Size(69, 13);
            this.lblCONTACTO.TabIndex = 67;
            this.lblCONTACTO.Text = "CONTACTO:";
            this.lblCONTACTO.Click += new System.EventHandler(this.lblCONTACTO_Click);
            // 
            // dgvLISTA_PACIENTES
            // 
            this.dgvLISTA_PACIENTES.AllowUserToAddRows = false;
            this.dgvLISTA_PACIENTES.AllowUserToDeleteRows = false;
            this.dgvLISTA_PACIENTES.AllowUserToResizeColumns = false;
            this.dgvLISTA_PACIENTES.AllowUserToResizeRows = false;
            this.dgvLISTA_PACIENTES.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLISTA_PACIENTES.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLISTA_PACIENTES.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.dgvLISTA_PACIENTES.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvLISTA_PACIENTES.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvLISTA_PACIENTES.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLISTA_PACIENTES.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLISTA_PACIENTES.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvLISTA_PACIENTES.EnableHeadersVisualStyles = false;
            this.dgvLISTA_PACIENTES.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200)))));
            this.dgvLISTA_PACIENTES.Location = new System.Drawing.Point(6, 61);
            this.dgvLISTA_PACIENTES.Name = "dgvLISTA_PACIENTES";
            this.dgvLISTA_PACIENTES.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLISTA_PACIENTES.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLISTA_PACIENTES.RowHeadersVisible = false;
            this.dgvLISTA_PACIENTES.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(66)))), ((int)(((byte)(82)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            this.dgvLISTA_PACIENTES.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvLISTA_PACIENTES.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLISTA_PACIENTES.Size = new System.Drawing.Size(771, 314);
            this.dgvLISTA_PACIENTES.TabIndex = 0;
            this.dgvLISTA_PACIENTES.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLISTA_PACIENTES_CellContentClick);
            // 
            // txtAPELLIDO
            // 
            this.txtAPELLIDO.Enabled = false;
            this.txtAPELLIDO.Location = new System.Drawing.Point(99, 89);
            this.txtAPELLIDO.Name = "txtAPELLIDO";
            this.txtAPELLIDO.Size = new System.Drawing.Size(166, 20);
            this.txtAPELLIDO.TabIndex = 70;
            this.txtAPELLIDO.TextChanged += new System.EventHandler(this.txtAPELLIDO_TextChanged);
            // 
            // btnBUSCAR
            // 
            this.btnBUSCAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBUSCAR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(66)))), ((int)(((byte)(82)))));
            this.btnBUSCAR.FlatAppearance.BorderSize = 0;
            this.btnBUSCAR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBUSCAR.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnBUSCAR.ForeColor = System.Drawing.Color.White;
            this.btnBUSCAR.Location = new System.Drawing.Point(677, 18);
            this.btnBUSCAR.Name = "btnBUSCAR";
            this.btnBUSCAR.Size = new System.Drawing.Size(100, 36);
            this.btnBUSCAR.TabIndex = 76;
            this.btnBUSCAR.Text = "Buscar";
            this.btnBUSCAR.UseVisualStyleBackColor = false;
            this.btnBUSCAR.Click += new System.EventHandler(this.btnBUSCAR_Click);
            // 
            // gbLISTA_PACIENTES
            // 
            this.gbLISTA_PACIENTES.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLISTA_PACIENTES.Controls.Add(this.btnBUSCAR);
            this.gbLISTA_PACIENTES.Controls.Add(this.txtBUSCAR);
            this.gbLISTA_PACIENTES.Controls.Add(this.lblFILTRO_GRUPO);
            this.gbLISTA_PACIENTES.Controls.Add(this.cmbFILTRO_OBRA_SOCIAL);
            this.gbLISTA_PACIENTES.Controls.Add(this.btnCERRAR);
            this.gbLISTA_PACIENTES.Controls.Add(this.btnELIMINAR);
            this.gbLISTA_PACIENTES.Controls.Add(this.btnCONSULTAR);
            this.gbLISTA_PACIENTES.Controls.Add(this.btnMODIFICAR);
            this.gbLISTA_PACIENTES.Controls.Add(this.btnAGREGAR);
            this.gbLISTA_PACIENTES.Controls.Add(this.dgvLISTA_PACIENTES);
            this.gbLISTA_PACIENTES.Controls.Add(this.lblEDAD_DESDE);
            this.gbLISTA_PACIENTES.Controls.Add(this.lblEDAD_HASTA);
            this.gbLISTA_PACIENTES.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbLISTA_PACIENTES.ForeColor = System.Drawing.Color.White;
            this.gbLISTA_PACIENTES.Location = new System.Drawing.Point(8, 11);
            this.gbLISTA_PACIENTES.Name = "gbLISTA_PACIENTES";
            this.gbLISTA_PACIENTES.Size = new System.Drawing.Size(783, 417);
            this.gbLISTA_PACIENTES.TabIndex = 10;
            this.gbLISTA_PACIENTES.TabStop = false;
            this.gbLISTA_PACIENTES.Text = "LISTA DE PACIENTES";
            this.gbLISTA_PACIENTES.Enter += new System.EventHandler(this.gbLISTA_PACIENTES_Enter);
            // 
            // lblFILTRO_GRUPO
            // 
            this.lblFILTRO_GRUPO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFILTRO_GRUPO.AutoSize = true;
            this.lblFILTRO_GRUPO.ForeColor = System.Drawing.Color.White;
            this.lblFILTRO_GRUPO.Location = new System.Drawing.Point(345, 31);
            this.lblFILTRO_GRUPO.Name = "lblFILTRO_GRUPO";
            this.lblFILTRO_GRUPO.Size = new System.Drawing.Size(152, 13);
            this.lblFILTRO_GRUPO.TabIndex = 71;
            this.lblFILTRO_GRUPO.Text = "FILTRAR POR OBRA SOCIAL";
            // 
            // btnAGREGAR
            // 
            this.btnAGREGAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAGREGAR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(66)))), ((int)(((byte)(82)))));
            this.btnAGREGAR.FlatAppearance.BorderSize = 0;
            this.btnAGREGAR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAGREGAR.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnAGREGAR.ForeColor = System.Drawing.Color.White;
            this.btnAGREGAR.Location = new System.Drawing.Point(5, 381);
            this.btnAGREGAR.Name = "btnAGREGAR";
            this.btnAGREGAR.Size = new System.Drawing.Size(83, 30);
            this.btnAGREGAR.TabIndex = 11;
            this.btnAGREGAR.Text = "Agregar";
            this.btnAGREGAR.UseVisualStyleBackColor = false;
            this.btnAGREGAR.Click += new System.EventHandler(this.btnAGREGAR_Click);
            // 
            // lblEDAD_DESDE
            // 
            this.lblEDAD_DESDE.AutoSize = true;
            this.lblEDAD_DESDE.ForeColor = System.Drawing.Color.White;
            this.lblEDAD_DESDE.Location = new System.Drawing.Point(6, 380);
            this.lblEDAD_DESDE.Name = "lblEDAD_DESDE";
            this.lblEDAD_DESDE.Size = new System.Drawing.Size(44, 13);
            this.lblEDAD_DESDE.TabIndex = 81;
            this.lblEDAD_DESDE.Text = "Edad ≥:";
            // 
            // lblEDAD_HASTA
            // 
            this.lblEDAD_HASTA.AutoSize = true;
            this.lblEDAD_HASTA.ForeColor = System.Drawing.Color.White;
            this.lblEDAD_HASTA.Location = new System.Drawing.Point(125, 380);
            this.lblEDAD_HASTA.Name = "lblEDAD_HASTA";
            this.lblEDAD_HASTA.Size = new System.Drawing.Size(44, 13);
            this.lblEDAD_HASTA.TabIndex = 83;
            this.lblEDAD_HASTA.Text = "Edad ≤:";
            // 
            // btnCANCELAR
            // 
            this.btnCANCELAR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCANCELAR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(66)))), ((int)(((byte)(82)))));
            this.btnCANCELAR.FlatAppearance.BorderSize = 0;
            this.btnCANCELAR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCANCELAR.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnCANCELAR.ForeColor = System.Drawing.Color.White;
            this.btnCANCELAR.Location = new System.Drawing.Point(155, 375);
            this.btnCANCELAR.Name = "btnCANCELAR";
            this.btnCANCELAR.Size = new System.Drawing.Size(107, 36);
            this.btnCANCELAR.TabIndex = 7;
            this.btnCANCELAR.Text = "Cancelar";
            this.btnCANCELAR.UseVisualStyleBackColor = false;
            this.btnCANCELAR.Click += new System.EventHandler(this.btnCANCELAR_Click);
            // 
            // btnGUARDAR
            // 
            this.btnGUARDAR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGUARDAR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(66)))), ((int)(((byte)(82)))));
            this.btnGUARDAR.FlatAppearance.BorderSize = 0;
            this.btnGUARDAR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGUARDAR.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnGUARDAR.ForeColor = System.Drawing.Color.White;
            this.btnGUARDAR.Location = new System.Drawing.Point(6, 375);
            this.btnGUARDAR.Name = "btnGUARDAR";
            this.btnGUARDAR.Size = new System.Drawing.Size(107, 36);
            this.btnGUARDAR.TabIndex = 1;
            this.btnGUARDAR.Text = "Guardar";
            this.btnGUARDAR.UseVisualStyleBackColor = false;
            this.btnGUARDAR.Click += new System.EventHandler(this.btnGUARDAR_Click);
            // 
            // gbDATOS_PACIENTE
            // 
            this.gbDATOS_PACIENTE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDATOS_PACIENTE.Controls.Add(this.cmbPLAN);
            this.gbDATOS_PACIENTE.Controls.Add(this.lblPLAN);
            this.gbDATOS_PACIENTE.Controls.Add(this.txtEMAIL);
            this.gbDATOS_PACIENTE.Controls.Add(this.lblEMAIL);
            this.gbDATOS_PACIENTE.Controls.Add(this.txtNOMBRE);
            this.gbDATOS_PACIENTE.Controls.Add(this.lblNOMBRE);
            this.gbDATOS_PACIENTE.Controls.Add(this.cmbOBRA_SOCIAL);
            this.gbDATOS_PACIENTE.Controls.Add(this.lblOBRA_SOCIAL);
            this.gbDATOS_PACIENTE.Controls.Add(this.lblAPELLIDO);
            this.gbDATOS_PACIENTE.Controls.Add(this.txtCONTACTO);
            this.gbDATOS_PACIENTE.Controls.Add(this.lblCONTACTO);
            this.gbDATOS_PACIENTE.Controls.Add(this.txtAPELLIDO);
            this.gbDATOS_PACIENTE.Controls.Add(this.lblEDAD);
            this.gbDATOS_PACIENTE.Controls.Add(this.nudEdad);
            this.gbDATOS_PACIENTE.Controls.Add(this.btnCANCELAR);
            this.gbDATOS_PACIENTE.Controls.Add(this.btnGUARDAR);
            this.gbDATOS_PACIENTE.Enabled = false;
            this.gbDATOS_PACIENTE.ForeColor = System.Drawing.Color.White;
            this.gbDATOS_PACIENTE.Location = new System.Drawing.Point(797, 11);
            this.gbDATOS_PACIENTE.Name = "gbDATOS_PACIENTE";
            this.gbDATOS_PACIENTE.Size = new System.Drawing.Size(271, 417);
            this.gbDATOS_PACIENTE.TabIndex = 11;
            this.gbDATOS_PACIENTE.TabStop = false;
            this.gbDATOS_PACIENTE.Text = "DATOS DEL PACIENTE";
            this.gbDATOS_PACIENTE.Enter += new System.EventHandler(this.gbDATOS_PACIENTE_Enter);
            // 
            // lblEDAD
            // 
            this.lblEDAD.AutoSize = true;
            this.lblEDAD.Enabled = false;
            this.lblEDAD.Location = new System.Drawing.Point(9, 176);
            this.lblEDAD.Name = "lblEDAD";
            this.lblEDAD.Size = new System.Drawing.Size(40, 13);
            this.lblEDAD.TabIndex = 78;
            this.lblEDAD.Text = "EDAD:";
            this.lblEDAD.Click += new System.EventHandler(this.lblEDAD_Click);
            // 
            // nudEdad
            // 
            this.nudEdad.Enabled = false;
            this.nudEdad.Location = new System.Drawing.Point(99, 174);
            this.nudEdad.Maximum = new decimal(new int[] {
            130,
            0,
            0,
            0});
            this.nudEdad.Name = "nudEdad";
            this.nudEdad.Size = new System.Drawing.Size(60, 20);
            this.nudEdad.TabIndex = 79;
            this.nudEdad.ValueChanged += new System.EventHandler(this.nudEdad_ValueChanged);
            // 
            // erpNombre
            // 
            this.erpNombre.ContainerControl = this;
            // 
            // erpApellido
            // 
            this.erpApellido.ContainerControl = this;
            // 
            // erpDocumento
            // 
            this.erpDocumento.ContainerControl = this;
            // 
            // erpEMAIL
            // 
            this.erpEMAIL.ContainerControl = this;
            // 
            // FrmPaciente
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(1077, 438);
            this.Controls.Add(this.gbLISTA_PACIENTES);
            this.Controls.Add(this.gbDATOS_PACIENTE);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrmPaciente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de Pacientes";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLISTA_PACIENTES)).EndInit();
            this.gbLISTA_PACIENTES.ResumeLayout(false);
            this.gbLISTA_PACIENTES.PerformLayout();
            this.gbDATOS_PACIENTE.ResumeLayout(false);
            this.gbDATOS_PACIENTE.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEdad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpNombre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpApellido)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpDocumento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpEMAIL)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.ComboBox cmbFILTRO_OBRA_SOCIAL;
        private System.Windows.Forms.TextBox txtBUSCAR;
        private System.Windows.Forms.ComboBox cmbPLAN;
        private System.Windows.Forms.Label lblPLAN;
        private System.Windows.Forms.TextBox txtEMAIL;
        private System.Windows.Forms.Label lblEMAIL;
        private System.Windows.Forms.TextBox txtNOMBRE;
        private System.Windows.Forms.Label lblNOMBRE;
        private System.Windows.Forms.ComboBox cmbOBRA_SOCIAL;
        private System.Windows.Forms.Button btnELIMINAR;
        private System.Windows.Forms.Button btnCONSULTAR;
        private System.Windows.Forms.Button btnMODIFICAR;
        private System.Windows.Forms.Label lblOBRA_SOCIAL;
        private System.Windows.Forms.Label lblAPELLIDO;
        private System.Windows.Forms.TextBox txtCONTACTO;
        private System.Windows.Forms.Button btnCERRAR;
        private System.Windows.Forms.Label lblCONTACTO;
        private System.Windows.Forms.DataGridView dgvLISTA_PACIENTES;
        private System.Windows.Forms.TextBox txtAPELLIDO;
        private System.Windows.Forms.Button btnBUSCAR;
        private System.Windows.Forms.GroupBox gbLISTA_PACIENTES;
        private System.Windows.Forms.Label lblFILTRO_GRUPO;
        private System.Windows.Forms.Button btnAGREGAR;
        private System.Windows.Forms.Button btnCANCELAR;
        private System.Windows.Forms.Button btnGUARDAR;
        private System.Windows.Forms.GroupBox gbDATOS_PACIENTE;
        // Declaraciones añadidas
        private System.Windows.Forms.NumericUpDown nudEdad;
        private System.Windows.Forms.Label lblEDAD;
        private System.Windows.Forms.Label lblEDAD_DESDE;
        private System.Windows.Forms.Label lblEDAD_HASTA;
        private System.Windows.Forms.ErrorProvider erpNombre;
        private System.Windows.Forms.ErrorProvider erpApellido;
        private System.Windows.Forms.ErrorProvider erpDocumento;
        private System.Windows.Forms.ErrorProvider erpEMAIL;
    }
}