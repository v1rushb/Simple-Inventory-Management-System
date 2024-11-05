using InventoryApplication.Interfaces;

namespace InventoryApplication.Repositories {
    public class Inventory {
        private readonly IProductRepository _productRepository;

        public Inventory(IProductRepository productRepository) {
            _productRepository = productRepository;
        }
        
        public void AddProduct(Product product) {
            _productRepository.AddProduct(product);
        }

        public void ShowProducts() {
            var prods = _productRepository.GetAllProducts();
            if(!prods.Any()) {
                System.Console.WriteLine("No products exist in the inventory.");
                return;
            }
            System.Console.WriteLine("Current Products: ");
            foreach(var prod in prods) {
                System.Console.WriteLine(prod);
            }
        }

        public void EditProduct(Product product) {
            _productRepository.UpdateProduct(product);
        }

        public void DeleteProduct(string name) {
            _productRepository.DeleteProduct(name);
        }

        public Product GetProductByName(string name) {
            return _productRepository.GetProductByName(name);
        }

        public void SearchProduct(string name) {
            var prod = GetProductByName(name);
            if(prod == null) {
                System.Console.WriteLine("Product Not found.");
                return;
            }
            System.Console.WriteLine($"Product Found: {prod}");
        }
    }
}