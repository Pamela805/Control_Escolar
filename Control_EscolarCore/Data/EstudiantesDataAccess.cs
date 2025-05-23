using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Npgsql;
using NpgsqlTypes;
using NLog;//Puros nugett
using Control_EscolarCore.Utilities;//tenemos loggin
using Control_EscolarCore.Model;//clases estudiantes y persona

namespace Control_EscolarCore.Data
{
    public class EstudiantesDataAccess
    {
        //Logger para la clase
        private static readonly Logger _logger = LoggingManager.GetLogger("Control_Escolar.Data");

        //Instancia del acceso a datos PostgreSQL
        private readonly PostgresSQLDataAccess _dbAccess;

        //Instancia de la clase para manejo de personas
        private readonly PersonaDataAccess _personaData;



        /// <summary>
        /// Constructor de la clase EstudiantesDataAccess
        /// </summary>
        public EstudiantesDataAccess()
        {
            try
            {
                //Obtiene la instancia unica de Postrgre (patron singleton)
                _dbAccess = PostgresSQLDataAccess.GetInstance();
                //Instancia al acceso a datos de personas para operaciones relacionadas
                _personaData = new PersonaDataAccess();
            }
            catch (Exception ex) 
            {
                _logger.Fatal(ex,"Error al inicializar EstudiantesDataAccess");
                throw;
            }
        }


        public List<Estudiante> ObtenerTodosLosEstudiantes (bool soloActivos = true, int tipoFecha = 0,
                                                        DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            List<Estudiante > estudiantes = new List<Estudiante>();

            try
            {
                string query = @"
                    SELECT e.id,e.matricula,e.semestre,e.fecha_alta,e.fecha_baja,e.estatus,
                            CASE
                                WHEN e.estatus=0 THEN 'Baja'
                                WHEN e.estatus=1 THEN 'Activo'
                                WHEN e.estatus=2 THEN 'Baja temporal'
                                    ELSE
                                        'Desconocido'
                            END AS descestatus_estudiante,
                                e.id_persona,p.nombre_completo,p.correo,p.telefono,p.fecha_nacimiento,p.curp,p.estatus as estatus_persona
                    FROM escolar.estudiantes e
                    INNER JOIN seguridad.personas p ON e.id_persona=p.id
                    WHERE 1=1";//Iniciamos con na condicion siempre verdadeera para facilitar la adicion de filros 


                List <NpgsqlParameter> parametros = new List<NpgsqlParameter>();
                //Filtro por estatus(activos/inactivos)
                if (soloActivos)
                {
                    query += " AND e.estatus = 1";
                }

                //Filtro por rago de fechas
                if (fechaInicio != null && fechaFin != null)
                {
                    switch (tipoFecha)
                    {
                        case 1://fecha de nacimiento
                            query += " AND p.fecha_nacimiento BETWEEN @FechaInicio AND @FechaFin";
                            break;
                        case 2://decha alta
                            query += " AND e.fecha_alta BETWEEN @FechaInicio AND @FechaFin";
                            break;
                        case 3://fecha baja
                            query += " AND e.fecha_baja BETWEEN @FechaInicio AND @FechaFin";
                            break;
                    }
                    parametros.Add(_dbAccess.CreateParameter("@FechaInicio", fechaInicio.Value));//@=parametro
                    parametros.Add(_dbAccess.CreateParameter("@FechaFin", fechaFin.Value));
                }
                //ordernar por id o matricula
                query += " ORDER BY e.id";

                //Establecer la conexion a la BD
                _dbAccess.Connect();

                //Ejecuta la consulta con parametros
                DataTable resultado = _dbAccess.ExecuteQuery_Reader(query, parametros.ToArray());

                //Cnovertir los resultados a objetos estudiantes
                foreach(DataRow row in resultado.Rows)
                { ///DataRow cada fila
                    //Crear el objetos Persona
                    Persona persona = new Persona(
                        Convert.ToInt32(row["id_persona"]),
                        row["nombre_completo"].ToString() ?? "",
                        row["correo"].ToString() ?? "",
                        row["telefono"].ToString() ?? "",
                        row["curp"].ToString() ?? "",
                        row["fecha_nacimiento"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["fecha_nacimiento"]) : null ,
                        Convert.ToBoolean(row["estatus_persona"])
                        );

                    //Crear el objetos Estudiante
                    Estudiante estudiante = new Estudiante(
                        Convert.ToInt32(row["id"]),
                        Convert.ToInt32(row["id_persona"]),//ID de la persona posiblemnete se tiene que borrar
                        row["matricula"].ToString() ?? "",
                        row["semestre"].ToString() ?? "",
                        Convert.ToDateTime(row["fecha_alta"]),
                        row["fecha_baja"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["fecha_baja"]) : null,
                        Convert.ToInt32(row["estatus"]),
                         row["descestatus_estudiante"].ToString() ?? "",
                         persona
                        );

                    estudiantes.Add(estudiante);               
                }
                _logger.Debug($"Se obtuvieron {estudiantes.Count} registros de estudiantes");
                return estudiantes;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtenere los estudiantes de la base de datos");
                throw;//retorna la lista vacia en caso de error
            }
            finally //Asegura que se cierra la conexion
            {
                _dbAccess.Disconnect();
            }
        }

