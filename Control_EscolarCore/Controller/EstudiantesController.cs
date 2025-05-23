using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Control_EscolarCore.Data;
using Control_EscolarCore.Utilities;
using Control_EscolarCore.Model;
using OfficeOpenXml;

namespace Control_EscolarCore.Controller//es donde se pone la conexion de los botones
{

    /// <summary>
    /// 
    /// </summary>
    public class EstudiantesController
    {
        private static readonly Logger _logger = LoggingManager.GetLogger("Control_Escolar.Controller.EstudiantesController");
        private readonly EstudiantesDataAccess _estudiantesData;
        private readonly PersonaDataAccess _personasData;

        public EstudiantesController()
        {
            try
            {
                _estudiantesData = new EstudiantesDataAccess();
                _personasData = new PersonaDataAccess();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al inicializar el controlador de estudiantes");
                throw;
            }
        }

        public List<Estudiante> ObtenerEstudiantes(bool soloActivos = true, int tipoFecha = 0, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            try
            {
                //Obtener datos de la capa de acceso
                var estudiantes = _estudiantesData.ObtenerTodosLosEstudiantes(soloActivos, tipoFecha, fechaInicio, fechaFin);//var 
                _logger.Info($"Se obtuvieron {estudiantes.Count} estudiantes");
                return estudiantes;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener la lista de estudiantes");
                throw;//Obtener la excepcion para que la capa superior la maneje
            }
        }

