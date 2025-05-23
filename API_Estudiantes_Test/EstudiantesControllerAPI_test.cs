using Control_EscolarCore.Controller;
using Microsoft.AspNetCore.Mvc;


namespace API_Estudiantes_Test
{
    //son objetos
    [ApiController]//
    [Route("api/[controller]")]//diseñar una ruta de acceso entre ese algo va haver un DOMINIO /api/nombre del controlador

    public class EstudiantesControllerAPI_test : ControllerBase
    {
        //controller base marca los errores  en codigos 400,404,
        private readonly EstudiantesController _estudiantesController;
        private readonly ILogger<EstudiantesControllerAPI_test> _logger;
        //no es el mismo del archivo de texto: ilogger muestra en consola, saca los errores en
        public EstudiantesControllerAPI_test(EstudiantesController estudiantesController, ILogger<EstudiantesControllerAPI_test> logger)
        {
            _estudiantesController = estudiantesController;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los estudiantes con filtros opcionales
        /// </summary>
        /// <param name="soloActivos">Filtrar solo estudiantes activos</param>
        /// <param name="tipoFecha">1=Fecha nacimiento, 2=Fecha alta, 3=Fecha baja</param>
        /// <param name="fechaInicio">Fecha inicial del rango</param>
        /// <param name="fechaFin">Fecha final del rango</param>
        /// <returns>Lista de estudiantes</returns>
        [HttpGet("list_estudiantes")]  // Ahora tiene una ruta específica
        public IActionResult GetEstudiantes(
             [FromQuery] bool soloActivos = true,
             [FromQuery] int tipoFecha = 0,
             [FromQuery] DateTime? fechaInicio = null,
             [FromQuery] DateTime? fechaFin = null)
        {
            //fromquery-los va obtener parametros de una cadena
            try
            {
                var estudiantes = _estudiantesController.ObtenerEstudiantes(
                    soloActivos,
                    tipoFecha,
                    fechaInicio,
                    fechaFin);

                return Ok(estudiantes);//el ok->devuelve la respuesta 200 que es un ok
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener estudiantes");
                return StatusCode(500, "Error interno del servidor" + ex.Message);
                //el error 500 hace referencia un error interno
            }
        }


        /// <summary>
        /// Actualiza los datos de un estudiante existente.
        /// </summary>
        /// <param name="idestudiante">Identificador del estudiante</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPut("actualizar_nombreestudiante")]
        public IActionResult ActualizarEstudiante([FromQuery] int idestudiante, [FromQuery] string nuevo_nombre)
        {
            try
            {
                var resultado = _estudiantesController.ActualizarNombreEstudiante(idestudiante, nuevo_nombre);

                if (resultado.exito)
                    return Ok(new { mensaje = resultado.mensaje });
                else
                    return BadRequest(new { mensaje = resultado.mensaje });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar estudiante");
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

    }
}
