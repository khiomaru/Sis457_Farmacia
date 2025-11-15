using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json; // Necesitas instalar NuGet: Install-Package Newtonsoft.Json

namespace CpFarmacia2024
{
    public partial class FrmPaciente : Form
    {
        private bool esNuevo = false;
        private bool hayCambios = false;
        private List<Paciente> listaPacientes = new List<Paciente>();
        private BindingSource bsPacientes = new BindingSource();
        private Paciente pacienteEnEdicion = null;
        private Dictionary<string, List<string>> _planesPorObra = new Dictionary<string, List<string>>();

        // Ruta del archivo de datos
        private readonly string _rutaArchivoDatos = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "CpFarmacia2024",
            "pacientes.json"
        );

        public FrmPaciente()
        {
            InitializeComponent();
            ConfigurarDataGrid();
            ConfigurarEventos();
            CargarListas();
            CargarDatos(); // Cargar datos guardados
            DesactivarCampos();
            gbLISTA_PACIENTES.Enabled = true;
            btnAGREGAR.Enabled = true;
            btnBUSCAR.Enabled = true;
            btnCERRAR.Enabled = true;
            Listar();
        }

        private void ConfigurarDataGrid()
        {
            dgvLISTA_PACIENTES.AutoGenerateColumns = true;
            dgvLISTA_PACIENTES.DataSource = bsPacientes;
            dgvLISTA_PACIENTES.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLISTA_PACIENTES.MultiSelect = false;
        }

        private void ConfigurarEventos()
        {
            btnAGREGAR.Click -= btnAGREGAR_Click; btnAGREGAR.Click += btnAGREGAR_Click;
            btnGUARDAR.Click -= btnGUARDAR_Click; btnGUARDAR.Click += btnGUARDAR_Click;
            btnCANCELAR.Click -= btnCANCELAR_Click; btnCANCELAR.Click += btnCANCELAR_Click;
            btnMODIFICAR.Click -= btnMODIFICAR_Click; btnMODIFICAR.Click += btnMODIFICAR_Click;
            btnELIMINAR.Click -= btnELIMINAR_Click; btnELIMINAR.Click += btnELIMINAR_Click;
            btnBUSCAR.Click -= btnBUSCAR_Click; btnBUSCAR.Click += btnBUSCAR_Click;
            btnCERRAR.Click -= btnCERRAR_Click; btnCERRAR.Click += btnCERRAR_Click;
            btnCONSULTAR.Click -= btnCONSULTAR_Click; btnCONSULTAR.Click += btnCONSULTAR_Click;

            dgvLISTA_PACIENTES.SelectionChanged -= dgvLISTA_PACIENTES_SelectionChanged;
            dgvLISTA_PACIENTES.SelectionChanged += dgvLISTA_PACIENTES_SelectionChanged;

            dgvLISTA_PACIENTES.CellDoubleClick -= dgvLISTA_PACIENTES_CellDoubleClick;
            dgvLISTA_PACIENTES.CellDoubleClick += dgvLISTA_PACIENTES_CellDoubleClick;

            cmbOBRA_SOCIAL.SelectedIndexChanged -= cmbOBRA_SOCIAL_SelectedIndexChanged;
            cmbOBRA_SOCIAL.SelectedIndexChanged += cmbOBRA_SOCIAL_SelectedIndexChanged;

            cmbFILTRO_OBRA_SOCIAL.SelectedIndexChanged -= cmbFILTRO_OBRA_SOCIAL_SelectedIndexChanged;
            cmbFILTRO_OBRA_SOCIAL.SelectedIndexChanged += cmbFILTRO_OBRA_SOCIAL_SelectedIndexChanged;

            txtBUSCAR.TextChanged -= txtBUSCAR_TextChanged;
            txtBUSCAR.TextChanged += txtBUSCAR_TextChanged;

            txtNOMBRE.TextChanged -= txtNOMBRE_TextChanged; txtNOMBRE.TextChanged += txtNOMBRE_TextChanged;
            txtAPELLIDO.TextChanged -= txtAPELLIDO_TextChanged; txtAPELLIDO.TextChanged += txtAPELLIDO_TextChanged;
            txtCONTACTO.TextChanged -= txtCONTACTO_TextChanged; txtCONTACTO.TextChanged += txtCONTACTO_TextChanged;
            txtEMAIL.TextChanged -= txtEMAIL_TextChanged; txtEMAIL.TextChanged += txtEMAIL_TextChanged;
            nudEdad.ValueChanged -= nudEdad_ValueChanged; nudEdad.ValueChanged += nudEdad_ValueChanged;
            cmbPLAN.SelectedIndexChanged -= cmbPLAN_SelectedIndexChanged; cmbPLAN.SelectedIndexChanged += cmbPLAN_SelectedIndexChanged;

            var nudDesde = this.Controls.Find("nudEdadDesde", true).FirstOrDefault() as NumericUpDown;
            var nudHasta = this.Controls.Find("nudEdadHasta", true).FirstOrDefault() as NumericUpDown;
            if (nudDesde != null)
            {
                nudDesde.ValueChanged -= NudEdadRange_ValueChanged;
                nudDesde.ValueChanged += NudEdadRange_ValueChanged;
            }
            if (nudHasta != null)
            {
                nudHasta.ValueChanged -= NudEdadRange_ValueChanged;
                nudHasta.ValueChanged += NudEdadRange_ValueChanged;
            }
        }

