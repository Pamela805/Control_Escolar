namespace Control_Escolar.View
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            lbl_usuario = new Label();
            label2 = new Label();
            lbl_contraseña = new Label();
            txtUsuario = new TextBox();
            txtContraseña = new TextBox();
            btn_login = new Button();
            btn_cancelar = new Button();
            pbLogin = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbLogin).BeginInit();
            SuspendLayout();
            // 
            // lbl_usuario
            // 
            lbl_usuario.AutoSize = true;
            lbl_usuario.Location = new Point(260, 106);
            lbl_usuario.Name = "lbl_usuario";
            lbl_usuario.Size = new Size(68, 23);
            lbl_usuario.TabIndex = 0;
            lbl_usuario.Text = "Usuario";
            // 
            // label2
            // 
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 8;
            // 
            // lbl_contraseña
            // 
            lbl_contraseña.AutoSize = true;
            lbl_contraseña.Location = new Point(260, 171);
            lbl_contraseña.Name = "lbl_contraseña";
            lbl_contraseña.Size = new Size(97, 23);
            lbl_contraseña.TabIndex = 2;
            lbl_contraseña.Text = "Contraseña";
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(377, 99);
            txtUsuario.MaxLength = 20;
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(192, 30);
            txtUsuario.TabIndex = 3;
            // 
            // txtContraseña
            // 
            txtContraseña.Location = new Point(377, 164);
            txtContraseña.MaxLength = 20;
            txtContraseña.Name = "txtContraseña";
            txtContraseña.PasswordChar = '*';
            txtContraseña.Size = new Size(192, 30);
            txtContraseña.TabIndex = 4;
            // 
            // btn_login
            // 
            btn_login.Image = Properties.Resources._59071_entrance_entry_login_sign_in_icon;
            btn_login.ImageAlign = ContentAlignment.MiddleLeft;
            btn_login.Location = new Point(258, 325);
            btn_login.Name = "btn_login";
            btn_login.Size = new Size(153, 36);
            btn_login.TabIndex = 5;
            btn_login.Text = "Iniciar Sesion";
            btn_login.TextAlign = ContentAlignment.MiddleRight;
            btn_login.UseVisualStyleBackColor = true;
            btn_login.Click += btn_login_Click;
            // 
            // btn_cancelar
            // 
            btn_cancelar.Image = (Image)resources.GetObject("btn_cancelar.Image");
            btn_cancelar.ImageAlign = ContentAlignment.MiddleLeft;
            btn_cancelar.Location = new Point(469, 325);
            btn_cancelar.Name = "btn_cancelar";
            btn_cancelar.Size = new Size(111, 36);
            btn_cancelar.TabIndex = 6;
            btn_cancelar.Text = "Cancelar";
            btn_cancelar.TextAlign = ContentAlignment.MiddleRight;
            btn_cancelar.UseVisualStyleBackColor = true;
            btn_cancelar.Click += btn_cancelar_Click;
            // 
            // pbLogin
            // 
            pbLogin.Image = Properties.Resources._5986159;
            pbLogin.Location = new Point(12, 57);
            pbLogin.Name = "pbLogin";
            pbLogin.Size = new Size(242, 201);
            pbLogin.SizeMode = PictureBoxSizeMode.StretchImage;
            pbLogin.TabIndex = 7;
            pbLogin.TabStop = false;
            // 
            // frmLogin
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pbLogin);
            Controls.Add(btn_cancelar);
            Controls.Add(btn_login);
            Controls.Add(txtContraseña);
            Controls.Add(txtUsuario);
            Controls.Add(lbl_contraseña);
            Controls.Add(label2);
            Controls.Add(lbl_usuario);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            Name = "frmLogin";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Login";
            Load += frmLogin_Load;
            ((System.ComponentModel.ISupportInitialize)pbLogin).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbl_usuario;
        private Label label2;
        private Label lbl_contraseña;
        private TextBox txtUsuario;
        private TextBox txtContraseña;
        private Button btn_login;
        private Button btn_cancelar;
        private PictureBox pbLogin;
    }
}