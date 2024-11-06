using InventoryApplication;

public interface IProductRepository
{
    Task AddProductAsync(Product product);
    Task DeleteProductAsync(string? name);
    Task EditProductNameAsync(string? name, string? newName);
    Task EditProductPriceAsync(string? name, decimal newPrice);
    Task EditProductQuantityAsync(string? name, int newQuantity);
    Task<List<Product>> GetAllProductsAsync();
}
