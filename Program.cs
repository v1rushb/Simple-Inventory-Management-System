using System;
using System.Security.Cryptography;
using System.Xml.Serialization;

namespace InventoryApplication {
    class Program {
        internal static void Main(string[] args) {
            Inventory inv = new Inventory();

            while(true) {
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Display Products");
                Console.WriteLine("3. Edit Product");
                Console.WriteLine("4. Delete Product");
                Console.WriteLine("5. Lookup Product");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch(choice) {
                    case 1:
                        Console.Write("Enter product name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter product price: ");
                        decimal price = Convert.ToDecimal(Console.ReadLine());
                        Console.Write("Enter product quantity: ");
                        int quantity = Convert.ToInt32(Console.ReadLine());
                        Product newProduct = new Product(name, price, quantity);
                        inv.AddProduct(newProduct);
                        break;
                    case 2:
                        inv.ShowProducts();
                        break;

                    case 3:
                        Console.Write("Enter the name of the Product");
                        string updatedProductedName = Console.ReadLine();
                        inv.EditProduct(updatedProductedName);
                        break;
                    case 4:
                        Console.Write("Enter the name of the product to delete: ");
                        string deletedProductName = Console.ReadLine();
                        inv.DeleteProduct(deletedProductName);
                        break;
                    case 5:
                        Console.Write("Enter the name of the Product you want to look up: ");
                        string searchedProduct = Console.ReadLine();
                        inv.SearchProduct(searchedProduct);
                        break;
                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;

                }
            }
        }
    }

}