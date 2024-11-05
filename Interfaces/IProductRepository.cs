using InventoryApplication;

namespace InventoryApplication.Interfaces {
    public interface IProductRepository {
        void AddProduct(Product product);
        IEnumerable<Product> GetAllProducts();
        Product GetProductByName(string name);
        void UpdateProduct(Product product);
        void DeleteProduct(string name);
    }

}