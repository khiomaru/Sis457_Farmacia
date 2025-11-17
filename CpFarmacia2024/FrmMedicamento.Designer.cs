using System.Drawing;
using System.Windows.Forms;

namespace CpFarmacia2024
{
    partial class FrmMedicamento
    {
        private System.ComponentModel.IContainer components = null;

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
            this.erpCategoria = new System.Windows.Forms.ErrorProvider(this.components);
            this.erpMarca = new System.Windows.Forms.ErrorProvider(this.components);
            this.erpDescripcion = new System.Windows.Forms.ErrorProvider(this.components);
            this.erpCodigo = new System.Windows.Forms.ErrorProvider(this.components);
            this.erpPrecioVenta = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblPrincipal = new System.Windows.Forms.Label();
            this.lblBusqueda = new System.Windows.Forms.Label();
            this.txtParametro = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.gbxLista = new System.Windows.Forms.GroupBox();
            this.dgvListaMedicamentos = new System.Windows.Forms.DataGridView();
            this.gbxDatos = new System.Windows.Forms.GroupBox();
            this.pnlInfoBasica = new System.Windows.Forms.Panel();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblMarca = new System.Windows.Forms.Label();
            this.lblPresentacion = new System.Windows.Forms.Label();
            this.pnlInventario = new System.Windows.Forms.Panel();
            this.lblStockActual = new System.Windows.Forms.Label();
            this.nudStockActual = new System.Windows.Forms.NumericUpDown();
            this.lblFechaCaducidad = new System.Windows.Forms.Label();
            this.dtpFechaCaducidad = new System.Windows.Forms.DateTimePicker();
            this.pnlDetalles = new System.Windows.Forms.Panel();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.cbxCategoria = new System.Windows.Forms.ComboBox();
            this.lblPrecioVenta = new System.Windows.Forms.Label();
            this.nudPrecioVenta = new System.Windows.Forms.NumericUpDown();
            this.lblEstado = new System.Windows.Forms.Label();
            this.cbxEstado = new System.Windows.Forms.ComboBox();
            this.pnlDatosAcciones = new System.Windows.Forms.Panel();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.cboMarca = new System.Windows.Forms.ComboBox();
            this.cboPresentacion = new System.Windows.Forms.ComboBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.cboGrupo = new System.Windows.Forms.Label();
            this.cboGrupoo = new System.Windows.Forms.ComboBox();
            this.lblUnidadMedida = new System.Windows.Forms.Label();
            this.cboUnidadMedida = new System.Windows.Forms.ComboBox();
            this.lblClasificacionATK = new System.Windows.Forms.Label();
            this.txtClasificacionATK = new System.Windows.Forms.TextBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.pnlAcciones = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.erpCategoria)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpMarca)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpDescripcion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpCodigo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpPrecioVenta)).BeginInit();
            this.gbxLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaMedicamentos)).BeginInit();
            this.gbxDatos.SuspendLayout();
            this.pnlInfoBasica.SuspendLayout();
            this.pnlInventario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStockActual)).BeginInit();
            this.pnlDetalles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecioVenta)).BeginInit();
            this.pnlDatosAcciones.SuspendLayout();
            this.pnlAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // erpCategoria
            // 
            this.erpCategoria.ContainerControl = this;
            // 
            // erpMarca
            // 
            this.erpMarca.ContainerControl = this;
            // 
            // erpDescripcion
            // 
            this.erpDescripcion.ContainerControl = this;
            // 
            // erpCodigo
            // 
            this.erpCodigo.ContainerControl = this;
            // 
            // erpPrecioVenta
            // 
            this.erpPrecioVenta.ContainerControl = this;
            // 
            // lblPrincipal
            // 
            this.lblPrincipal.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold);
            this.lblPrincipal.Location = new System.Drawing.Point(0, -2);
            this.lblPrincipal.Name = "lblPrincipal";
            this.lblPrincipal.Size = new System.Drawing.Size(900, 36);
            this.lblPrincipal.TabIndex = 0;
            this.lblPrincipal.Text = "Medicamentos";
            this.lblPrincipal.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblBusqueda
            // 
            this.lblBusqueda.AutoSize = true;
            this.lblBusqueda.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lblBusqueda.Location = new System.Drawing.Point(51, 25);
            this.lblBusqueda.Name = "lblBusqueda";
            this.lblBusqueda.Size = new System.Drawing.Size(261, 19);
            this.lblBusqueda.TabIndex = 1;
            this.lblBusqueda.Text = "Buscar por descripción del Medicamento:";
            // 
            // txtParametro
            // 
            this.txtParametro.Location = new System.Drawing.Point(55, 47);
            this.txtParametro.Name = "txtParametro";
            this.txtParametro.Size = new System.Drawing.Size(688, 23);
            this.txtParametro.TabIndex = 2;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnBuscar.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnBuscar.Location = new System.Drawing.Point(754, 38);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(91, 36);
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click_1);
            // 
            // gbxLista
            // 
            this.gbxLista.Controls.Add(this.dgvListaMedicamentos);
            this.gbxLista.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.gbxLista.Location = new System.Drawing.Point(55, 76);
            this.gbxLista.Name = "gbxLista";
            this.gbxLista.Size = new System.Drawing.Size(790, 185);
            this.gbxLista.TabIndex = 4;
            this.gbxLista.TabStop = false;
            this.gbxLista.Text = "Lista de Medicamentos";
            // 
            // dgvListaMedicamentos
            // 
            this.dgvListaMedicamentos.AllowUserToAddRows = false;
            this.dgvListaMedicamentos.AllowUserToDeleteRows = false;
            this.dgvListaMedicamentos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvListaMedicamentos.Location = new System.Drawing.Point(7, 22);
            this.dgvListaMedicamentos.Name = "dgvListaMedicamentos";
            this.dgvListaMedicamentos.ReadOnly = true;
            this.dgvListaMedicamentos.RowHeadersVisible = false;
            this.dgvListaMedicamentos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListaMedicamentos.Size = new System.Drawing.Size(776, 162);
            this.dgvListaMedicamentos.TabIndex = 0;
            // 
            // gbxDatos
            // 
            this.gbxDatos.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.gbxDatos.Controls.Add(this.pnlInfoBasica);
            this.gbxDatos.Controls.Add(this.pnlInventario);
            this.gbxDatos.Controls.Add(this.pnlDetalles);
            this.gbxDatos.Controls.Add(this.pnlDatosAcciones);
            this.gbxDatos.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.gbxDatos.Location = new System.Drawing.Point(55, 318);
            this.gbxDatos.Name = "gbxDatos";
            this.gbxDatos.Size = new System.Drawing.Size(790, 310);
            this.gbxDatos.TabIndex = 10;
            this.gbxDatos.TabStop = false;
            this.gbxDatos.Text = "Detalles del Medicamento";
            // 
            // pnlInfoBasica
            // 
            this.pnlInfoBasica.Controls.Add(this.txtDescripcion);
            this.pnlInfoBasica.Controls.Add(this.lblDescripcion);
            this.pnlInfoBasica.Controls.Add(this.cboPresentacion);
            this.pnlInfoBasica.Controls.Add(this.cboMarca);
            this.pnlInfoBasica.Controls.Add(this.lblCodigo);
            this.pnlInfoBasica.Controls.Add(this.txtCodigo);
            this.pnlInfoBasica.Controls.Add(this.lblNombre);
            this.pnlInfoBasica.Controls.Add(this.txtNombre);
            this.pnlInfoBasica.Controls.Add(this.lblMarca);
            this.pnlInfoBasica.Controls.Add(this.lblPresentacion);
            this.pnlInfoBasica.Location = new System.Drawing.Point(10, 22);
            this.pnlInfoBasica.Name = "pnlInfoBasica";
            this.pnlInfoBasica.Size = new System.Drawing.Size(770, 101);
            this.pnlInfoBasica.TabIndex = 11;
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(3, 8);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(57, 19);
            this.lblCodigo.TabIndex = 0;
            this.lblCodigo.Text = "Código:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(90, 5);
            this.txtCodigo.MaxLength = 250;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(250, 26);
            this.txtCodigo.TabIndex = 1;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(3, 40);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(63, 19);
            this.lblNombre.TabIndex = 2;
            this.lblNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(90, 37);
            this.txtNombre.MaxLength = 500;
            this.txtNombre.Multiline = true;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(250, 26);
            this.txtNombre.TabIndex = 3;
            // 
            // lblMarca
            // 
            this.lblMarca.AutoSize = true;
            this.lblMarca.Location = new System.Drawing.Point(473, 8);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(52, 19);
            this.lblMarca.TabIndex = 4;
            this.lblMarca.Text = "Marca:";
            // 
            // lblPresentacion
            // 
            this.lblPresentacion.AutoSize = true;
            this.lblPresentacion.Location = new System.Drawing.Point(473, 60);
            this.lblPresentacion.Name = "lblPresentacion";
            this.lblPresentacion.Size = new System.Drawing.Size(89, 19);
            this.lblPresentacion.TabIndex = 6;
            this.lblPresentacion.Text = "Presentación:";
            // 
            // pnlInventario
            // 
            this.pnlInventario.Controls.Add(this.cboUnidadMedida);
            this.pnlInventario.Controls.Add(this.lblUnidadMedida);
            this.pnlInventario.Controls.Add(this.cboGrupoo);
            this.pnlInventario.Controls.Add(this.cboGrupo);
            this.pnlInventario.Controls.Add(this.lblStockActual);
            this.pnlInventario.Controls.Add(this.nudStockActual);
            this.pnlInventario.Controls.Add(this.lblFechaCaducidad);
            this.pnlInventario.Controls.Add(this.dtpFechaCaducidad);
            this.pnlInventario.Location = new System.Drawing.Point(10, 129);
            this.pnlInventario.Name = "pnlInventario";
            this.pnlInventario.Size = new System.Drawing.Size(308, 133);
            this.pnlInventario.TabIndex = 12;
            // 
            // lblStockActual
            // 
            this.lblStockActual.AutoSize = true;
            this.lblStockActual.Location = new System.Drawing.Point(3, 8);
            this.lblStockActual.Name = "lblStockActual";
            this.lblStockActual.Size = new System.Drawing.Size(90, 19);
            this.lblStockActual.TabIndex = 0;
            this.lblStockActual.Text = "Stock Actual:";
            // 
            // nudStockActual
            // 
            this.nudStockActual.Location = new System.Drawing.Point(130, 5);
            this.nudStockActual.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudStockActual.Name = "nudStockActual";
            this.nudStockActual.Size = new System.Drawing.Size(140, 26);
            this.nudStockActual.TabIndex = 1;
            // 
            // lblFechaCaducidad
            // 
            this.lblFechaCaducidad.AutoSize = true;
            this.lblFechaCaducidad.Location = new System.Drawing.Point(3, 35);
            this.lblFechaCaducidad.Name = "lblFechaCaducidad";
            this.lblFechaCaducidad.Size = new System.Drawing.Size(119, 19);
            this.lblFechaCaducidad.TabIndex = 4;
            this.lblFechaCaducidad.Text = "Fecha Caducidad:";
            // 
            // dtpFechaCaducidad
            // 
            this.dtpFechaCaducidad.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaCaducidad.Location = new System.Drawing.Point(128, 33);
            this.dtpFechaCaducidad.Name = "dtpFechaCaducidad";
            this.dtpFechaCaducidad.ShowCheckBox = true;
            this.dtpFechaCaducidad.Size = new System.Drawing.Size(139, 26);
            this.dtpFechaCaducidad.TabIndex = 5;
            // 
            // pnlDetalles
            // 
            this.pnlDetalles.Controls.Add(this.txtClasificacionATK);
            this.pnlDetalles.Controls.Add(this.lblClasificacionATK);
            this.pnlDetalles.Controls.Add(this.lblCategoria);
            this.pnlDetalles.Controls.Add(this.cbxCategoria);
            this.pnlDetalles.Controls.Add(this.lblPrecioVenta);
            this.pnlDetalles.Controls.Add(this.nudPrecioVenta);
            this.pnlDetalles.Controls.Add(this.lblEstado);
            this.pnlDetalles.Controls.Add(this.cbxEstado);
            this.pnlDetalles.Location = new System.Drawing.Point(336, 129);
            this.pnlDetalles.Name = "pnlDetalles";
            this.pnlDetalles.Size = new System.Drawing.Size(444, 133);
            this.pnlDetalles.TabIndex = 13;
            // 
            // lblCategoria
            // 
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Location = new System.Drawing.Point(3, 8);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(71, 19);
            this.lblCategoria.TabIndex = 0;
            this.lblCategoria.Text = "Categoría:";
            // 
            // cbxCategoria
            // 
            this.cbxCategoria.FormattingEnabled = true;
            this.cbxCategoria.Location = new System.Drawing.Point(130, 3);
            this.cbxCategoria.Name = "cbxCategoria";
            this.cbxCategoria.Size = new System.Drawing.Size(200, 27);
            this.cbxCategoria.TabIndex = 1;
            // 
            // lblPrecioVenta
            // 
            this.lblPrecioVenta.AutoSize = true;
            this.lblPrecioVenta.Location = new System.Drawing.Point(3, 37);
            this.lblPrecioVenta.Name = "lblPrecioVenta";
            this.lblPrecioVenta.Size = new System.Drawing.Size(108, 19);
            this.lblPrecioVenta.TabIndex = 2;
            this.lblPrecioVenta.Text = "Precio de Venta:";
            // 
            // nudPrecioVenta
            // 
            this.nudPrecioVenta.Location = new System.Drawing.Point(130, 33);
            this.nudPrecioVenta.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudPrecioVenta.Name = "nudPrecioVenta";
            this.nudPrecioVenta.Size = new System.Drawing.Size(113, 26);
            this.nudPrecioVenta.TabIndex = 3;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(4, 98);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(54, 19);
            this.lblEstado.TabIndex = 4;
            this.lblEstado.Text = "Estado:";
            // 
            // cbxEstado
            // 
            this.cbxEstado.FormattingEnabled = true;
            this.cbxEstado.Items.AddRange(new object[] {
            "Activo",
            "Inactivo"});
            this.cbxEstado.Location = new System.Drawing.Point(130, 98);
            this.cbxEstado.Name = "cbxEstado";
            this.cbxEstado.Size = new System.Drawing.Size(95, 27);
            this.cbxEstado.TabIndex = 5;
            // 
            // pnlDatosAcciones
            // 
            this.pnlDatosAcciones.Controls.Add(this.btnGuardar);
            this.pnlDatosAcciones.Controls.Add(this.btnCancelar);
            this.pnlDatosAcciones.Location = new System.Drawing.Point(10, 268);
            this.pnlDatosAcciones.Name = "pnlDatosAcciones";
            this.pnlDatosAcciones.Size = new System.Drawing.Size(770, 36);
            this.pnlDatosAcciones.TabIndex = 14;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnGuardar.Location = new System.Drawing.Point(520, 0);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(95, 36);
            this.btnGuardar.TabIndex = 15;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click_1);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnCancelar.Location = new System.Drawing.Point(625, 0);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(95, 36);
            this.btnCancelar.TabIndex = 16;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // cboMarca
            // 
            this.cboMarca.FormattingEnabled = true;
            this.cboMarca.Location = new System.Drawing.Point(570, 9);
            this.cboMarca.Name = "cboMarca";
            this.cboMarca.Size = new System.Drawing.Size(169, 27);
            this.cboMarca.TabIndex = 8;
            // 
            // cboPresentacion
            // 
            this.cboPresentacion.FormattingEnabled = true;
            this.cboPresentacion.Location = new System.Drawing.Point(570, 57);
            this.cboPresentacion.Name = "cboPresentacion";
            this.cboPresentacion.Size = new System.Drawing.Size(169, 27);
            this.cboPresentacion.TabIndex = 9;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(3, 77);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(84, 19);
            this.lblDescripcion.TabIndex = 10;
            this.lblDescripcion.Text = "Descripcion:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(90, 74);
            this.txtDescripcion.MaxLength = 500;
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(250, 26);
            this.txtDescripcion.TabIndex = 11;
            // 
            // cboGrupo
            // 
            this.cboGrupo.AutoSize = true;
            this.cboGrupo.Location = new System.Drawing.Point(3, 66);
            this.cboGrupo.Name = "cboGrupo";
            this.cboGrupo.Size = new System.Drawing.Size(51, 19);
            this.cboGrupo.TabIndex = 6;
            this.cboGrupo.Text = "Grupo:";
            // 
            // cboGrupoo
            // 
            this.cboGrupoo.FormattingEnabled = true;
            this.cboGrupoo.Location = new System.Drawing.Point(130, 63);
            this.cboGrupoo.Name = "cboGrupoo";
            this.cboGrupoo.Size = new System.Drawing.Size(140, 27);
            this.cboGrupoo.TabIndex = 7;
            // 
            // lblUnidadMedida
            // 
            this.lblUnidadMedida.AutoSize = true;
            this.lblUnidadMedida.Location = new System.Drawing.Point(-2, 98);
            this.lblUnidadMedida.Name = "lblUnidadMedida";
            this.lblUnidadMedida.Size = new System.Drawing.Size(126, 19);
            this.lblUnidadMedida.TabIndex = 8;
            this.lblUnidadMedida.Text = "Unidad de Medida:";
            // 
            // cboUnidadMedida
            // 
            this.cboUnidadMedida.FormattingEnabled = true;
            this.cboUnidadMedida.Location = new System.Drawing.Point(130, 98);
            this.cboUnidadMedida.Name = "cboUnidadMedida";
            this.cboUnidadMedida.Size = new System.Drawing.Size(140, 27);
            this.cboUnidadMedida.TabIndex = 9;
            // 
            // lblClasificacionATK
            // 
            this.lblClasificacionATK.AutoSize = true;
            this.lblClasificacionATK.Location = new System.Drawing.Point(3, 66);
            this.lblClasificacionATK.Name = "lblClasificacionATK";
            this.lblClasificacionATK.Size = new System.Drawing.Size(118, 19);
            this.lblClasificacionATK.TabIndex = 8;
            this.lblClasificacionATK.Text = "Clasificacion ATK";
            // 
            // txtClasificacionATK
            // 
            this.txtClasificacionATK.Location = new System.Drawing.Point(130, 63);
            this.txtClasificacionATK.Name = "txtClasificacionATK";
            this.txtClasificacionATK.Size = new System.Drawing.Size(224, 26);
            this.txtClasificacionATK.TabIndex = 9;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnCerrar.Location = new System.Drawing.Point(497, 3);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(87, 42);
            this.btnCerrar.TabIndex = 9;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Enabled = false;
            this.btnEliminar.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnEliminar.Location = new System.Drawing.Point(400, 3);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(93, 42);
            this.btnEliminar.TabIndex = 8;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click_1);
            // 
            // btnEditar
            // 
            this.btnEditar.Enabled = false;
            this.btnEditar.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnEditar.Location = new System.Drawing.Point(307, 3);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(87, 42);
            this.btnEditar.TabIndex = 7;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click_1);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnNuevo.Location = new System.Drawing.Point(210, 3);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(91, 42);
            this.btnNuevo.TabIndex = 6;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click_1);
            // 
            // pnlAcciones
            // 
            this.pnlAcciones.Controls.Add(this.btnNuevo);
            this.pnlAcciones.Controls.Add(this.btnEditar);
            this.pnlAcciones.Controls.Add(this.btnEliminar);
            this.pnlAcciones.Controls.Add(this.btnCerrar);
            this.pnlAcciones.Location = new System.Drawing.Point(55, 267);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Size = new System.Drawing.Size(789, 48);
            this.pnlAcciones.TabIndex = 5;
            // 
            // FrmMedicamento
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(893, 633);
            this.Controls.Add(this.gbxDatos);
            this.Controls.Add(this.pnlAcciones);
            this.Controls.Add(this.gbxLista);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtParametro);
            this.Controls.Add(this.lblBusqueda);
            this.Controls.Add(this.lblPrincipal);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FrmMedicamento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de Medicamentos";
            this.Load += new System.EventHandler(this.FrmMedicamento_Load);
            ((System.ComponentModel.ISupportInitialize)(this.erpCategoria)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpMarca)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpDescripcion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpCodigo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpPrecioVenta)).EndInit();
            this.gbxLista.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaMedicamentos)).EndInit();
            this.gbxDatos.ResumeLayout(false);
            this.pnlInfoBasica.ResumeLayout(false);
            this.pnlInfoBasica.PerformLayout();
            this.pnlInventario.ResumeLayout(false);
            this.pnlInventario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStockActual)).EndInit();
            this.pnlDetalles.ResumeLayout(false);
            this.pnlDetalles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecioVenta)).EndInit();
            this.pnlDatosAcciones.ResumeLayout(false);
            this.pnlAcciones.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        // Campos
        private ErrorProvider erpCategoria;
        private ErrorProvider erpMarca;
        private ErrorProvider erpDescripcion;
        private ErrorProvider erpCodigo;
        private ErrorProvider erpPrecioVenta;

        private Label lblPrincipal;
        private Label lblBusqueda;
        private TextBox txtParametro;
        private Button btnBuscar;
        private GroupBox gbxLista;
        private DataGridView dgvListaMedicamentos;

        private GroupBox gbxDatos;
        private Panel pnlInfoBasica;
        private Label lblCodigo;
        private TextBox txtCodigo;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblMarca;
        private Label lblPresentacion;

        private Panel pnlInventario;
        private Label lblStockActual;
        private NumericUpDown nudStockActual;
        private Label lblFechaCaducidad;
        private DateTimePicker dtpFechaCaducidad;

        private Panel pnlDetalles;
        private Label lblCategoria;
        private ComboBox cbxCategoria;
        private Label lblPrecioVenta;
        private NumericUpDown nudPrecioVenta;
        private Label lblEstado;
        private ComboBox cbxEstado;

        private Panel pnlDatosAcciones;
        private Button btnGuardar;
        private Button btnCancelar;
        private ComboBox cboPresentacion;
        private ComboBox cboMarca;
        private Label lblDescripcion;
        private TextBox txtDescripcion;
        private ComboBox cboGrupoo;
        private Label cboGrupo;
        private ComboBox cboUnidadMedida;
        private Label lblUnidadMedida;
        private TextBox txtClasificacionATK;
        private Label lblClasificacionATK;
        private Panel pnlAcciones;
        private Button btnNuevo;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnCerrar;
    }
}
