using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Control_EscolarCore.Bussines;
using Control_Escolar.Utilities;
using Control_EscolarCore.Controller;
using Control_EscolarCore.Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Control_Escolar.View
{
    public partial class frmEstudiantes : Form
    {
        public frmEstudiantes(Form parent)///CONSTRUIR
        {
            InitializeComponent();
            //InicializaForma(parent);//que se ejecute como una forma hija
            Formas.InicializaForma(this, parent);

        }

        private void frmEstudiantes_Load(object sender, EventArgs e)///CARGAR
        {
            InicializaVentanaEstudiantes();
        }

        private void InicializaVentanaEstudiantes()
        {
            PoblaComboFecha();
            PoblaComboEstatus();
            scEstudiantes.Panel1Collapsed = true;
            dtpFechaAlta.Value = DateTime.Now;
            CargarEstudiantes();
        }


        private void PoblaComboEstatus()
        {
            //Crear un diccionario con los valores
            Dictionary<int, string> list_estatus = new Dictionary<int, string>
            {   //key  //value
                { 1, "Activo" },
                { 0, "Baja" },
                { 2, "Baja Temporal" }
            };

            //Asignar el diccionario al comboBox
            cbxEstatus.DataSource = new BindingSource(list_estatus, null);
            cbxEstatus.DisplayMember = "Value";//Lo que se muestra(texto)
            cbxEstatus.ValueMember = "Key";//Lo que se guarda(numeros)
            cbxEstatus.SelectedValue = 1;//Con el que se inicializa

        }

        private void PoblaComboFecha()
        {
            //Crear un diccionario con los valores
            Dictionary<int, string> list_tipoFechas = new Dictionary<int, string>
            {   //key  //value
                { 1, "Nacimiento" },
                { 2, "Alta" },
                { 3, "Baja" }
            };

            //Asignar el diccionario al comboBox
            cbxTipoF.DataSource = new BindingSource(list_tipoFechas, null);
            cbxTipoF.DisplayMember = "Value";//Lo que se muestra(texto)
            cbxTipoF.ValueMember = "Key";//Lo que se guarda(numeros)
            cbxTipoF.SelectedValue = 1;//Con el que se inicializa
        }

        private void GuardarEstudiante()
        {
            try
            {
                if (DatosVacios())
                {
                    MessageBox.Show("Por favor, llene todos los campos.", "Informacion del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!DatosValidos())
                {
                    return;
                }

                //Crear el objeto persona con los datos del formaulario
                Persona persona = new Persona(
                    txtNombre.Text.Trim(),
                    txtCorreo.Text.Trim(),
                    txtTelefono.Text.Trim(),
                    txtCurp.Text.Trim()
                );

                //Asignarla fecha de nacimiento
                persona.FechaNacimiento = dtpFechaNacim.Value;

                //Crear el obejo Estudiante con los datos del formulario
                Estudiante estudiante = new Estudiante
                {
                    Matricula = txtNoControl.Text.Trim(),
                    Semestre = nudSemestre.Text.Trim(),
                    FechaAlta = dtpFechaAlta.Value,
                    Estatus = 1, //Activo por defecto
                    DatosPersonales = persona
                };

                //Crear intancia del controlador
                EstudiantesController estudiantesController = new EstudiantesController();

                //Llamar el metodo para registrar el estudiante utilizando el modelo
                var (idEstudiante, mensaje) = estudiantesController.RegistrarEstudiante(estudiante);

                //Verificar el resultado
                if (idEstudiante > 0)
                {
                    MessageBox.Show(mensaje, "Informacion del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos(); //Metodo para limpiar el formulario edspues de guardar

                    //Actualizar la lista de estudiantes si esta presente en la misma vista
                    CargarEstudiantes();
                }
                else
                {
                    //Mostrar mensaje de error devuelto por el controlador
                    MessageBox.Show(mensaje, "Informacion del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    //Enfocar el campo apropiado basado en el codigo de error
                    switch (idEstudiante)
                    {
                        case -2: //Error de curp duplicado
                            txtCurp.Focus();
                            txtCurp.SelectAll();
                            break;
                        case -3: //Error de matricula duplicada
                            txtNoControl.Focus();
                            txtNoControl.SelectAll();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                //El detalle del error ya se guardara en el log por el controlador
                MessageBox.Show("No se pudo completar el registro del estudiante.Por favor, intente nuevamente o contacte al administrador del sistema.",
                    "Informacion del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Actualiza los datos de un estudiante existente
        /// </summary>
        private void ActualizarEstudiante()
        {
            try
            {
                // Validaciones a nivel de interfaz
                if (DatosVacios())
                {
                    MessageBox.Show("Por favor, llene todos los campos.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!DatosValidos())
                {
                    return;
                }

                // Obtener el ID del estudiante almacenado en el Tag
                if (this.Tag == null || !(this.Tag is int))
                {
                    MessageBox.Show("No se ha seleccionado un estudiante para actualizar.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idEstudiante = (int)this.Tag;

                // Crear el objeto Persona con los datos del formulario
                Persona persona = new Persona
                {
                    Id = 0, // Se actualizará con el valor correcto en el controller
                    NombreCompleto = txtNombre.Text.Trim(),
                    Correo = txtCorreo.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Curp = txtCurp.Text.Trim(),
                    FechaNacimiento = dtpFechaNacim.Value,
                    Estatus = true // Asumimos que la persona está activa
                };

                // Crear el objeto Estudiante con los datos del formulario
                Estudiante estudiante = new Estudiante
                {
                    Id = idEstudiante,
                    IdPersona = 0, // Se actualizará con el valor correcto en el controller
                    Matricula = txtNoControl.Text.Trim(),
                    FechaAlta = dtpFechaAlta.Value,
                    FechaBaja = dtpFechaBaja.Value,
                    Estatus = cbxEstatus.SelectedValue != null ? (int)cbxEstatus.SelectedValue : 1, // 0=Baja, 1=Activo, 2=Baja Temporal
                    DatosPersonales = persona
                };


                // Asignar fecha de baja si corresponde
                if (cbxEstatus.SelectedIndex == 0) // Si el estatus es "Baja"
                {
                    estudiante.FechaBaja = dtpFechaBaja.Value;
                }
                else if (dtpFechaBaja.Enabled && cbxEstatus.SelectedIndex == 2) // Si es "Baja Temporal" y hay fecha
                {
                    estudiante.FechaBaja = dtpFechaBaja.Value;
                }
                else
                {
                    estudiante.FechaBaja = null;
                }

                // Crear instancia del controlador
                EstudiantesController estudiantesController = new EstudiantesController();

                // Llamar al método para actualizar el estudiante utilizando el modelo
                var (resultado, mensaje) = estudiantesController.ActualizarEstudiante(estudiante);

                // Verificar el resultado
                if (resultado)
                {
                    MessageBox.Show(mensaje, "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar formulario y restablecer modo
                    LimpiarCampos();
                    ModoEdicion(false);

                    // Actualizar la lista de estudiantes
                    CargarEstudiantes();
                }
                else
                {
                    // Mostrar mensaje de error devuelto por el controlador
                    MessageBox.Show(mensaje, "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                // El detalle del error ya se guardará en el log por el controlador
                MessageBox.Show("No se pudo completar la actualización del estudiante. Por favor, intente nuevamente o contacte al administrador del sistema.",
                                "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Obtiene los detalles del estudiante seleccionado y los muestra en el formulario.
        /// </summary>
        /// <param name="idEstudiante">ID del estudiante a obtener</param>
        private void ObtenerDetalleEstudiante(int idEstudiante)
        {
            try
            {
                // Llamar al controlador para obtener el estudiante
                EstudiantesController controller_estudiante = new EstudiantesController();
                Estudiante? estudiante = controller_estudiante.ObtenerDetalleEstudiante(idEstudiante);

                if (estudiante != null)
                {
                    // Poblar los controles con la información del estudiante
                    CargarDatosEstudiante(estudiante);

                    // Cambiar a modo de edición
                    ModoEdicion(true);
                }
                else
                {
                    MessageBox.Show("No se pudo obtener la información del estudiante.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener los detalles del estudiante: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Carga los datos del estudiante en los controles del formulario
        /// </summary>
        /// <param name="estudiante">Objeto Estudiante con los datos a mostrar</param>
        private void CargarDatosEstudiante(Estudiante estudiante)
        {
            // Datos personales
            txtNombre.Text = estudiante.DatosPersonales.NombreCompleto;
            txtCorreo.Text = estudiante.DatosPersonales.Correo;
            txtTelefono.Text = estudiante.DatosPersonales.Telefono;
            txtCurp.Text = estudiante.DatosPersonales.Curp;

            if (estudiante.DatosPersonales.FechaNacimiento.HasValue)
                dtpFechaNacim.Value = estudiante.DatosPersonales.FechaNacimiento.Value;
            else
                dtpFechaNacim.Value = DateTime.Now;

            // Datos del estudiante
            txtNoControl.Text = estudiante.Matricula;

            // Buscar el semestre en el control
            //for (int i = 0; i < nudSemestre.Items.Count; i++)
            //{
            //    if (nudSemestre.Items[i].ToString() == estudiante.Semestre)
            //    {
            //        nudSemestre.SelectedIndex = i;
            //        break;
            //    }
            //}

            // Fechas
            dtpFechaAlta.Value = estudiante.FechaAlta;

            if (estudiante.FechaBaja.HasValue)
            {
                dtpFechaBaja.Value = estudiante.FechaBaja.Value;
                dtpFechaBaja.Enabled = true;
            }
            else
            {
                dtpFechaBaja.Value = DateTime.Now;
                dtpFechaBaja.Enabled = false;
            }

            // Estatus
            cbxEstatus.SelectedValue = estudiante.Estatus;

            // Guardar el ID en una propiedad o tag para usarlo al actualizar
            this.Tag = estudiante.Id;
        }

        /// <summary>
        /// Cambia el modo de operación entre nuevo registro y edición
        /// </summary>
        /// <param name="edicion">True para modo edición, False para modo nuevo registro</param>
        private void ModoEdicion(bool edicion)
        {
            // Cambiar título y configurar botones según el modo
            gbEstudiante.Text = edicion ? "Editar Estudiante" : "Nuevo Estudiante";
            btnGuardar.Text = edicion ? "Actualizar" : "Guardar";

            // Si es modo edición, desactivar campos que no deberían modificarse
            txtNoControl.ReadOnly = edicion;

            // Activar el panel izquierdo para mostrar los detalles
            if (scEstudiantes.Panel1Collapsed)
            {
                scEstudiantes.Panel1Collapsed = false;
                btnMostrarCap.Text = "Ocultar captura rápida";
            }
        }


        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtCorreo.Clear();
            txtTelefono.Clear();
            txtNoControl.Clear();
            txtCurp.Clear();
            nudSemestre.Text = "1";
            dtpFechaNacim.Value = DateTime.Now;
            dtpFechaAlta.Value = DateTime.Now;
            cbxEstatus.SelectedValue = 2;
        }

        private bool DatosVacios()
        {
            if (txtNombre.Text == "" || txtCorreo.Text == "" || txtTelefono.Text == ""
                || txtCurp.Text == "" || nudSemestre.Text == "" || txtNoControl.Text == ""
                || nudSemestre.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool DatosValidos()

        {
            if (!EstudiantesNegocio.EsCorreoValido(txtCorreo.Text.Trim()))
            {
                MessageBox.Show("Correo inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!EstudiantesNegocio.EsCURPValido(txtCurp.Text.Trim()))
            {
                MessageBox.Show("CURP inválida.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!EstudiantesNegocio.EsNoControlValido(txtNoControl.Text.Trim()))
            {
                MessageBox.Show("Número de control inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }


        private void cbxTipoF_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Determinar si estamos en modo edición o nuevo registro
            if (btnGuardar.Text == "Actualizar") // Modo edición
            {
                ActualizarEstudiante(); // Método para actualizar estudiante existente
            }
            else // Modo nuevo registro
            {
                GuardarEstudiante(); // Método para guardar nuevo estudiante

            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarEstudiantes();
        }

        private void btnMostrarCap_Click(object sender, EventArgs e)
        {
            if (scEstudiantes.Panel1Collapsed)
            {

                scEstudiantes.Panel1Collapsed = false;
                btnMostrarCap.Text = "Ocultar captura rapida";
            }
            else
            {
                scEstudiantes.Panel1Collapsed = true;
                btnMostrarCap.Text = "Mostrar captura rapida";
            }
        }

        private void btnCargaMas_Click(object sender, EventArgs e)
        {
            ofdArchivo.Title = "Seleccionar archivo de Excel";
            ofdArchivo.Filter = "Archivo de Excel (*.xlsl;*.xls)|*.xlsx;*.xls";///*==solo busca esos tipos de archivos 
            ofdArchivo.InitialDirectory = "C:\\";//Carpeta inicial
            ofdArchivo.FilterIndex = 1;//Selecciona el primer filtro por defecto
            ofdArchivo.RestoreDirectory = true;

            if (ofdArchivo.ShowDialog() == DialogResult.OK)
            {
                string filePath = ofdArchivo.FileName;
                string extension = Path.GetExtension(filePath).ToLower();
                if (extension == ".xlsx" || extension == ".xls")
                {
                    MessageBox.Show("Archivo valido: " + filePath, "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Por favor seleccione un archivo de Excel valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void CargarEstudiantes()
        {
            try
            {
                //Mostrar indicador de  carga si es necesario
                Cursor = Cursors.WaitCursor;

                //Crea instancia del controlador
                EstudiantesController estudianteController = new EstudiantesController();

                //Obtener la lista de estudiantes(solo activos por defecto
                List<Estudiante> estudiantes = estudianteController.ObtenerEstudiantes(
                    soloActivos: false,
                    tipoFecha: cbxTipoF.SelectedValue != null ? (int)cbxTipoF.SelectedValue : 0,
                    fechaInicio: dtpFechaInicio.Enabled ? dtpFechaInicio.Value : (DateTime?)null,
                    fechaFin: dtpFechaFin.Enabled ? dtpFechaFin.Value : (DateTime?)null
                    );
                //limpiar el DataGridView
                dgvEstudiantes.DataSource = null;

                if (estudiantes.Count == 0)
                {
                    lblTotalRegistros.Text = "Total: 0  registros";

                    //opcionalmente mostrar mensaje cuando no hay datos
                    if (!string.IsNullOrEmpty(txtBusquedaTxt.Text))
                    {
                        MessageBox.Show("No se encontraron estudiantes con el criterio de busqueda especificado", "Informacion del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    return;
                }

                //Crear una tabla
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Matrícula", typeof(string));
                dt.Columns.Add("Nombre Completo", typeof(string));
                dt.Columns.Add("Semestre", typeof(string));
                dt.Columns.Add("Correo", typeof(string));
                dt.Columns.Add("Teléfono", typeof(string));
                dt.Columns.Add("CURP", typeof(string));
                dt.Columns.Add("Fecha Nacimiento", typeof(DateTime));
                dt.Columns.Add("Fecha Alta", typeof(DateTime));
                dt.Columns.Add("Estatus", typeof(string));

                //llenar el dataTime con la info de los estudiantes
                foreach (Estudiante estudiante in estudiantes)
                {
                    dt.Rows.Add(
                        estudiante.Id,
                        estudiante.Matricula,
                        estudiante.DatosPersonales.NombreCompleto,
                        estudiante.Semestre,
                        estudiante.DatosPersonales.Correo,
                        estudiante.DatosPersonales.Telefono,
                        estudiante.DatosPersonales.Curp,
                        estudiante.DatosPersonales.FechaNacimiento,
                        estudiante.FechaAlta,
                        estudiante.DescripcionEstatus
                        );
                }

                //asignar el dataTime como origen de dattos
                dgvEstudiantes.DataSource = dt;

                //configurar la paraencia
                ConfigurarDataGridView();

                //Actualizar contador de registros
                lblTotalRegistros.Text = $"Total: {estudiantes.Count} registros";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar estudiantes. Contacta al administrador del sistema",
                    "Error del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //restaurar cursor, no tener bolita de carga
                Cursor = Cursors.Default;
            }
        }


        private void ConfigurarDataGridView()
        {
            //Ajustes generales
            dgvEstudiantes.AllowUserToAddRows = false;
            dgvEstudiantes.AllowUserToDeleteRows = false;
            dgvEstudiantes.ReadOnly = true;

            // Ajustar el ancho de las columnas
            dgvEstudiantes.Columns["Matrícula"].Width = 100;
            dgvEstudiantes.Columns["Nombre Completo"].Width = 200;
            dgvEstudiantes.Columns["Semestre"].Width = 80;
            dgvEstudiantes.Columns["Correo"].Width = 180;
            dgvEstudiantes.Columns["Teléfono"].Width = 120;
            dgvEstudiantes.Columns["CURP"].Width = 150;
            dgvEstudiantes.Columns["Fecha Nacimiento"].Width = 120;
            dgvEstudiantes.Columns["Fecha Alta"].Width = 120;
            dgvEstudiantes.Columns["Estatus"].Width = 100;

            // Ocultar columna ID si es necesario
            dgvEstudiantes.Columns["ID"].Visible = false;

            // Formato para las fechas
            dgvEstudiantes.Columns["Fecha Nacimiento"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvEstudiantes.Columns["Fecha Alta"].DefaultCellStyle.Format = "dd/MM/yyyy";

            // Alineación
            dgvEstudiantes.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvEstudiantes.Columns["Matrícula"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEstudiantes.Columns["Semestre"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEstudiantes.Columns["Estatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Color alternado de filas
            dgvEstudiantes.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            // Selección de fila completa
            dgvEstudiantes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Estilo de cabeceras
            dgvEstudiantes.EnableHeadersVisualStyles = false;
            dgvEstudiantes.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvEstudiantes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvEstudiantes.ColumnHeadersDefaultCellStyle.Font = new Font(dgvEstudiantes.Font, FontStyle.Bold);
            dgvEstudiantes.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Ordenar al hacer clic en el encabezado
            dgvEstudiantes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvEstudiantes.ColumnHeadersHeight = 35;
        }

        private void gbFiltros_Enter(object sender, EventArgs e)
        {

        }

        private void nudSemestre_ValueChanged(object sender, EventArgs e)
        {

        }

        private void editarEstudianteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar si hay una fila seleccionada en el grid
                if (dgvEstudiantes.SelectedRows.Count > 0)
                {
                    // Obtener el ID del estudiante de la fila seleccionada
                    int idEstudiante = Convert.ToInt32(dgvEstudiantes.SelectedRows[0].Cells["id"].Value);

                    // Llamar a la función para obtener y mostrar los detalles
                    ObtenerDetalleEstudiante(idEstudiante);
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un estudiante para editar.",
                                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al preparar la edición del estudiante: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ImportarExcel()
        {
            try
            {
                // Crear instancia del controlador
                EstudiantesController estudiantesController = new EstudiantesController();

                // Obtener los filtros actuales de la interfaz
                bool soloActivos = true;// chkSoloActivos.Checked;
                int tipoFecha = cbxTipoF.SelectedValue != null ? (int)cbxTipoF.SelectedValue : 0;
                DateTime? fechaInicio = dtpFechaInicio.Value;
                DateTime? fechaFin = dtpFechaFin.Value;

                // Mostrar diálogo para guardar archivo
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Archivos de Excel (*.xlsx)|*.xlsx";
                    saveFileDialog.Title = "Guardar archivo de Excel";
                    saveFileDialog.FileName = $"Estudiantes_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                    saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)//Espera confirmacion
                    {
                        // Mostrar cursor de espera
                        Cursor.Current = Cursors.WaitCursor;//Se puede usar el PogressBar para que muestre al progreso de la accion (se recomienda usarlo cuando se procesa mucha informacion para no ver el circulito girando por tanto tiempo)

                        // Exportar usando el método del controlador
                        bool resultado = estudiantesController.ExportarEstudiantesExcel(
                            saveFileDialog.FileName,
                            soloActivos,
                            tipoFecha,
                            fechaInicio,
                            fechaFin);

                        // Restaurar cursor normal
                        Cursor.Current = Cursors.Default;

                        if (resultado)
                        {
                            MessageBox.Show("Archivo Excel exportado correctamente",
                                            "Éxito",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);

                            // Preguntar si desea abrir el archivo
                            DialogResult abrirArchivo = MessageBox.Show(
                                            "¿Desea abrir el archivo Excel generado?",
                                            "Abrir archivo",
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question);

                            if (abrirArchivo == DialogResult.Yes)
                            {
                                // Usar ProcessStartInfo para abrir el archivo con la aplicación asociada
                                var startInfo = new System.Diagnostics.ProcessStartInfo
                                {
                                    FileName = saveFileDialog.FileName,
                                    UseShellExecute = true
                                };
                                System.Diagnostics.Process.Start(startInfo);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No se encontraron estudiantes para exportar",
                                            "Información",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show($"Error al exportar a Excel: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void csmEstudiantes_Opening(object sender, CancelEventArgs e)
        {

        }

        private void scEstudiantes_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbl_titulo_Click(object sender, EventArgs e)
        {

        }

        private void cbxEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            ImportarExcel();
        }
    }
}