        public (int id, string mensaje) RegistrarEstudiante(Estudiante estudiante)
        {
            try
            {
                //Verificar si la matricula ya existe
                if (_estudiantesData.ExisteMatricula(estudiante.Matricula))
                {
                    _logger.Warn($"Intento de registrar estudiante con matricula duplicada: {estudiante.Matricula}");
                    return (-3, $"La matricula{estudiante.Matricula} ya esta registrada en el sistema");
                }
                //Verificar si el CURP ya existe
                if (_personasData.ExisteCurp(estudiante.DatosPersonales.Curp))
                {
                    _logger.Warn($"Intento de registrar estudiante con el CURP duplicado: {estudiante.DatosPersonales.Curp}");
                    return (-2, $"El CURP: {estudiante.DatosPersonales.Curp} ya esta registrado en el sistema");
                }

                //Registrar el estudiante
                _logger.Info($"Registrando nuevo estudiante {estudiante.DatosPersonales.NombreCompleto}, Matricula: {estudiante.Matricula}");
                int idEstudiante = _estudiantesData.InsertarEstudiante(estudiante);

                if (idEstudiante <= 0)
                {
                    return (-4, "Error al registrar el estudiante en la base de datos");
                }

                _logger.Info($"Estudiante registrado exitosamente con ID: {idEstudiante}");
                return (idEstudiante, "Estudiante registrado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.Error($"Error al registrar al estudiante: {estudiante.DatosPersonales?.NombreCompleto ?? "Sin nombre"}, Matricula: {estudiante.Matricula}");
                return (-5, $"Error inesperado: {ex.Message}");
            }
        }

        public Estudiante? ObtenerDetalleEstudiante(int idEstudiante)
        {
            try
            {
                _logger.Debug($"Solicitando detalle del estudiante con ID: {idEstudiante}");
                return _estudiantesData.ObtenerEstudiantePorId(idEstudiante);
            }
            catch(Exception ex)
            {
                _logger.Error(ex, $"Error al obtener detalles del estudiante con ID: {idEstudiante}");
                throw;
            }
        }

        public (bool exito, string mensaje) ActualizarEstudiante(Estudiante estudiante)
        {
            try
            {
                //Validaciones basicas
                if(estudiante == null)
                {
                    return (false, "No se proporcionaron datos del estudiante");
                }
                if (estudiante.Id <= 0)
                {
                    return (false, "ID del estusiante no valido");
                }
                if (estudiante.DatosPersonales == null)
                {
                    return (false, "No se proporcionaron datos personales del estudiante");
                }
                //Verificar si el estudiante existe
                Estudiante? estudianteExistente = _estudiantesData.ObtenerEstudiantePorId(estudiante.Id);
                if(estudianteExistente == null)
                {
                    return (false, $"No se encontro el estudiante con ID: {estudiante.Id}");
                }

                //Verificar si la matricula no este duplicada (si ah cambiado)
                if (estudiante.Matricula != estudianteExistente.Matricula
                    && _estudiantesData.ExisteMatricula(estudiante.Matricula))
                {
                    return (false, $"La matricula {estudiante.Matricula} ya esta registrada en el sistema");
                }

                //Verificar si la matricula no este duplicada (si ah cambiado)
                if (estudiante.Matricula != estudianteExistente.Matricula
                    && _estudiantesData.ExisteMatricula(estudiante.Matricula))
                {
                    return (false, $"La matricula {estudiante.Matricula} ya esta registrada en el sistema");
                }

                //Verificar que el CURP no este duplicado (si ah cambiado)
                if (estudiante.DatosPersonales.Curp!= estudianteExistente.DatosPersonales.Curp)
                {
                    //Buscar si existe otra persona con el mismo curp que no sea la persona de este estudiante
                    bool personaConMismoCurp = _personasData.ExisteCurp(estudiante.DatosPersonales.Curp);
                    if (personaConMismoCurp)
                    {
                        return (false, $"El CURP {estudiante.DatosPersonales.Curp} ya esta registrado para otra persona");
                    }
                }

                //Actualizar el estudiante
                _logger.Info($"Actualizando estudiante con ID: {estudiante.Id}, Nombre: {estudiante.DatosPersonales.NombreCompleto}");
                bool resultado = _estudiantesData.ActualizarEstudiante(estudiante);

                if(!resultado)
                {
                    _logger.Error($"Error al actualizar el estudiante con el ID: {estudiante.Id}");
                    return (false, "Error al actualizar el estudiante en la base de datos");
                }

                _logger.Info($"El estudiante con el ID: {estudiante.Id} actualizado exitosamente");
                return (true, "Estudiante actualizado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error");
                throw;
            }
        }


        public bool ExportarEstudiantesExcel(string rutaArchivo, bool soloActivos = true, int tipoFecha = 0, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            try
            {
                //Obtener los estudiantes
                var estudiantes = ObtenerEstudiantes(soloActivos, tipoFecha, fechaInicio, fechaFin);

                if (estudiantes == null || estudiantes.Count == 0)
                {
                    _logger.Warn("No hay estudiantes para exportar");
                    return false;
                }

                //Crear archivo Excel //using para liberar memoria, no saturar la memoria
                using (var package = new ExcelPackage()) 
                { //Crea una hoja de trabajo
                    var worksheet = package.Workbook.Worksheets.Add("Estudiantes");

                    //Establecer encabezados
                    worksheet.Cells[1, 1].Value = "Matricula";
                    worksheet.Cells[1, 2].Value = "Nombre Completo";
                    worksheet.Cells[1, 3].Value = "Semestre";
                    worksheet.Cells[1, 4].Value = "Correo";
                    worksheet.Cells[1, 5].Value = "Telefono";
                    worksheet.Cells[1, 6].Value = "CURP";
                    worksheet.Cells[1, 7].Value = "Fecha de Nacimiento";
                    worksheet.Cells[1, 8].Value = "Fecha Alta";
                    worksheet.Cells[1, 9].Value = "Fecha Baja";
                    worksheet.Cells[1, 10].Value = "Estado";

                    using (var range = worksheet.Cells[1, 1, 1, 10])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                        range.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                    }

                    //llenar los datos
                    int row = 2;//se va a colocar en la siguiente fila
                    foreach(var estudiante in estudiantes)
                    {
                        worksheet.Cells[row, 1].Value = estudiante.Matricula;
                        worksheet.Cells[row, 2].Value = estudiante.DatosPersonales.NombreCompleto;
                        worksheet.Cells[row, 3].Value = estudiante.Semestre;
                        worksheet.Cells[row, 4].Value = estudiante.DatosPersonales.Correo;
                        worksheet.Cells[row, 5].Value = estudiante.DatosPersonales.Telefono;
                        worksheet.Cells[row, 6].Value = estudiante.DatosPersonales.Curp;
                        worksheet.Cells[row, 7].Value = estudiante.DatosPersonales.FechaNacimiento;
                        worksheet.Cells[row, 8].Value = estudiante.FechaAlta;
                        worksheet.Cells[row, 9].Value = estudiante.FechaBaja;
                        worksheet.Cells[row, 10].Value = estudiante.DescripcionEstatus;

                        //Aplicar formato a las fechas
                        if (estudiante.DatosPersonales.FechaNacimiento.HasValue)
                        {
                            worksheet.Cells[row, 7].Style.Numberformat.Format = "dd/MM/yyyy";
                        }
                        worksheet.Cells[row, 8].Style.Numberformat.Format = "dd/MM/yyyy";
                        if (estudiante.FechaBaja.HasValue) 
                        {
                            worksheet.Cells[row, 9].Style.Numberformat.Format = "dd/MM/yyyy";
                        }
                        row++;
                    }

                    //Ajustar el ancho de las columnas
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    //Guardar el archivo 
                    FileInfo fileInfo = new FileInfo(rutaArchivo);
                    package.SaveAs(fileInfo);

                    _logger.Info($"Archivo Excel exportado correctamente: {rutaArchivo}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al exportar estudiantes a Excel");
                throw;
            }
        }


















        //public List<Estudiante> BuscarEstudiantes(string terminoBusqueda)
        //{
        //    try
        //    {
        //        if (string.IsNullOrWhiteEmpty(terminoBusqueda));

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error(ex, "Error al obtener la lista de estudiantes");
        //        throw;//Obtener la excepcion para que la capa superior la maneje
        //    }
        //}


















































    }
}
