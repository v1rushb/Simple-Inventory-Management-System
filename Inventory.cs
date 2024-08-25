using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace InventoryApplication {

    public class Inventory {
        private List<Product> _products;

        public Inventory(List<Product> products) {
            this._products = products;
        }

        public Inventory() {
            _products = new List<Product>();
        }

        public void AddProduct(Product product) {
            _products.Add(product);
            Console.WriteLine($"Product {product.Name} added to inventory.");
        }
        // do the add quantity.

        public void ShowProducts() {
            if(_products.Count == 0) {
                Console.WriteLine("No products exist in inv, time to add?");
                return; // probs return writeline? check later.
            }
                
            foreach(var product in _products) {
                Console.WriteLine(product);
            }
        }

        public void EditProduct(string name) {
            var product = _products.Find(el => el.Name.Equals(name));

            if(product != null) {
                Console.WriteLine($"Current Product: {product}");
                Console.WriteLine("Enter the new name.");

                string? newName = Console.ReadLine();
                if(!string.IsNullOrWhiteSpace(newName)) {
                    product.Name = newName;
                }

                Console.Write("Enter new price: ");

                string pricestr = Console.ReadLine();

                if(decimal.TryParse(pricestr, out decimal res)) {
                    product.Price = res;
                }

                Console.Write("Enter new quantity (leave empty to keep the current quantity): ");
                string quantitystr = Console.ReadLine();
                
                if(int.TryParse(quantitystr, out int quantityint)) {
                    product.Quantity = quantityint;
                }

                Console.WriteLine("Yoink! product's been updated successfully.");
            } else {
                Console.WriteLine("Oops, product not found!");
            }

        }

        public void DeleteProduct(string name) {
            var product = _products.Find(el => el.Name.Equals(name));

            if(product != null) {
                _products.Remove(product);
                Console.WriteLine($"Product: {product} has been deleted!");
            } else {
                Console.WriteLine("Nothing to delete.");
            }
        }

        public void SearchProduct(string name) {
            var product = _products.Find(el => el.Name.Equals(name));
            if(product != null) {
                Console.WriteLine($"Product found, details: {product}");
                return;
            }
            Console.WriteLine($"Product not found.");
        }
    }
}