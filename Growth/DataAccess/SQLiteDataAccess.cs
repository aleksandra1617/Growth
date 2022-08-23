using Microsoft.Extensions.Logging;
using System.Data.SQLite;


namespace DataAccess
{
    public class SQLiteDataAccess : IDataAccess
    {
        private readonly ILogger<SQLiteDataAccess> _logger;
        private static SQLiteConnection? _connection;

        public const string _databaseName = "database.sqlite3";
        public const string _connectionString = "Data Source=" + _databaseName;

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

        public bool Connect()
        {
            _connection = new SQLiteConnection(_connectionString);
            _connection.Open();

            if (_connection.State == System.Data.ConnectionState.Open) 
                return true;

            return false;
        }

        public string GetData(){ return "Test Data"; }
    }
}
