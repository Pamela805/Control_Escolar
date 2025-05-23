using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Control_EscolarCore.Utilities;

namespace Control_EscolarCore.Bussines
{
    public class UsuariosNegocio
    {
        public static bool EsFormatoValido(string correo)
        {
            return Validaciones.EsCorreoValido(correo);//llama a utilities
        }
    }

    public class EstudiantesNegocio
    {
        public static bool EsCorreoValido(string correo)
        {
            return Validaciones.EsCorreoValido(correo);
        }

        public static bool EsCURPValido(string curp)
        {
            return Validaciones.EsCURPValido(curp);
        }


        public static bool EsNoControlValido(string nocontrol)
        {
            string patron = @"^(T|M)-\d{4}-\d{3,5}$";
            return Regex.IsMatch(nocontrol, patron);

        }







    }

}
