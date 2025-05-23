using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Npgsql;
using NpgsqlTypes;
using Control_EscolarCore.Utilities;
using Control_EscolarCore.Model;

namespace Control_EscolarCore.Data
{
    /// <summary>
    /// Clase que maneja las operaciones de acceso a daatos para la entidad Personas
    /// en la tabla seguridad .personas de PostgreSQL
    /// </summary>
    public class PersonaDataAccess
    {
        //Logger para esta clase
        private static readonly Logger _logger = LogManager.GetLogger("Control_Escolar.Data.PersonasDataAccess");

        //Instancia del acceso a datos PostgreSQL
        private readonly PostgresSQLDataAccess _dbAccess;

        public PersonaDataAccess()
        {
            try
            {
                //Obtiene la instancia unica de PostgreSQLDataAccess (patron Singleton)
                _dbAccess = PostgresSQLDataAccess.GetInstance();
            }
            catch (Exception ex) 
            {
                _logger.Fatal(ex,"Error al inicializar PersonaDataAccess");
                throw;
            }
        }

        public int InsertarPersona(Persona persona)
        {
            try
            {
                string query = "INSERT INTO seguridad.personas (nombre_completo, correo, telefono, fecha_nacimiento, curp, estatus) " +
                    "VALUES (@NombreCompleto, @Correo, @Telefono, @FechaNacimiento, @Curp, @Estatus) " +
                    "RETURNING id";

                //Crea los parametros
                NpgsqlParameter paramNombre = _dbAccess.CreateParameter("@NombreCompleto", persona.NombreCompleto);
                NpgsqlParameter paramCorreo = _dbAccess.CreateParameter("@Correo", persona.Correo);
                NpgsqlParameter paramTelefono = _dbAccess.CreateParameter("@Telefono", persona.Telefono);
                NpgsqlParameter paramFechaNac = _dbAccess.CreateParameter("@FechaNacimiento", persona.FechaNacimiento);
                NpgsqlParameter paramCurp = _dbAccess.CreateParameter("@Curp", persona.Curp);
                NpgsqlParameter paramEstatus = _dbAccess.CreateParameter("@Estatus", persona.Estatus);

                //Establece la coneccion a la BD
                _dbAccess.Connect();

                //Ejecuta la insercion y obtiene el ID generado
                object? resultado = _dbAccess.ExecuteScalar(query, paramNombre, paramCorreo, paramTelefono,
                    paramFechaNac, paramCurp, paramEstatus);

                //Convierte el resultado a entero
                int idGenerado = Convert.ToInt32(resultado);
                _logger.Info($"Persona insertada correctamente con ID {idGenerado}");
                return idGenerado;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al insertar la persona {persona.NombreCompleto}");
                return -1;
            }
            finally
            {
                //Asegura que se cierre la conexion 
                _dbAccess.Disconnect();
            }
        }

        public bool ExisteCurp(string curp)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM seguridad.personas WHERE curp = @Curp";
                NpgsqlParameter paramCurp = _dbAccess.CreateParameter("@Curp", curp);

                _dbAccess.Connect();

                object? resultado = _dbAccess.ExecuteScalar(query, paramCurp);
                int count = Convert.ToInt32(resultado);

                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al verificar la existencia del CURP {curp}");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

    public bool ActualizarPersona(Persona persona)
        {
            try
            {
                string query = "UPDATE seguridad.personas " +
                    "SET nombre_completo = @NombreCompleto, "+
                    "    correo  =@Correo, " +
                    "    telefono  =@Telefono, " +
                    "    fecha_nacimiento  =@FechaNacimiento, " +
                    "    curp  =@Curp, " +
                    "    estatus  =@Estatus, " +
                    "WHERE id = @Id";

                //Crea los parametros
                NpgsqlParameter paramId = _dbAccess.CreateParameter("@Id", persona.Id);
                NpgsqlParameter paramNombre = _dbAccess.CreateParameter("@NombreCompleto", persona.NombreCompleto);
                NpgsqlParameter paramCorreo = _dbAccess.CreateParameter("@Correo", persona.Correo);
                NpgsqlParameter paramTelefono = _dbAccess.CreateParameter("@Telefono", persona.Telefono);
                NpgsqlParameter paramFechaNac = _dbAccess.CreateParameter("@FechaNacimiento", persona.FechaNacimiento ?? (object)DBNull.Value);
                NpgsqlParameter paramCurp = _dbAccess.CreateParameter("@Curp", persona.Curp);
                NpgsqlParameter paramEstatus = _dbAccess.CreateParameter("@Estatus", persona.Estatus);

                //Establece la conexion a la BD
                _dbAccess.Connect();

                //Ejecuta la actualizacion 
                int filasAfectadas = _dbAccess.ExecuteNonQuery(query, paramId, paramNombre, paramCorreo, paramTelefono,
                    paramFechaNac, paramCurp, paramEstatus);

                bool exito = filasAfectadas > 0;
                if (exito)
                {
                    _logger.Info($"Persona con ID {persona.Id} actualizada correctamente");
                }
                else
                {
                    _logger.Warn($"No se pudo actualizar la persona con ID {persona.Id}. No se encontro el registro");
                }
                return exito;
            }
            catch(Exception ex)
            {
                _logger.Error($"Error al actualizar la persona con ID {persona.Id}");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        














    }
}