        private void NudEdadRange_ValueChanged(object sender, EventArgs e) => AplicarFiltroBusquedaAvanzada();

        private void CargarListas()
        {
            var obras = new List<string> { "Particular", "ObraSocial A", "ObraSocial B" };
            cmbOBRA_SOCIAL.Items.Clear();
            cmbFILTRO_OBRA_SOCIAL.Items.Clear();
            foreach (var o in obras)
            {
                cmbOBRA_SOCIAL.Items.Add(o);
                cmbFILTRO_OBRA_SOCIAL.Items.Add(o);
            }

            _planesPorObra = new Dictionary<string, List<string>>
            {
                { "Particular", new List<string> { "Sin Plan" } },
                { "ObraSocial A", new List<string> { "Plan A1", "Plan A2" } },
                { "ObraSocial B", new List<string> { "Plan B1", "Plan B2", "Plan B3" } }
            };
        }

        // ===== PERSISTENCIA DE DATOS =====
        private void CargarDatos()
        {
            try
            {
                if (File.Exists(_rutaArchivoDatos))
                {
                    string json = File.ReadAllText(_rutaArchivoDatos);
                    listaPacientes = JsonConvert.DeserializeObject<List<Paciente>>(json) ?? new List<Paciente>();
                }
                else
                {
                    // Datos iniciales si no existe archivo
                    listaPacientes = new List<Paciente>
                    {
                        new Paciente { Id = 1, Nombre = "Juan", Apellido = "Pérez", Contacto = "12345678", Edad = 30, Email = "juan@example.com", ObraSocial = "Particular", Plan = "Sin Plan" },
                        new Paciente { Id = 2, Nombre = "María", Apellido = "Gómez", Contacto = "87654321", Edad = 45, Email = "maria@example.com", ObraSocial = "ObraSocial A", Plan = "Plan A1" }
                    };
                    GuardarDatos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                listaPacientes = new List<Paciente>();
            }
        }

        private void GuardarDatos()
        {
            try
            {
                string directorio = Path.GetDirectoryName(_rutaArchivoDatos);
                if (!Directory.Exists(directorio))
                    Directory.CreateDirectory(directorio);

                string json = JsonConvert.SerializeObject(listaPacientes, Formatting.Indented);
                File.WriteAllText(_rutaArchivoDatos, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== MÉTODOS FALTANTES =====
        private void CargarPlanesParaObraSeleccionada()
        {
            cmbPLAN.Items.Clear();
            var obraSeleccionada = cmbOBRA_SOCIAL.SelectedItem as string;

            if (!string.IsNullOrEmpty(obraSeleccionada) && _planesPorObra.ContainsKey(obraSeleccionada))
            {
                foreach (var plan in _planesPorObra[obraSeleccionada])
                {
                    cmbPLAN.Items.Add(plan);
                }
            }
        }

        private string ConstruirDetallePaciente(object obj)
        {
            var p = obj as Paciente;
            if (p == null) return "Paciente no válido";

            var sb = new StringBuilder();
            sb.AppendLine("═══════════════════════════════════");
            sb.AppendLine($"ID: {p.Id}");
            sb.AppendLine($"Nombre: {p.Nombre}");
            sb.AppendLine($"Apellido: {p.Apellido}");
            sb.AppendLine($"Contacto: {p.Contacto}");
            sb.AppendLine($"Edad: {p.Edad} años");
            sb.AppendLine($"Email: {(p.Email ?? "No especificado")}");
            sb.AppendLine($"Obra Social: {(p.ObraSocial ?? "No especificada")}");
            sb.AppendLine($"Plan: {(p.Plan ?? "No especificado")}");
            sb.AppendLine("═══════════════════════════════════");

            return sb.ToString();
        }
                
        // ===== UTIL: mostrar/ocultar panel de datos y ajustar botones/Accept/Cancel =====
        private void MostrarPanelDatos(bool mostrar)
        {
            // Habilita/deshabilita el grupo de datos y lista, ajusta botones y teclas Enter/Esc
            gbDATOS_PACIENTE.Enabled = mostrar;
            gbLISTA_PACIENTES.Enabled = !mostrar;

            // Asegurar visibilidad y foco
            if (mostrar)
            {
                gbDATOS_PACIENTE.BringToFront();
                txtNOMBRE.Focus();
                this.AcceptButton = btnGUARDAR;
                this.CancelButton = btnCANCELAR;
            }
            else
            {
                // Restaurar comportamiento por defecto
                this.AcceptButton = null;
                this.CancelButton = null;
                gbLISTA_PACIENTES.BringToFront();
            }
        }

        // ===== EVENT HANDLERS FALTANTES / MEJORAS =====
        private void cmbOBRA_SOCIAL_SelectedIndexChanged(object sender, EventArgs e)
        {
            hayCambios = true;
            CargarPlanesParaObraSeleccionada();
            cmbPLAN.SelectedIndex = -1;
        }

        private void cmbFILTRO_OBRA_SOCIAL_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltroBusquedaAvanzada();
        }

        // ===== MÉTODOS EXISTENTES (CON MEJORAS) =====
        private void Listar()
        {
            bsPacientes.DataSource = null;
            bsPacientes.DataSource = listaPacientes;
            bsPacientes.ResetBindings(false);

            if (dgvLISTA_PACIENTES.Columns.Contains("Id"))
                dgvLISTA_PACIENTES.Columns["Id"].Visible = false;

            var hay = listaPacientes.Count > 0;
            btnMODIFICAR.Enabled = btnELIMINAR.Enabled = btnCONSULTAR.Enabled = hay && dgvLISTA_PACIENTES.SelectedRows.Count > 0;
        }

        private void DesactivarCampos() => SetCamposEnabled(false);
        private void HabilitarCampos() => SetCamposEnabled(true);

        private void SetCamposEnabled(bool estado)
        {
            txtNOMBRE.Enabled = estado;
            txtAPELLIDO.Enabled = estado;
            txtCONTACTO.Enabled = estado;
            nudEdad.Enabled = estado;
            cmbOBRA_SOCIAL.Enabled = estado;
            cmbPLAN.Enabled = estado;
            txtEMAIL.Enabled = estado;
            btnGUARDAR.Enabled = estado;
            btnCANCELAR.Enabled = estado;
        }

        private bool Validar()
        {
            bool valido = true;
            LimpiarErrores();

            valido &= ValidarCampo(txtNOMBRE, erpNombre, "Nombre obligatorio");
            valido &= ValidarCampo(txtAPELLIDO, erpApellido, "Apellido obligatorio");
            valido &= ValidarCampo(txtCONTACTO, erpDocumento, "Contacto obligatorio");

            if (!string.IsNullOrWhiteSpace(txtEMAIL.Text) && !EsEmailValido(txtEMAIL.Text))
            {
                erpEMAIL.SetError(txtEMAIL, "Email inválido");
                valido = false;
            }

            return valido;
        }

        private bool ValidarCampo(Control ctrl, ErrorProvider erp, string mensaje)
        {
            if (string.IsNullOrWhiteSpace(ctrl.Text))
            {
                erp.SetError(ctrl, mensaje);
                return false;
            }
            return true;
        }

        private void LimpiarErrores()
        {
            erpNombre.SetError(txtNOMBRE, "");
            erpApellido.SetError(txtAPELLIDO, "");
            erpDocumento.SetError(txtCONTACTO, "");
            erpEMAIL.SetError(txtEMAIL, "");
        }

        private void LimpiarCampos()
        {
            txtNOMBRE.Clear();
            txtAPELLIDO.Clear();
            txtCONTACTO.Clear();
            txtEMAIL.Clear();
            nudEdad.Value = 0;
            cmbOBRA_SOCIAL.SelectedIndex = -1;
            cmbPLAN.Items.Clear();
            cmbPLAN.SelectedIndex = -1;
            LimpiarErrores();
            hayCambios = false;
        }

        private void btnAGREGAR_Click(object sender, EventArgs e)
        {
            if (hayCambios)
            {
                if (MessageBox.Show("Hay cambios sin guardar. ¿Desea descartarlos y crear un nuevo paciente?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }

            esNuevo = true;
            pacienteEnEdicion = null;
            LimpiarCampos();
            HabilitarCampos();

            // Mostrar panel de datos y ajustar comportamiento de botones
            MostrarPanelDatos(true);

            btnAGREGAR.Enabled = false;
            btnMODIFICAR.Enabled = false;
            btnELIMINAR.Enabled = false;
            btnCONSULTAR.Enabled = false;
            txtNOMBRE.Focus();
        }

        private void btnGUARDAR_Click(object sender, EventArgs e)
        {
            if (!Validar()) return;

            try
            {
                if (esNuevo)
                {
                    var nuevo = new Paciente
                    {
                        Id = listaPacientes.Count > 0 ? listaPacientes.Max(x => x.Id) + 1 : 1,
                        Nombre = txtNOMBRE.Text.Trim(),
                        Apellido = txtAPELLIDO.Text.Trim(),
                        Contacto = txtCONTACTO.Text.Trim(),
                        Edad = (int)nudEdad.Value,
                        Email = txtEMAIL.Text.Trim(),
                        ObraSocial = cmbOBRA_SOCIAL.SelectedItem as string ?? string.Empty,
                        Plan = cmbPLAN.SelectedItem as string ?? string.Empty
                    };
                    listaPacientes.Add(nuevo);
                    GuardarDatos();
                    Listar();
                    var posNuevo = listaPacientes.FindIndex(p => p.Id == nuevo.Id);
                    if (posNuevo >= 0) bsPacientes.Position = posNuevo;
                    MessageBox.Show("Paciente agregado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (pacienteEnEdicion == null)
                    {
                        MessageBox.Show("No hay paciente en edición.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    pacienteEnEdicion.Nombre = txtNOMBRE.Text.Trim();
                    pacienteEnEdicion.Apellido = txtAPELLIDO.Text.Trim();
                    pacienteEnEdicion.Contacto = txtCONTACTO.Text.Trim();
                    pacienteEnEdicion.Edad = (int)nudEdad.Value;
                    pacienteEnEdicion.Email = txtEMAIL.Text.Trim();
                    pacienteEnEdicion.ObraSocial = cmbOBRA_SOCIAL.SelectedItem as string ?? string.Empty;
                    pacienteEnEdicion.Plan = cmbPLAN.SelectedItem as string ?? string.Empty;

                    GuardarDatos();
                    Listar();
                    var posEditado = listaPacientes.FindIndex(p => p.Id == pacienteEnEdicion.Id);
                    if (posEditado >= 0) bsPacientes.Position = posEditado;
                    MessageBox.Show("Paciente modificado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                pacienteEnEdicion = null;
                esNuevo = false;
                hayCambios = false;
                LimpiarCampos();
                DesactivarCampos();

                // Ocultar panel de datos y regresar a lista
                MostrarPanelDatos(false);

                gbLISTA_PACIENTES.Enabled = true;
                btnAGREGAR.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMODIFICAR_Click(object sender, EventArgs e)
        {
            var seleccionado = dgvLISTA_PACIENTES.CurrentRow?.DataBoundItem as Paciente;
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione un paciente para modificar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (hayCambios)
            {
                if (MessageBox.Show("Hay cambios sin guardar. ¿Desea descartarlos?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }

            esNuevo = false;
            pacienteEnEdicion = seleccionado;

            txtNOMBRE.Text = seleccionado.Nombre;
            txtAPELLIDO.Text = seleccionado.Apellido;
            txtCONTACTO.Text = seleccionado.Contacto;
            txtEMAIL.Text = seleccionado.Email ?? string.Empty;

            if (!string.IsNullOrEmpty(seleccionado.ObraSocial) && cmbOBRA_SOCIAL.Items.Contains(seleccionado.ObraSocial))
                cmbOBRA_SOCIAL.SelectedItem = seleccionado.ObraSocial;
            else
                cmbOBRA_SOCIAL.SelectedIndex = -1;

            CargarPlanesParaObraSeleccionada();

            if (!string.IsNullOrEmpty(seleccionado.Plan) && cmbPLAN.Items.Contains(seleccionado.Plan))
                cmbPLAN.SelectedItem = seleccionado.Plan;
            else
                cmbPLAN.SelectedIndex = -1;

            nudEdad.Value = seleccionado.Edad < nudEdad.Minimum ? nudEdad.Minimum :
                            (seleccionado.Edad > nudEdad.Maximum ? nudEdad.Maximum : seleccionado.Edad);

            HabilitarCampos();
            hayCambios = false;

            // Mostrar panel de datos y ajustar comportamiento de botones
            MostrarPanelDatos(true);

            btnAGREGAR.Enabled = false;
            txtNOMBRE.Focus();
        }

        private void btnELIMINAR_Click(object sender, EventArgs e)
        {
            var seleccionado = dgvLISTA_PACIENTES.CurrentRow?.DataBoundItem as Paciente;
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione un paciente para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show($"¿Desea eliminar a {seleccionado.Nombre} {seleccionado.Apellido}?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                var idx = listaPacientes.FindIndex(p => p.Id == seleccionado.Id);
                if (idx >= 0)
                {
                    listaPacientes.RemoveAt(idx);
                    GuardarDatos();
                    Listar();
                    MessageBox.Show("Paciente eliminado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    hayCambios = false;
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el paciente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCONSULTAR_Click(object sender, EventArgs e)
        {
            var seleccionadoObj = dgvLISTA_PACIENTES.CurrentRow?.DataBoundItem;
            if (seleccionadoObj == null)
            {
                MessageBox.Show("Seleccione un paciente para consultar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var detalle = ConstruirDetallePaciente(seleccionadoObj);
            MessageBox.Show(detalle, "Detalle del paciente", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCERRAR_Click(object sender, EventArgs e)
        {
            if (hayCambios)
            {
                var res = MessageBox.Show("Hay cambios sin guardar. ¿Desea salir y perder los cambios?", "Confirmar salida", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res != DialogResult.Yes) return;
            }
            Close();
        }

        private void btnCANCELAR_Click(object sender, EventArgs e)
        {
            if (hayCambios)
            {
                if (MessageBox.Show("¿Descartar los cambios realizados?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }

            pacienteEnEdicion = null;
            esNuevo = false;
            hayCambios = false;
            LimpiarCampos();
            DesactivarCampos();

            // Ocultar panel de datos y regresar a lista
            MostrarPanelDatos(false);

            gbLISTA_PACIENTES.Enabled = true;
            btnAGREGAR.Enabled = true;
        }

        private void dgvLISTA_PACIENTES_SelectionChanged(object sender, EventArgs e)
        {
            var seleccionado = dgvLISTA_PACIENTES.CurrentRow?.DataBoundItem as Paciente;
            var tieneSeleccion = seleccionado != null;
            btnMODIFICAR.Enabled = btnELIMINAR.Enabled = btnCONSULTAR.Enabled = tieneSeleccion;
        }

        private void dgvLISTA_PACIENTES_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                btnMODIFICAR_Click(sender, EventArgs.Empty);
        }

        private void txtBUSCAR_TextChanged(object sender, EventArgs e) => AplicarFiltroBusquedaAvanzada();
        private void btnBUSCAR_Click(object sender, EventArgs e) => AplicarFiltroBusquedaAvanzada();

        private void AplicarFiltroBusquedaAvanzada()
        {
            var termino = (txtBUSCAR.Text ?? string.Empty).Trim();
            var tokens = termino.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            IEnumerable<Paciente> resultado = listaPacientes;

            if (tokens.Length > 0)
                resultado = resultado.Where(p => TokensCumplen(p, tokens));

            if (cmbFILTRO_OBRA_SOCIAL.SelectedIndex >= 0)
            {
                var obra = cmbFILTRO_OBRA_SOCIAL.SelectedItem as string;
                if (!string.IsNullOrWhiteSpace(obra))
                    resultado = resultado.Where(p => string.Equals(p.ObraSocial ?? string.Empty, obra, StringComparison.OrdinalIgnoreCase));
            }

            try
            {
                var ctrlDesde = this.Controls.Find("nudEdadDesde", true).FirstOrDefault() as NumericUpDown;
                var ctrlHasta = this.Controls.Find("nudEdadHasta", true).FirstOrDefault() as NumericUpDown;
                if (ctrlDesde != null || ctrlHasta != null)
                {
                    int desde = ctrlDesde != null ? (int)ctrlDesde.Value : int.MinValue;
                    int hasta = ctrlHasta != null ? (int)ctrlHasta.Value : int.MaxValue;
                    bool aplicar = (ctrlDesde != null && ctrlDesde.Value > 0) || (ctrlHasta != null && ctrlHasta.Value > 0);
                    if (aplicar)
                    {
                        resultado = resultado.Where(p =>
                        {
                            var edad = p != null ? p.Edad : 0;
                            return edad >= desde && edad <= hasta;
                        });
                    }
                }
            }
            catch { }

            var listaFinal = resultado.ToList();
            bsPacientes.DataSource = null;
            bsPacientes.DataSource = listaFinal;
            bsPacientes.ResetBindings(false);
        }

        private bool TokensCumplen(Paciente p, string[] tokens)
        {
            foreach (var token in tokens)
            {
                if (token.StartsWith("edad:", StringComparison.OrdinalIgnoreCase))
                {
                    if (!EvaluaTokenEdad(token.Substring(5), p.Edad))
                        return false;
                }
                else
                {
                    if (!CoincideTokenEnCampos(p, token))
                        return false;
                }
            }
            return true;
        }

        private bool EvaluaTokenEdad(string expr, int edad)
        {
            expr = expr.Trim();
            if (string.IsNullOrEmpty(expr)) return true;

            if (expr.Contains("-"))
            {
                var partes = expr.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                int min, max;
                if (partes.Length == 2 && int.TryParse(partes[0], out min) && int.TryParse(partes[1], out max))
                    return edad >= min && edad <= max;
                return false;
            }

            if (expr.StartsWith(">="))
            {
                int n; if (int.TryParse(expr.Substring(2), out n)) return edad >= n;
                return false;
            }
            if (expr.StartsWith(">"))
            {
                int n; if (int.TryParse(expr.Substring(1), out n)) return edad > n;
                return false;
            }
            if (expr.StartsWith("<="))
            {
                int n; if (int.TryParse(expr.Substring(2), out n)) return edad <= n;
                return false;
            }
            if (expr.StartsWith("<"))
            {
                int n; if (int.TryParse(expr.Substring(1), out n)) return edad < n;
                return false;
            }

            int val;
            if (int.TryParse(expr, out val)) return edad == val;
            return false;
        }

        private bool CoincideTokenEnCampos(object obj, string token)
        {
            if (obj == null) return false;
            token = token.ToLowerInvariant();

            var p = obj as Paciente;
            if (p != null)
            {
                if (!string.IsNullOrEmpty(p.Nombre) && p.Nombre.ToLowerInvariant().Contains(token)) return true;
                if (!string.IsNullOrEmpty(p.Apellido) && p.Apellido.ToLowerInvariant().Contains(token)) return true;
                if (!string.IsNullOrEmpty(p.Contacto) && p.Contacto.ToLowerInvariant().Contains(token)) return true;
                if (!string.IsNullOrEmpty(p.Email) && p.Email.ToLowerInvariant().Contains(token)) return true;
                if (!string.IsNullOrEmpty(p.ObraSocial) && p.ObraSocial.ToLowerInvariant().Contains(token)) return true;
                if (!string.IsNullOrEmpty(p.Plan) && p.Plan.ToLowerInvariant().Contains(token)) return true;
                if ((p.Edad.ToString()).Contains(token)) return true;
            }

            return false;
        }

        // ===== VALIDACIÓN MEJORADA DE EMAIL =====
        private bool EsEmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            try
            {
                // Expresión regular para validar email
                string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(email, patron);
            }
            catch
            {
                return false;
            }
        }

        // ===== EVENT HANDLERS DE CAMBIOS =====
        private void txtNOMBRE_TextChanged(object sender, EventArgs e)
        {
            erpNombre.SetError(txtNOMBRE, "");
            hayCambios = true;
        }

        private void txtAPELLIDO_TextChanged(object sender, EventArgs e)
        {
            erpApellido.SetError(txtAPELLIDO, "");
            hayCambios = true;
        }

        private void txtCONTACTO_TextChanged(object sender, EventArgs e)
        {
            erpDocumento.SetError(txtCONTACTO, "");
            hayCambios = true;
        }

        private void nudEdad_ValueChanged(object sender, EventArgs e)
        {
            hayCambios = true;
            var v = (int)nudEdad.Value;
            if (v < 0 || v > 130)
                MessageBox.Show("Edad fuera de rango (0-130).", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void txtEMAIL_TextChanged(object sender, EventArgs e)
        {
            erpEMAIL.SetError(txtEMAIL, "");
            hayCambios = true;
            var email = txtEMAIL.Text.Trim();
            if (!string.IsNullOrEmpty(email) && !EsEmailValido(email))
            {
                erpEMAIL.SetError(txtEMAIL, "Email inválido");
            }
        }

        private void cmbPLAN_SelectedIndexChanged(object sender, EventArgs e)
        {
            hayCambios = true;
        }

        private void lblNOMBRE_Click(object sender, EventArgs e) { txtNOMBRE.Focus(); }
        private void lblAPELLIDO_Click(object sender, EventArgs e) { txtAPELLIDO.Focus(); }
        private void lblCONTACTO_Click(object sender, EventArgs e) { txtCONTACTO.Focus(); }
        private void lblEDAD_Click(object sender, EventArgs e) { nudEdad.Focus(); }
        private void lblOBRA_SOCIAL_Click(object sender, EventArgs e) { cmbOBRA_SOCIAL.Focus(); }
        private void lblPLAN_Click(object sender, EventArgs e) { cmbPLAN.Focus(); }

        private void gbDATOS_PACIENTE_Enter(object sender, EventArgs e)
        {
            if (txtNOMBRE.Enabled) txtNOMBRE.Focus();
            else if (txtAPELLIDO.Enabled) txtAPELLIDO.Focus();
        }

        private void gbLISTA_PACIENTES_Enter(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtBUSCAR.Text)) txtBUSCAR.Focus();
            else if (dgvLISTA_PACIENTES.Enabled) dgvLISTA_PACIENTES.Focus();
        }

        private void dgvLISTA_PACIENTES_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvLISTA_PACIENTES.Rows[e.RowIndex].Selected = true;
                dgvLISTA_PACIENTES_SelectionChanged(sender, EventArgs.Empty);
            }
        }
    }

    public class Paciente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Contacto { get; set; }
        public int Edad { get; set; }
        public string Email { get; set; }
        public string ObraSocial { get; set; }
        public string Plan { get; set; }
    }
}