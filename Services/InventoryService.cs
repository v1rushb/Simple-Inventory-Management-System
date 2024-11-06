namespace InventoryApplication.Services {
    public class InventoryService(IProductRepository productRepository)
    {
        private readonly IProductRepository _productRepository = productRepository;

        public async Task AddProductAsync(Product product) {
            await _productRepository.AddProductAsync(product);
        }

        public async Task ShowProductsAsync() {
            var prods = await _productRepository.GetAllProductsAsync();
            if(!prods.Any()) {
                System.Console.WriteLine("No products exist in the inventory.");
                return;
            }
            System.Console.WriteLine("Current Products: ");
            foreach(var prod in prods) {
                System.Console.WriteLine(prod);
            }
        }

        public async Task EditProductNameAsync(string name, string newName) {
            await _productRepository.EditProductNameAsync(name, newName);
        }

        public async Task EditProductPriceAsync(string name, decimal newPrice) {
            await _productRepository.EditProductPriceAsync(name, newPrice);
        }

        public async Task EditProductQuantityAsync(string name, int newQuantity) {
            await _productRepository.EditProductQuantityAsync(name, newQuantity);
        }

        public async Task DeleteProductAsync(string name) {
            await _productRepository.DeleteProductAsync(name);
        }

        public async Task SearchProductAsync(string name) {
            var products = await _productRepository.GetAllProductsAsync();
            var prod = products.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (prod == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }
            Console.WriteLine($"Product Found: {prod}");
        }
    }
}