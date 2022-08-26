using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SQLite;
using System.Reflection;

namespace DataAccess
{
    public class SQLiteDataAccess : IDataAccess
    {
        #region Private
        private readonly ILogger<SQLiteDataAccess> _logger;
        private static SQLiteConnection? _connection;
        private string _databaseName = "database.sqlite3";
        private string _connectionString = "Data Source=database.sqlite3";
        #endregion

        public SQLiteDataAccess(ILogger<SQLiteDataAccess> logger)
        {
            _logger = logger;

            if (!File.Exists("./" + _databaseName))
            {
                _logger.LogWarning("DB file not found! Creating DB File..");
                SQLiteConnection.CreateFile(_databaseName);
                _logger.LogInformation("DB file created!");
            }

            _logger.LogDebug("<<SQLiteDataAccess>> Constructed Successfully.");
        }

        public void SetConnectionString(string connectionStr) =>  _connectionString = connectionStr;


        public void SetDBName(string name)
        {
            if (name == null)
            {
                _logger?.LogError("<<SQLiteDataAccess::SetDBName>> Attempt to set DB name failed, the given DB name is null.");
                throw new ArgumentNullException("name");
            }
                
            _databaseName = name;
            _connectionString = "Data Source=" + _databaseName;
        } 

        public bool Connect()
        {
            _connection = new SQLiteConnection(_connectionString);
            
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                _logger.LogWarning("<<SQLiteDataAccess>> Redundant call to 'Connect', connection already open");
                return true;
            }
            
            _connection.Open();

            if (_connection.State == System.Data.ConnectionState.Open) 
                return true;

            
            _logger.LogError("<<SQLiteDataAccess>> Issue occured while attempting to open a connection to " + _databaseName);

            return false;
        }

        public string? GetData(string tableName)
        {
            string query = "SELECT * FROM " + tableName;
            SQLiteCommand cmd = new SQLiteCommand(query, _connection);
            DataTable dataTable = new DataTable(tableName);
            
            using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(cmd))
                dataAdapter.Fill(dataTable);

            if (dataTable.Rows.Count == 0)
            {
                _logger.LogWarning("<<SQLiteDataAccess::GetData>> The select query returned no data.");
                return null;
            }

            return dataTable.Rows[1].ItemArray[1].ToString(); 
        }

        public bool CreateTable()
        {
            throw new NotImplementedException();
        }
    }
}
