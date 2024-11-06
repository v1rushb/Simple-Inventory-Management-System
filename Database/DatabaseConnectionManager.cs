using System.Net;
using InventoryApplication.Constants;
using Microsoft.Data.SqlClient;

namespace InventoryApplication.Database {
    public class DatabaseConnectionManager {
        private readonly string _connectionString;

        public DatabaseConnectionManager() {
            _connectionString = DatabaseConstants.SqlServerConnectionString;
            CreateProductTableAsync().Wait();
        }

        public SqlConnection GetConnection() {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        public async Task CreateProductTableAsync() {
            var query = @"
                IF NOT EXISTS (
                    SELECT * FROM INFORMATION_SCHEMA.TABLES 
                    WHERE TABLE_NAME = 'Products'
                )
                CREATE TABLE Products (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    Name NVARCHAR(100) NOT NULL,
                    Price DECIMAL(18, 2) NOT NULL,
                    Quantity INT NOT NULL
                );";
            using var connection = GetConnection();
            var cmd = new SqlCommand(query, connection);
            try {
                await cmd.ExecuteNonQueryAsync();
                System.Console.WriteLine("Products table has been created (if it didn't already exist).");
            } catch (SqlException ex) {
                System.Console.WriteLine($"An error occurred while creating the Products table: {ex.Message}");
            }
        }
    }
}