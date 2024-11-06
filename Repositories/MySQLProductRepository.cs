using InventoryApplication.Database;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InventoryApplication.Repositories
{
    public class MySQLProductRepository : IProductRepository, IDisposable
    {
        private readonly SqlConnection _connection;

        public MySQLProductRepository() {
            _connection = DatabaseConnectionManager.GetConnection();
        }

        public async Task AddProductAsync(Product product)
        {
            try {
                var query = "INSERT INTO Products (Name, Price, Quantity) VALUES (@Name, @Price, @Quantity)";
                using var cmd = new SqlCommand(query, _connection);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = product.Name;
                cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = product.Price;
                cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = product.Quantity;

                await cmd.ExecuteNonQueryAsync();
            } catch (SqlException ex) {
                System.Console.WriteLine($"An Error Occured while adding the product: {ex.Message}");
            }
        }

        public async Task DeleteProductAsync(string? name)
        {
            var query = "DELETE FROM Products WHERE Name = @Name";
            using var cmd = new SqlCommand(query, _connection);
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task EditProductNameAsync(string? name, string? newName)
        {
            var query = "UPDATE Products SET Name = @NewName WHERE Name = @Name";
            using var cmd = new SqlCommand(query, _connection);

            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
            cmd.Parameters.Add("@NewName", SqlDbType.NVarChar).Value = newName;

            int rows = await cmd.ExecuteNonQueryAsync();
            if (rows >= 1)
            {
                Console.WriteLine("Product name has been updated.");
            }
            else
            {
                Console.WriteLine("No such product found.");
            }
        }

        public async Task EditProductPriceAsync(string? name, decimal newPrice)
        {
            var query = "UPDATE Products SET Price = @NewPrice WHERE Name = @Name";
            using var cmd = new SqlCommand(query, _connection);

            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
            cmd.Parameters.Add("@NewPrice", SqlDbType.Decimal).Value = newPrice;

            int rows = await cmd.ExecuteNonQueryAsync();
            if (rows >= 1)
            {
                Console.WriteLine("Product price has been updated.");
            }
            else
            {
                Console.WriteLine("No such product found.");
            }
        }


        public async Task EditProductQuantityAsync(string? name, int newQuantity)
        {
            var query = "UPDATE Products SET Quantity = @NewQuantity WHERE Name = @Name";
            using var cmd = new SqlCommand(query, _connection);

            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
            cmd.Parameters.Add("@NewQuantity", SqlDbType.Int).Value = newQuantity;

            int rows = await cmd.ExecuteNonQueryAsync();
            if (rows >= 1)
            {
                Console.WriteLine("Product quantity has been updated.");
            }
            else
            {
                Console.WriteLine("No such product found.");
            }
        }


        public async Task<List<Product>> GetAllProductsAsync()
        {
            var products = new List<Product>();
            var query = "SELECT Name, Price, Quantity FROM Products";
            try
            {
                using var cmd = new SqlCommand(query, _connection);

                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    products.Add(new Product
                    {
                        Name = reader.GetString(0),
                        Price = reader.GetDecimal(1),
                        Quantity = reader.GetInt32(2),
                    });
                }
                return products;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while fetching products: {ex.Message}");
                return null;
            }
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}