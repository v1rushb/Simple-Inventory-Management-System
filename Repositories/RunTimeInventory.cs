using InventoryApplication.Interfaces;

namespace InventoryApplication.Repositories {

    public class RunTimeInventory : IProductRepository {
        private readonly List<Product> _products = new();

        // public RunTimeInventory(List<Product> products) {
        //     _products = products;
        // }

        // public RunTimeInventory() {
        //     _products = new List<Product>();
        // }

        public void AddProduct(Product product) {
            _products.Add(product);
            Console.WriteLine($"Product {product.Name} added to inventory.");
        }
        // do the add quantity.

        public IEnumerable<Product> GetAllProducts() => _products;
        public void ShowProducts() {
            if(_products.Count == 0) {
                Console.WriteLine("No products exist in inv, time to add?");
                return; // probs return writeline? check later.
            }
                
            foreach(var product in _products) {
                Console.WriteLine(product);
            }
        }

        public void UpdateProduct(Product product)
{
            var existingProduct = _products.FirstOrDefault(p => p.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase));
            if (existingProduct == null) 
            {
                Console.WriteLine("No such product exists.");
                return;
            }
            existingProduct.Price = product.Price;
            existingProduct.Quantity = product.Quantity;
            existingProduct.Name = product.Name;

            Console.WriteLine("Product has been updated successfully.");
        }

        // public void UpdateProduct(string name) {
        //     var product = _products.Find(el => el.Name.Equals(name));

        //     if(product == null) {
        //         Console.WriteLine("No such product exists.");
        //         return;
        //     }

        //     if(product != null) {
        //         Console.WriteLine($"Current Product: {product}");
        //         Console.WriteLine("Enter the new name. (Press `Enter` to keep the current quantity):");

        //         string? newName = Console.ReadLine();
        //         if(!string.IsNullOrWhiteSpace(newName)) {
        //             product.Name = newName;
        //         }

        //         Console.Write("Enter new price. (Press `Enter` to keep the current quantity): ");

        //         string pricestr = Console.ReadLine();

        //         if(decimal.TryParse(pricestr, out decimal res)) {
        //             product.Price = res;
        //         }

        //         Console.Write("Enter new quantity. (Press `Enter` to keep the current quantity): ");
        //         string quantitystr = Console.ReadLine();
                
        //         if(int.TryParse(quantitystr, out int quantityint)) {
        //             product.Quantity = quantityint;
        //         }

        //         Console.WriteLine("Yoink! product's been updated successfully.");
        //     } else {
        //         Console.WriteLine("Oops, product not found!");
        //     }

        // }

        public void DeleteProduct(string name) {
            var product = _products.Find(el => el.Name.Equals(name));

            if(product != null) {
                _products.Remove(product);
                Console.WriteLine($"Product: {product} has been deleted!");
            } else {
                Console.WriteLine("Nothing to delete.");
            }
        }

        public Product? GetProductByName(string name) {
            var product = _products.Find(el => el.Name.Equals(name));
            if(product != null) {
                Console.WriteLine($"Product found, details: {product}");
                return product;
            }
            Console.WriteLine($"Product not found.");
            return null;
        }

        public void UpdateProduct(string name)
        {
            throw new NotImplementedException();
        }
    }
}
