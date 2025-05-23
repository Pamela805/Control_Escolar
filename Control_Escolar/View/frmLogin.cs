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
using NLog;
using Control_Escolar.Utilities;


namespace Control_Escolar.View
{
    public partial class frmLogin : Form
    {
        //private static readonly Logger _logger = LoggingManager.GetLogger("Control_Escolar.View.frmLogin");

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {

        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MessageBox.Show("El campo de usuario no puede estar vacio", "Informacion del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (String.IsNullOrWhiteSpace(txtContraseña.Text))
            {
                MessageBox.Show("El campo de contraseña no puede estar vacio", "Informacion del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!UsuariosNegocio.EsFormatoValido(txtUsuario.Text))
            {
                MessageBox.Show("El nombre de usuario no tiene un formato correcto", "Informacion del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //MessageBox.Show("Listo para iniciar sesion", "Informacion del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            this.Hide();
            //MDI_Control_escolar mdi =new MDI_Control_escolar();
            //mdi.Show();
            #region Ejemplo de region
            //codigooo
            #endregion
            this.DialogResult = DialogResult.OK;
            this.Close();


        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            //_logger.Info("Usuario accedio ha iniciar sesion");
            //_logger.Warn("Espacio en disco bajo");
            try { 
                try
                {
                int divisor = 0;
                int resultado = 10/divisor;//Esto genera una DivideByZeroException
                }
                catch(DivideByZeroException ex)
                {
                    //capturamos la primera excepcion y la envolvemos en otra
                    throw new ApplicationException("Error al realizar el caculo en la aplicacion");
                }
            }
            catch (Exception ex) 
            {
                //Aqui ouedes manejar la excepcion que contiene la inner exception
                //_logger.Error(ex, "Se produjo unerror en la operacion");

                //0 o registrar expecificamente usando la inner exception
                if(ex.InnerException != null)
                {
                    //_logger.Fatal(ex,$"Error crinico con detalle interno: {ex.InnerException.Message}");
                }

            }
        }
    }
}
