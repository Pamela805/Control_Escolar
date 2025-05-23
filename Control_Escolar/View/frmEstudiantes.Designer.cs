namespace Control_Escolar.View
{
    partial class frmEstudiantes
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEstudiantes));
            lbl_titulo = new Label();
            scEstudiantes = new SplitContainer();
            gbEstudiante = new GroupBox();
            btnGuardar = new Button();
            lblDatos = new Label();
            dtpFechaBaja = new DateTimePicker();
            lblFechaBaja = new Label();
            lblEstatus = new Label();
            dtpFechaAlta = new DateTimePicker();
            cbxEstatus = new ComboBox();
            lblFechaAlta = new Label();
            pbInfo = new PictureBox();
            txtNoControl = new TextBox();
            lblNcontrol = new Label();
            lblSemestre = new Label();
            nudSemestre = new NumericUpDown();
            lblFechaNacim = new Label();
            dtpFechaNacim = new DateTimePicker();
            txtCurp = new TextBox();
            txtTelefono = new TextBox();
            txtCorreo = new TextBox();
            txtNombre = new TextBox();
            lblCurp = new Label();
            lblTelefono = new Label();
            lblCorreo = new Label();
            lblNombre = new Label();
            lblTotalRegistros = new Label();
            dgvEstudiantes = new DataGridView();
            gbFiltros = new GroupBox();
            btnActualizar = new Button();
            dtpFechaFin = new DateTimePicker();
            lblFechaFin = new Label();
            dtpFechaInicio = new DateTimePicker();
            lblFechaInicio = new Label();
            txtBusquedaTxt = new TextBox();
            lblBusquedaTxt = new Label();
            lblTipoF = new Label();
            cbxTipoF = new ComboBox();
            gbHerramientas = new GroupBox();
            btnExcel = new Button();
            lblRutaArch = new Label();
            btnCargaMas = new Button();
            btnMostrarCap = new Button();
            ttInfo = new ToolTip(components);
            ofdArchivo = new OpenFileDialog();
            contextMenuStrip1 = new ContextMenuStrip(components);
            csmEstudiantes = new ContextMenuStrip(components);
            editarEstudianteToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)scEstudiantes).BeginInit();
            scEstudiantes.Panel1.SuspendLayout();
            scEstudiantes.Panel2.SuspendLayout();
            scEstudiantes.SuspendLayout();
            gbEstudiante.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbInfo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSemestre).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvEstudiantes).BeginInit();
            gbFiltros.SuspendLayout();
            gbHerramientas.SuspendLayout();
            csmEstudiantes.SuspendLayout();
            SuspendLayout();
            // 
            // lbl_titulo
            // 
            lbl_titulo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lbl_titulo.BackColor = SystemColors.ActiveCaption;
            lbl_titulo.Font = new Font("Segoe UI Black", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_titulo.Location = new Point(12, 9);
            lbl_titulo.Name = "lbl_titulo";
            lbl_titulo.Size = new Size(1210, 23);
            lbl_titulo.TabIndex = 0;
            lbl_titulo.Text = "Control de Estudiantes";
            lbl_titulo.TextAlign = ContentAlignment.MiddleCenter;
            lbl_titulo.Click += lbl_titulo_Click;
            // 
            // scEstudiantes
            // 
            scEstudiantes.Location = new Point(12, 35);
            scEstudiantes.Name = "scEstudiantes";
            // 
            // scEstudiantes.Panel1
            // 
            scEstudiantes.Panel1.Controls.Add(gbEstudiante);
            // 
            // scEstudiantes.Panel2
            // 
            scEstudiantes.Panel2.Controls.Add(lblTotalRegistros);
            scEstudiantes.Panel2.Controls.Add(dgvEstudiantes);
            scEstudiantes.Panel2.Controls.Add(gbFiltros);
            scEstudiantes.Panel2.Controls.Add(gbHerramientas);
            scEstudiantes.Panel2.Paint += scEstudiantes_Panel2_Paint;
            scEstudiantes.Size = new Size(1210, 759);
            scEstudiantes.SplitterDistance = 315;
            scEstudiantes.TabIndex = 1;
            // 
            // gbEstudiante
            // 
            gbEstudiante.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbEstudiante.Controls.Add(btnGuardar);
            gbEstudiante.Controls.Add(lblDatos);
            gbEstudiante.Controls.Add(dtpFechaBaja);
            gbEstudiante.Controls.Add(lblFechaBaja);
            gbEstudiante.Controls.Add(lblEstatus);
            gbEstudiante.Controls.Add(dtpFechaAlta);
            gbEstudiante.Controls.Add(cbxEstatus);
            gbEstudiante.Controls.Add(lblFechaAlta);
            gbEstudiante.Controls.Add(pbInfo);
            gbEstudiante.Controls.Add(txtNoControl);
            gbEstudiante.Controls.Add(lblNcontrol);
            gbEstudiante.Controls.Add(lblSemestre);
            gbEstudiante.Controls.Add(nudSemestre);
            gbEstudiante.Controls.Add(lblFechaNacim);
            gbEstudiante.Controls.Add(dtpFechaNacim);
            gbEstudiante.Controls.Add(txtCurp);
            gbEstudiante.Controls.Add(txtTelefono);
            gbEstudiante.Controls.Add(txtCorreo);
            gbEstudiante.Controls.Add(txtNombre);
            gbEstudiante.Controls.Add(lblCurp);
            gbEstudiante.Controls.Add(lblTelefono);
            gbEstudiante.Controls.Add(lblCorreo);
            gbEstudiante.Controls.Add(lblNombre);
            gbEstudiante.Location = new Point(3, 3);
            gbEstudiante.Name = "gbEstudiante";
            gbEstudiante.Size = new Size(309, 731);
            gbEstudiante.TabIndex = 0;
            gbEstudiante.TabStop = false;
            gbEstudiante.Text = "Alta o Edicion";
            // 
            // btnGuardar
            // 
            btnGuardar.Image = (Image)resources.GetObject("btnGuardar.Image");
            btnGuardar.ImageAlign = ContentAlignment.MiddleLeft;
            btnGuardar.Location = new Point(200, 666);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(103, 31);
            btnGuardar.TabIndex = 22;
            btnGuardar.Text = "Guardar";
            btnGuardar.TextAlign = ContentAlignment.MiddleRight;
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // lblDatos
            // 
            lblDatos.AutoSize = true;
            lblDatos.Location = new Point(6, 666);
            lblDatos.Name = "lblDatos";
            lblDatos.Size = new Size(159, 23);
            lblDatos.TabIndex = 21;
            lblDatos.Text = "*Datos Obligatorios";
            // 
            // dtpFechaBaja
            // 
            dtpFechaBaja.Format = DateTimePickerFormat.Short;
            dtpFechaBaja.Location = new Point(155, 535);
            dtpFechaBaja.Name = "dtpFechaBaja";
            dtpFechaBaja.Size = new Size(124, 30);
            dtpFechaBaja.TabIndex = 20;
            // 
            // lblFechaBaja
            // 
            lblFechaBaja.AutoSize = true;
            lblFechaBaja.Location = new Point(155, 509);
            lblFechaBaja.Name = "lblFechaBaja";
            lblFechaBaja.Size = new Size(83, 23);
            lblFechaBaja.TabIndex = 19;
            lblFechaBaja.Text = "Feha Baja";
            // 
            // lblEstatus
            // 
            lblEstatus.AutoSize = true;
            lblEstatus.Location = new Point(6, 582);
            lblEstatus.Name = "lblEstatus";
            lblEstatus.Size = new Size(71, 23);
            lblEstatus.TabIndex = 18;
            lblEstatus.Text = "Estatus*";
            // 
            // dtpFechaAlta
            // 
            dtpFechaAlta.Format = DateTimePickerFormat.Short;
            dtpFechaAlta.Location = new Point(6, 535);
            dtpFechaAlta.Name = "dtpFechaAlta";
            dtpFechaAlta.Size = new Size(124, 30);
            dtpFechaAlta.TabIndex = 17;
            // 
            // cbxEstatus
            // 
            cbxEstatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxEstatus.FormattingEnabled = true;
            cbxEstatus.Location = new Point(6, 608);
            cbxEstatus.Name = "cbxEstatus";
            cbxEstatus.Size = new Size(256, 31);
            cbxEstatus.TabIndex = 16;
            cbxEstatus.SelectedIndexChanged += cbxEstatus_SelectedIndexChanged;
            // 
            // lblFechaAlta
            // 
            lblFechaAlta.AutoSize = true;
            lblFechaAlta.Location = new Point(6, 509);
            lblFechaAlta.Name = "lblFechaAlta";
            lblFechaAlta.Size = new Size(88, 23);
            lblFechaAlta.TabIndex = 15;
            lblFechaAlta.Text = "Feha Alta*";
            // 
            // pbInfo
            // 
            pbInfo.Image = Properties.Resources._134192_information_question_icon;
            pbInfo.Location = new Point(271, 453);
            pbInfo.Name = "pbInfo";
            pbInfo.Size = new Size(32, 30);
            pbInfo.SizeMode = PictureBoxSizeMode.StretchImage;
            pbInfo.TabIndex = 14;
            pbInfo.TabStop = false;
            ttInfo.SetToolTip(pbInfo, "T/M Año de ingreso-No. de Alumno");
            // 
            // txtNoControl
            // 
            txtNoControl.Location = new Point(6, 453);
            txtNoControl.MaxLength = 20;
            txtNoControl.Name = "txtNoControl";
            txtNoControl.Size = new Size(259, 30);
            txtNoControl.TabIndex = 13;
            // 
            // lblNcontrol
            // 
            lblNcontrol.AutoSize = true;
            lblNcontrol.Location = new Point(6, 427);
            lblNcontrol.Name = "lblNcontrol";
            lblNcontrol.Size = new Size(106, 23);
            lblNcontrol.TabIndex = 12;
            lblNcontrol.Text = "No. Control*";
            // 
            // lblSemestre
            // 
            lblSemestre.AutoSize = true;
            lblSemestre.Location = new Point(186, 351);
            lblSemestre.Name = "lblSemestre";
            lblSemestre.Size = new Size(87, 23);
            lblSemestre.TabIndex = 11;
            lblSemestre.Text = "Semestre*";
            // 
            // nudSemestre
            // 
            nudSemestre.Location = new Point(186, 377);
            nudSemestre.Maximum = new decimal(new int[] { 13, 0, 0, 0 });
            nudSemestre.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudSemestre.Name = "nudSemestre";
            nudSemestre.Size = new Size(120, 30);
            nudSemestre.TabIndex = 10;
            nudSemestre.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudSemestre.ValueChanged += nudSemestre_ValueChanged;
            // 
            // lblFechaNacim
            // 
            lblFechaNacim.AutoSize = true;
            lblFechaNacim.Location = new Point(6, 351);
            lblFechaNacim.Name = "lblFechaNacim";
            lblFechaNacim.Size = new Size(146, 23);
            lblFechaNacim.TabIndex = 9;
            lblFechaNacim.Text = "Feha Nacimiento*";
            // 
            // dtpFechaNacim
            // 
            dtpFechaNacim.Format = DateTimePickerFormat.Short;
            dtpFechaNacim.Location = new Point(6, 377);
            dtpFechaNacim.Name = "dtpFechaNacim";
            dtpFechaNacim.Size = new Size(124, 30);
            dtpFechaNacim.TabIndex = 8;
            // 
            // txtCurp
            // 
            txtCurp.Location = new Point(6, 299);
            txtCurp.MaxLength = 18;
            txtCurp.Name = "txtCurp";
            txtCurp.Size = new Size(300, 30);
            txtCurp.TabIndex = 7;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(6, 222);
            txtTelefono.MaxLength = 15;
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(300, 30);
            txtTelefono.TabIndex = 6;
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(6, 140);
            txtCorreo.MaxLength = 100;
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(300, 30);
            txtCorreo.TabIndex = 5;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(6, 66);
            txtNombre.MaxLength = 255;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(300, 30);
            txtNombre.TabIndex = 4;
            // 
            // lblCurp
            // 
            lblCurp.AutoSize = true;
            lblCurp.Location = new Point(6, 273);
            lblCurp.Name = "lblCurp";
            lblCurp.Size = new Size(60, 23);
            lblCurp.TabIndex = 3;
            lblCurp.Text = "CURP*";
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new Point(6, 196);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(81, 23);
            lblTelefono.TabIndex = 2;
            lblTelefono.Text = "Telefono*";
            // 
            // lblCorreo
            // 
            lblCorreo.AutoSize = true;
            lblCorreo.Location = new Point(6, 114);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new Size(69, 23);
            lblCorreo.TabIndex = 1;
            lblCorreo.Text = "Correo*";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(6, 40);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(160, 23);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre Completo*";
            // 
            // lblTotalRegistros
            // 
            lblTotalRegistros.AutoSize = true;
            lblTotalRegistros.Location = new Point(9, 186);
            lblTotalRegistros.Name = "lblTotalRegistros";
            lblTotalRegistros.Size = new Size(153, 23);
            lblTotalRegistros.TabIndex = 3;
            lblTotalRegistros.Text = "Total de Registros: ";
            // 
            // dgvEstudiantes
            // 
            dgvEstudiantes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvEstudiantes.BackgroundColor = SystemColors.HighlightText;
            dgvEstudiantes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEstudiantes.Location = new Point(3, 212);
            dgvEstudiantes.Name = "dgvEstudiantes";
            dgvEstudiantes.Size = new Size(885, 506);
            dgvEstudiantes.TabIndex = 2;
            // 
            // gbFiltros
            // 
            gbFiltros.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbFiltros.Controls.Add(btnActualizar);
            gbFiltros.Controls.Add(dtpFechaFin);
            gbFiltros.Controls.Add(lblFechaFin);
            gbFiltros.Controls.Add(dtpFechaInicio);
            gbFiltros.Controls.Add(lblFechaInicio);
            gbFiltros.Controls.Add(txtBusquedaTxt);
            gbFiltros.Controls.Add(lblBusquedaTxt);
            gbFiltros.Controls.Add(lblTipoF);
            gbFiltros.Controls.Add(cbxTipoF);
            gbFiltros.Location = new Point(3, 73);
            gbFiltros.Name = "gbFiltros";
            gbFiltros.Size = new Size(885, 110);
            gbFiltros.TabIndex = 1;
            gbFiltros.TabStop = false;
            gbFiltros.Text = "Filtros";
            // 
            // btnActualizar
            // 
            btnActualizar.Image = (Image)resources.GetObject("btnActualizar.Image");
            btnActualizar.ImageAlign = ContentAlignment.MiddleLeft;
            btnActualizar.Location = new Point(648, 70);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(124, 31);
            btnActualizar.TabIndex = 23;
            btnActualizar.Text = "Actualizar";
            btnActualizar.TextAlign = ContentAlignment.MiddleRight;
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // dtpFechaFin
            // 
            dtpFechaFin.Format = DateTimePickerFormat.Short;
            dtpFechaFin.Location = new Point(648, 26);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(124, 30);
            dtpFechaFin.TabIndex = 27;
            // 
            // lblFechaFin
            // 
            lblFechaFin.AutoSize = true;
            lblFechaFin.Location = new Point(564, 29);
            lblFechaFin.Name = "lblFechaFin";
            lblFechaFin.Size = new Size(78, 23);
            lblFechaFin.TabIndex = 26;
            lblFechaFin.Text = "Fecha fin";
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Format = DateTimePickerFormat.Short;
            dtpFechaInicio.Location = new Point(407, 26);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(124, 30);
            dtpFechaInicio.TabIndex = 23;
            dtpFechaInicio.Value = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            // 
            // lblFechaInicio
            // 
            lblFechaInicio.AutoSize = true;
            lblFechaInicio.Location = new Point(302, 29);
            lblFechaInicio.Name = "lblFechaInicio";
            lblFechaInicio.Size = new Size(99, 23);
            lblFechaInicio.TabIndex = 25;
            lblFechaInicio.Text = "Fecha inicio";
            // 
            // txtBusquedaTxt
            // 
            txtBusquedaTxt.Location = new Point(172, 74);
            txtBusquedaTxt.MaxLength = 18;
            txtBusquedaTxt.Name = "txtBusquedaTxt";
            txtBusquedaTxt.Size = new Size(359, 30);
            txtBusquedaTxt.TabIndex = 23;
            // 
            // lblBusquedaTxt
            // 
            lblBusquedaTxt.AutoSize = true;
            lblBusquedaTxt.Location = new Point(6, 77);
            lblBusquedaTxt.Name = "lblBusquedaTxt";
            lblBusquedaTxt.Size = new Size(164, 23);
            lblBusquedaTxt.TabIndex = 23;
            lblBusquedaTxt.Text = "Busqueda por texto:";
            // 
            // lblTipoF
            // 
            lblTipoF.AutoSize = true;
            lblTipoF.Location = new Point(6, 29);
            lblTipoF.Name = "lblTipoF";
            lblTipoF.Size = new Size(92, 23);
            lblTipoF.TabIndex = 24;
            lblTipoF.Text = "Tipo Fecha";
            // 
            // cbxTipoF
            // 
            cbxTipoF.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxTipoF.FormattingEnabled = true;
            cbxTipoF.Location = new Point(172, 26);
            cbxTipoF.Name = "cbxTipoF";
            cbxTipoF.Size = new Size(124, 31);
            cbxTipoF.TabIndex = 23;
            cbxTipoF.SelectedIndexChanged += cbxTipoF_SelectedIndexChanged;
            // 
            // gbHerramientas
            // 
            gbHerramientas.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbHerramientas.Controls.Add(btnExcel);
            gbHerramientas.Controls.Add(lblRutaArch);
            gbHerramientas.Controls.Add(btnCargaMas);
            gbHerramientas.Controls.Add(btnMostrarCap);
            gbHerramientas.Location = new Point(3, 3);
            gbHerramientas.Name = "gbHerramientas";
            gbHerramientas.Size = new Size(885, 71);
            gbHerramientas.TabIndex = 0;
            gbHerramientas.TabStop = false;
            gbHerramientas.Text = "Herramientas";
            // 
            // btnExcel
            // 
            btnExcel.Location = new Point(707, 24);
            btnExcel.Name = "btnExcel";
            btnExcel.Size = new Size(172, 34);
            btnExcel.TabIndex = 4;
            btnExcel.Text = "Exportar a Excel";
            btnExcel.UseVisualStyleBackColor = true;
            btnExcel.Click += btnExcel_Click;
            // 
            // lblRutaArch
            // 
            lblRutaArch.AutoSize = true;
            lblRutaArch.Location = new Point(330, 35);
            lblRutaArch.Name = "lblRutaArch";
            lblRutaArch.Size = new Size(214, 23);
            lblRutaArch.TabIndex = 2;
            lblRutaArch.Text = "Ruta de archivo a importar";
            // 
            // btnCargaMas
            // 
            btnCargaMas.Location = new Point(168, 29);
            btnCargaMas.Name = "btnCargaMas";
            btnCargaMas.Size = new Size(156, 34);
            btnCargaMas.TabIndex = 1;
            btnCargaMas.Text = "Carga Masiva";
            btnCargaMas.UseVisualStyleBackColor = true;
            btnCargaMas.Click += btnCargaMas_Click;
            // 
            // btnMostrarCap
            // 
            btnMostrarCap.Location = new Point(6, 29);
            btnMostrarCap.Name = "btnMostrarCap";
            btnMostrarCap.Size = new Size(156, 34);
            btnMostrarCap.TabIndex = 0;
            btnMostrarCap.Text = "Mostrar Captura";
            btnMostrarCap.UseVisualStyleBackColor = true;
            btnMostrarCap.Click += btnMostrarCap_Click;
            // 
            // ofdArchivo
            // 
            ofdArchivo.FileName = "ofdArchivo";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // csmEstudiantes
            // 
            csmEstudiantes.Items.AddRange(new ToolStripItem[] { editarEstudianteToolStripMenuItem });
            csmEstudiantes.Name = "csmEstudiantes";
            csmEstudiantes.Size = new Size(214, 32);
            csmEstudiantes.Opening += csmEstudiantes_Opening;
            // 
            // editarEstudianteToolStripMenuItem
            // 
            editarEstudianteToolStripMenuItem.Name = "editarEstudianteToolStripMenuItem";
            editarEstudianteToolStripMenuItem.Size = new Size(213, 28);
            editarEstudianteToolStripMenuItem.Text = "Editar Estudiante.";
            editarEstudianteToolStripMenuItem.Click += editarEstudianteToolStripMenuItem_Click;
            // 
            // frmEstudiantes
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1234, 797);
            Controls.Add(scEstudiantes);
            Controls.Add(lbl_titulo);
            Name = "frmEstudiantes";
            Text = "frmEstudiantes";
            Load += frmEstudiantes_Load;
            scEstudiantes.Panel1.ResumeLayout(false);
            scEstudiantes.Panel2.ResumeLayout(false);
            scEstudiantes.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)scEstudiantes).EndInit();
            scEstudiantes.ResumeLayout(false);
            gbEstudiante.ResumeLayout(false);
            gbEstudiante.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbInfo).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSemestre).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvEstudiantes).EndInit();
            gbFiltros.ResumeLayout(false);
            gbFiltros.PerformLayout();
            gbHerramientas.ResumeLayout(false);
            gbHerramientas.PerformLayout();
            csmEstudiantes.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lbl_titulo;
        private SplitContainer scEstudiantes;
        private GroupBox gbEstudiante;
        private Label lblCurp;
        private Label lblTelefono;
        private Label lblCorreo;
        private Label lblNombre;
        private TextBox txtCorreo;
        private TextBox txtNombre;
        private TextBox txtCurp;
        private TextBox txtTelefono;
        private DateTimePicker dtpFechaNacim;
        private Label lblFechaNacim;
        private Label lblNcontrol;
        private Label lblSemestre;
        private NumericUpDown nudSemestre;
        private TextBox txtNoControl;
        private PictureBox pbInfo;
        private ToolTip ttInfo;
        private ComboBox cbxEstatus;
        private Label lblFechaAlta;
        private Label lblEstatus;
        private DateTimePicker dtpFechaAlta;
        private Label lblDatos;
        private DateTimePicker dtpFechaBaja;
        private Label lblFechaBaja;
        private Button btnGuardar;
        private GroupBox gbFiltros;
        private GroupBox gbHerramientas;
        private Label lblRutaArch;
        private Button btnCargaMas;
        private Button btnMostrarCap;
        private DateTimePicker dtpFechaFin;
        private Label lblFechaFin;
        private DateTimePicker dtpFechaInicio;
        private Label lblFechaInicio;
        private TextBox txtBusquedaTxt;
        private Label lblBusquedaTxt;
        private Label lblTipoF;
        private ComboBox cbxTipoF;
        private DataGridView dgvEstudiantes;
        private Button btnActualizar;
        private OpenFileDialog ofdArchivo;
        private Label lblTotalRegistros;
        private ContextMenuStrip contextMenuStrip1;
        private ContextMenuStrip csmEstudiantes;
        private ToolStripMenuItem editarEstudianteToolStripMenuItem;
        private Button btnExcel;
    }
}