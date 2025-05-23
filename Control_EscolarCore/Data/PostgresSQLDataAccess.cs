using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using Npgsql;
using NLog;
using Control_EscolarCore.Utilities;

namespace Control_EscolarCore.Data
{
    /// <summary>
    /// Clase que maneja el acceso a datos PostgreSQL, incluyendo conexiones,
    /// y ejecucion de procedimientos almacenados
    /// </summary>
    public class PostgresSQLDataAccess
    {
        //Logger usando el LoggingManager centralizado
        private static readonly Logger _logger = LoggingManager.GetLogger("Control_Escolar.Data.PosrgreSQLDataAccess");

        //Cadena de conexion desde App.Config
        private static readonly String _connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        private NpgsqlConnection _connection;
        private static PostgresSQLDataAccess? _instance;

        private PostgresSQLDataAccess()
        {
            try
            {
                _connection = new NpgsqlConnection(_connectionString);
                _logger.Info("Instancia de acceso a datos creada correctamente");
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Error al inicializar el acceso a la base de datos");
                throw;
            }
        }

        /// <summary>
        /// Obtiene la instancia unica de la clase (Patrón Singleton)
        /// </summary>
        /// <returns>La instancia PostgresSQLDataAccess  </returns>
        public static PostgresSQLDataAccess GetInstance() {
            if (_instance == null)
            {
                _instance = new PostgresSQLDataAccess();
            }
            return _instance;
        }

        public bool Connect()
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                    _logger.Info("Coneccion a la base de datos exitosa");
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al conectar la base de datos");
                throw;
            }

        }

        public void Disconnect()
        {
            try
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                    _logger.Info("Cerrar a la base de datos exitosa");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al cerrar la conexion");
                throw;
            }

        }

        public DataTable ExecuteQuery_Reader(string query, params NpgsqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();

            try
            {
                _logger.Debug($"Ejecutando consulta: {query}");
                using (NpgsqlCommand command = CreateCommand(query, parameters))
                {//using para crear lo nuevo, cuando sale de llave libera memoria, de lo contrario para liberar, se usa al final de las llaves command.Dispose()
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))//Se necesita un adapter para crear lo nuevo
                    {
                        adapter.Fill(dataTable);//Es como el ExecuteReader
                        _logger.Debug($"Consulta ejecutada exitosamente. Filas obtenidas: {dataTable.Rows.Count}");//Obtiene el dato de la tabla
                    }
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al ejecutar la consulta: {query}");
                throw;
            }
        }

        private NpgsqlCommand CreateCommand(string query, NpgsqlParameter[] parameters)//recibir un postgres y mandar un parametro
        {//conjunto de paramet5ros de postgres
            NpgsqlCommand command = new NpgsqlCommand(query, _connection);

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);//Añadir un rango de parametro
                foreach (var parameter in parameters)//Si viene un dato nulo, ponlo, sino pon lo que se debe
                {
                    _logger.Trace($"Parámetro: {parameter.ParameterName}= {parameter.Value ?? "NULL"}");
                }
            }
            return command;
        }

        public int ExecuteNonQuery(string query, params NpgsqlParameter[] parameters)
        {
            try
            {
                _logger.Debug($"Ejecutando consulta: {query}");
                using (NpgsqlCommand command = CreateCommand(query, parameters))
                {//using para crear lo nuevo, cuando sale de llave libera memoria, de lo contrario para liberar, se usa al final de las llaves command.Dispose()
                    int result = command.ExecuteNonQuery();
                    _logger.Debug($"Consulta ejecutada exitosamente. Filas obtenidas: {result}");//Obtiene el dato de la tabla
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al ejecutar la consulta: {query}");
                throw;
            }
        }

        public object? ExecuteScalar(string query, params NpgsqlParameter[] parameters)
        {
            try
            {
                _logger.Debug($"Ejecutando consulta: {query}");
                using (NpgsqlCommand command = CreateCommand(query, parameters))
                {//using para crear lo nuevo, cuando sale de llave libera memoria, de lo contrario para liberar, se usa al final de las llaves command.Dispose()
                    object? result = command.ExecuteScalar();
                    _logger.Debug($"Consulta escalar ejecutada exitosamente. ID afectado: {result}");//Obtiene el dato de la tabla
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al ejecutar la consulta: {query}");
                throw;
            }
        }


        public NpgsqlParameter CreateParameter(string name, object value)
        {
            return new NpgsqlParameter(name, value ?? DBNull.Value);
        } 















    }
}
