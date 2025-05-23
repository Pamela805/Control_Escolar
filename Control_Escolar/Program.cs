using Control_Escolar.View;
using NLog;
using Control_Escolar.Utilities;
using OfficeOpenXml;


namespace Control_Escolar
{
    internal static class Program
    {

        private static Logger? _Logger;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //_Logger = LoggingManager.GetLogger("Control_Escolar.Program");
            //_Logger.Info("Aplicacion iniciada");
            ExcelPackage.License.SetNonCommercialOrganization("My Noncommercial organization");

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            frmLogin login_form = new frmLogin();
            if (login_form.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new MDI_Control_escolar());
            }
        }
    }
}