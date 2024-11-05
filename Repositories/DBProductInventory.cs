using Microsoft.Data.SqlClient;
using InventoryApplication.Interfaces;
using System.Data;

namespace InventoryApplication.Repositories
{
    public class DBProductInventory : IProductRepository
    {
        private readonly SqlConnection _connection;

        public DBProductInventory() {
            var connectionString = "Server=localhost,1433;Database=InventoryDB;User Id=SA;Password=SafePassw0rdYoink;Encrypt=False;";
            _connection = new SqlConnection(connectionString);
            _connection.Open();
        }
        public void AddProduct(Product product)
        {
            try {
                var query = "INSERT INTO Products (Name, Price, Quantity) VALUES (@Name, @Price, @Quantity)";
                using var cmd = new SqlCommand(query, _connection);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = product.Name;
                cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = product.Price;
                cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = product.Quantity;

                cmd.ExecuteNonQuery();
            } catch (SqlException ex) {
                System.Console.WriteLine($"An Error Occured while Adding the product: {ex.Message}");
            }
        }

        public void DeleteProduct(string name)
        {
            var query = "DELETE FROM Products WHERE Name = @Name";
            using var cmd = new SqlCommand(query, _connection);
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;

            cmd.ExecuteNonQuery();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = new List<Product>();
            var query = "SELECT Name, Price, Quantity FROM Products";
            try {
                using var cmd = new SqlCommand(query, _connection);

                using var reader = cmd.ExecuteReader();
                while(reader.Read()) {
                    products.Add(new Product {
                        Name = reader.GetString(0),
                        Price = reader.GetDecimal(1),
                        Quantity = reader.GetInt32(2),
                    });
                }
                return products;
            } catch(SqlException ex) {
                System.Console.WriteLine($"An error occurred while fetching products: {ex.Message}");
                return null;
            }
        }

        public Product GetProductByName(string name)
        {
            var query = "SELECT Name, Price, Quantity FROM Products WHERE Name = @Name";
            try {
                using var cmd = new SqlCommand(query, _connection);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                    {
                        return new Product {
                            Name = reader.GetString(0),
                            Price = reader.GetDecimal(1),
                            Quantity = reader.GetInt32(2),
                        };
                    }
            } catch(SqlException ex) {
                Console.WriteLine($"An error occurred while fetching the product: {ex.Message}");
            }
            return null;
        }

        public void UpdateProduct(Product product)
        {
            var query = "UPDATE Products SET Name = @NewName, Price = @Price, Quantity = @Quantity WHERE Name = @Name";
            try {
                using var cmd = new SqlCommand(query, _connection);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = product.Name;
                cmd.Parameters.Add("@NewName", SqlDbType.NVarChar).Value = product.Name;
                cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = product.Price;
                cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = product.Quantity;

                int rows = cmd.ExecuteNonQuery();
                if(rows >= 1) {
                    System.Console.WriteLine("Product set has been changed.");
                } else {
                    System.Console.WriteLine("No such product were found.");
                }

            } catch (SqlException ex) {
                System.Console.WriteLine($"An error occurred while updating the product: {ex.Message}");
            }
        }
    }
}