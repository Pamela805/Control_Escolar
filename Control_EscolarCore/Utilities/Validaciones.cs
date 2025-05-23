using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Control_EscolarCore.Utilities
{
    internal class Validaciones
    {

        #region Valida correo
        public static bool EsCorreoValido(string correo)
        {
            string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(correo, patron);//que coincidan el correo con el patron
        }
        #endregion
        public static bool EsCURPValido(string curp)
        {
            string patron = @"^[A-Z]{4}\d{6}[HM][A-Z]{5}[A-Z0-9]{2}$";
            return Regex.IsMatch(curp, patron);//que coincidan el curp con el patron
        }






    }
}
