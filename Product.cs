using System;

namespace InventoryApplication {
    public class Product {

        public string Name { get; set; } // eeh for now?
        public decimal Price { get; set; }
        public int Quantity {get; set ;} 

        public Product(string name, decimal price, int quantity) {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public override string ToString() {
            return $"Name: {Name}, Price: {Price}, Quantity: {Quantity}";
        }
    }
} 
