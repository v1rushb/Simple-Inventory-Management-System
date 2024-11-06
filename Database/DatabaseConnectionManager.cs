using InventoryApplication.Constants;
using Microsoft.Data.SqlClient;

namespace InventoryApplication.Database {
    public static class DatabaseConnectionManager {
        private static readonly string _connectionString = DatabaseConstants.SqlServerConnectionString;

        public static SqlConnection GetConnection() {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}