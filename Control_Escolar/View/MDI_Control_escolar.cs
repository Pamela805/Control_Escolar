using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Control_Escolar.View
{
    public partial class MDI_Control_escolar : Form
    {
        public MDI_Control_escolar()
        {
            InitializeComponent();
        }

        private void MDI_Control_escolar_Load(object sender, EventArgs e)
        {

        }

        private void reporte11ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void estudiantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmEstudiantes forma_estudiantes = new frmEstudiantes(this);
            //forma_estudiantes.Show();//espera respuesta
            AbreVentanaHija("frmestudiantes");
        }

        private void reporte111ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmReporte111 forma_reporte111 = new frmReporte111(this);
            //forma_reporte111.Show();
            AbreVentanaHija("frmreporte111");

        }

        private void reporte12ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmReporte12 forma_reporte12 = new frmReporte12(this);
            //forma_reporte12.Show();
            AbreVentanaHija("frmreporte12");

        }

        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmRoles forma_roles = new frmRoles(this);
            //forma_roles.Show();
            AbreVentanaHija("frmroles");

        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmUsuarios forma_usuarios = new frmUsuarios(this);
            //forma_usuarios.Show();
            AbreVentanaHija("frmusuarios");
        }

        private void cascadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void mozaicoHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void mozaicoVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void AbreVentanaHija(string nombre_forma)
        {
            foreach (Form form in this.MdiChildren)
            {
                if (form.Name.ToLower() == nombre_forma)
                {
                    // Si la ventana ya está abierta, traerla al frente y restaurarla si estaba minimizada
                    form.WindowState = FormWindowState.Normal;
                    form.BringToFront();
                    return;
                }
            }

            // Si no está abierta, crear y mostrar una nueva instancia
            Form childForm;
            switch (nombre_forma.ToLower())
            {
                case "frmestudiantes":
                    childForm = new frmEstudiantes(this);
                    break;
                case "frmreporte111":
                    childForm = new frmReporte111(this);
                    break;
                case "frmreporte12":
                    childForm = new frmReporte12(this);
                    break;
                case "frmroles":
                    childForm = new frmRoles(this);
                    break;
                case "frmusuarios":
                    childForm = new frmUsuarios(this);
                    break;
                default:
                    return;
            }
            childForm.Show();
        }

    }
}
