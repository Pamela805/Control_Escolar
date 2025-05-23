using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control_Escolar.Utilities
{
    internal class Formas
    {
        public static void InicializaForma(Form formChild, Form formParent)//
        {   //Inicializamos la forma

            //propiedades basicas
            formChild.MdiParent = formParent;//asignar el padre MDI
            formChild.FormBorderStyle = FormBorderStyle.Sizable;//perimitir redimensionar(se puede mover, grande o pequeño)
            formChild.MaximizeBox = true;//Permitir maximizar
            formChild.MinimizeBox = true;//Permitir minimizar
            formChild.WindowState = FormWindowState.Normal;//estado inicial de la ventana

            //Propiedades de control
            formChild.ControlBox = true;//mostrar botones de control(minimizar, maximizar, cerrar)
            formChild.ShowIcon = true;//mostrar el icono en la barra de titulo
            formChild.ShowInTaskbar = false;//no mostrar en la barra de tareas(ya que es una ventana hija)

            //Propiedades de tamaño
            formChild.AutoScaleMode = AutoScaleMode.Font;//Modo de escalado
            formChild.ClientSize = new Size(800, 600);//Tamaño inicial
            formChild.MinimumSize = new Size(400, 300);//Tamaño minimo permitido
            formChild.MaximumSize = new Size(3440, 1440);//tamaño maximo permitido

            //Propiedades de inicio
            formChild.StartPosition = FormStartPosition.CenterScreen;

            //Propiedades de comportamineto
            formChild.AutoScroll = true;//permitir el scroll si el contenido sobrepasa de la ventana
            formChild.KeyPreview = true;//permitir que reciba eventos del teclado
        }
    }
}