        public int InsertarEstudiante(Estudiante estudiante)
        {
            try
            {
                //Primero insertamos la persona
                int idPersona = _personaData.InsertarPersona(estudiante.DatosPersonales);

                if(idPersona <= 0)
                {
                    _logger.Error($"No se pudo insertar la persona para el estudiante{estudiante.Matricula}");
                    return -1;
                }
                //Actualizar el IdPersona en el objeto estudiante
                estudiante.IdPersona = idPersona;

                //Luego insertamos el estudiante
                string query = @"
                    INSERT INTO escolar.estudiantes(id_persona, matricula, semestre, fecha_alta, estatus)
	                    VALUES (@IdPersona, @Matricula, @Semestre, @FechaAlta, @Estatus)
	                    RETURNING id ";

                //Crea los parametros
                NpgsqlParameter paramIdPersona = _dbAccess.CreateParameter("@IdPersona", estudiante.IdPersona);
                NpgsqlParameter paramMatricula = _dbAccess.CreateParameter("@Matricula", estudiante.Matricula);
                NpgsqlParameter paramSemestre = _dbAccess.CreateParameter("@Semestre", estudiante.Semestre);
                NpgsqlParameter paramFechaAlta = _dbAccess.CreateParameter("@FechaAlta", estudiante.FechaAlta);
                NpgsqlParameter paramEstatus = _dbAccess.CreateParameter("@Estatus", estudiante.Estatus);

                //Establece conexion a la BD
                _dbAccess.Connect();

                //Ejecuta la insercion y obtiene el ID generado
                object? resultado = _dbAccess.ExecuteScalar(query, paramIdPersona, paramMatricula, paramSemestre,
                    paramFechaAlta, paramEstatus);


                //Convierte el resultado a entero
                int idEstudiante_generado = Convert.ToInt32(resultado);
                _logger.Info($"Estudiante insertado correctamente con ID {idEstudiante_generado}");

                //Actualizar el ID en el objeto estudiante
                //estudiante.Id = idEstudiante_generado;
                return idEstudiante_generado;
            }
            catch(Exception ex)
            {
                _logger.Error(ex, $"Error al insertar el estudiante con matricula {estudiante.Matricula}");
                return -1;
            }
            finally
            {
                //Asegura  que se cierre la conexion
                _dbAccess.Disconnect();
            }
        }

        public bool ExisteMatricula(string matricula)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM escolar.estudiantes WHERE matricula = @Matricula";

                //Crea el perimetro
                NpgsqlParameter paramMatricula = _dbAccess.CreateParameter(@"Matricula", matricula);

                //Establece la coneccion a la BD
                _dbAccess.Connect();

                //Ejecuta la consulta
                object? resultado = _dbAccess.ExecuteScalar(query, paramMatricula);

                int cantidad = Convert.ToInt32(resultado);
                bool existe = cantidad > 0;

                return existe;
            }
            catch(Exception ex)
            {
                _logger.Error(ex, $"Error al verificar la existencia de la matricula{matricula}");
                return false;
            }
            finally
            {
                //Asegura que se cierre la conexion
                _dbAccess.Disconnect();
            }
        }


        public Estudiante? ObtenerEstudiantePorId(int id)
        {
            try
            {
                string query = @"
                    SELECT e.id, e.matricula, e.semestre, e.fecha_alta, e.fecha_baja, e.estatus,
                           e.id_persona, p.nombre_completo, p.correo, p.telefono, p.fecha_nacimiento, p.curp, p.estatus as estatus_persona
                    FROM escolar.estudiantes e
                    INNER JOIN seguridad.personas p ON e.id_persona = p.id
                    WHERE e.id = @Id";

                //Crea un parametro
                NpgsqlParameter paramId = _dbAccess.CreateParameter("@Id", id);

                //Establece la conexion a la BD
                _dbAccess.Connect();

                //Ejecuta la consulta con el parametro
                DataTable resultado = _dbAccess.ExecuteQuery_Reader(query, paramId);

                if(resultado.Rows.Count == 0)
                {
                    _logger.Warn($"No se encontro ningun estudiante con ID: {id}");
                    return null;
                }

                //Obtiene la primera fila (deberia ser la unica)
                DataRow row = resultado.Rows[0];

                //Crea el objeto persona
                Persona persona = new Persona(
                        Convert.ToInt32(row["id_persona"]),
                        row["nombre_completo"].ToString() ?? "",
                        row["correo"].ToString() ?? "",
                        row["telefono"].ToString() ?? "",
                        row["curp"].ToString() ?? "",
                        row["fecha_nacimiento"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["fecha_nacimiento"]) : null,
                        Convert.ToBoolean(row["estatus_persona"])
                 );

                //Crea el objeto estudiante
                Estudiante estudiante = new Estudiante(
                        Convert.ToInt32(row["id"]),
                        Convert.ToInt32(row["id_persona"]),
                        row["matricula"].ToString() ?? "",
                        row["semestre"].ToString() ?? "",
                        Convert.ToDateTime(row["fecha_alta"]),
                        row["fecha_baja"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["fecha_baja"]) : null,
                        Convert.ToInt32(row["estatus"]),
                        row["desestatus_estudiante"].ToString() ?? "Desconocido",
                        persona
                 );
                return estudiante;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al obtener el estudiante con ID {id}");
                return null;
            }
            finally
            {
                //Asegura que se cierre la conexion
                _dbAccess.Disconnect();
            }


        }


        public bool ActualizarEstudiante(Estudiante estudiante)
        {
            try
            {
                string queryEstudiante = @"
                    UPDATE escolar.estudiantes
                    SET matricula = @Matricula, 
                        semestre = @Semestre
                        fecha_alta = @FechaAlta, 
                        estatus = @Estatus
                        fecha_baja = @FechaBaja, 
                    WHERE id = @IdEstudiante";

                //Establece la conexion a la BD
                _dbAccess.Connect();

                //Crea los parametros
                NpgsqlParameter paramIdEstudiante = _dbAccess.CreateParameter("@IdEstudiante", estudiante.Id);
                NpgsqlParameter paramMatricula = _dbAccess.CreateParameter("@Matricula", estudiante.Matricula);
                NpgsqlParameter paramSemestre = _dbAccess.CreateParameter("@Semestre", estudiante.Semestre);
                NpgsqlParameter paramFechaAlta = _dbAccess.CreateParameter("@FechaAlta", estudiante.FechaAlta);
                NpgsqlParameter paramEstatus = _dbAccess.CreateParameter("@Estatus", estudiante.Estatus);
                NpgsqlParameter paramFechaBaja = _dbAccess.CreateParameter("@FechaBaja",
                    estudiante.FechaBaja.HasValue ? (object)estudiante.FechaBaja.Value : DBNull.Value);

                //Ejecuta la actualizacion 
                int filasAfectadasEstudiante = _dbAccess.ExecuteNonQuery(queryEstudiante, paramIdEstudiante, paramMatricula, paramSemestre,
                    paramEstatus, paramFechaBaja);

                bool exito = filasAfectadasEstudiante > 0;
                if (!exito)
                {
                    _logger.Warn($"No se pudo actualizar el estudiante con ID {estudiante.Id}. No se encontro el registro");
                }
                else
                {
                    _logger.Warn($"Estudiante con ID {estudiante.Id} actualizado correctamente");
                }
                return exito;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error al actualizar el estudiante con ID {estudiante.Id}");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }














    }
}
