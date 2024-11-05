using InventoryApplication.Repositories;

namespace InventoryApplication {
    class Menu {
        private Inventory _inv;
        private MenuUtils _utils;

        public Menu(Inventory inv, MenuUtils utils) {
            _inv = inv;
            _utils = utils;
        }

        private void DisplayMenu() {
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Display Products");
            Console.WriteLine("3. Edit Product");
            Console.WriteLine("4. Delete Product");
            Console.WriteLine("5. Lookup Product");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");
        }


        public void Start() {
            Console.Clear();
            while(true) {
                DisplayMenu();
                int choice = _utils.GetValidatedInput();

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        AddProduct();
                        break;
                    case 2:
                        ShowProducts();
                        break;
                    case 3:
                        Console.Clear();
                        EditProduct();
                        break;
                    case 4:
                        Console.Clear();
                        DeleteProduct();
                        break;
                    case 5:
                        Console.Clear();
                        SearchProduct();
                        break;
                    case 6:
                        Console.Clear();
                        Console.WriteLine("Exiting...");
                        _utils.Delay(2);
                        Console.Clear();
                        return;
                }
            } 
        }

        private void AddProduct() {
            Console.WriteLine("Please enter product name: ");
            string name = _utils.GetValidatedStringInput();
            Console.WriteLine("Please enter the price: ");
            decimal price = _utils.GetValidatedDecimalInput();
            Console.WriteLine("Please enter the quantity of that product: ");
            int quantity = _utils.GetValidatedIntInput();
            Console.Clear();
            _inv.AddProduct(new Product { Name = name, Price = price, Quantity = quantity}); // probs simplify? ask later.
            _utils.Delay(2);
            Console.Clear();
        }

        private void ShowProducts() {
            Console.Clear();
            _inv.ShowProducts();
            GetBackToMenu();
        }

        private void EditProduct() {
            Console.WriteLine("Enter the name of the product: ");
            string name = _utils.GetValidatedStringInput();

            var existingProduct = _inv.GetProductByName(name);
            if (existingProduct == null)
            {
                Console.WriteLine("No such product exists.");
                _utils.Delay(2);
                Console.Clear();
                return;
            }

            System.Console.WriteLine($"Current Product: {existingProduct}");

            System.Console.WriteLine("Enter the new name (or press Enter to keep the current name):");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                existingProduct.Name = newName;
            }

            System.Console.Write("Enter new price (or press Enter to keep the current price): ");
            string priceInput = Console.ReadLine();
            if (decimal.TryParse(priceInput, out decimal newPrice))
            {
                existingProduct.Price = newPrice;
            }

            System.Console.Write("Enter new quantity (or press Enter to keep the current quantity): ");
            string quantityInput = Console.ReadLine();
            if (int.TryParse(quantityInput, out int newQuantity))
            {
                existingProduct.Quantity = newQuantity;
            }
            _inv.EditProduct(existingProduct);
            _utils.Delay(2);
            Console.Clear();
        }

        private void DeleteProduct() {
            Console.WriteLine("Enter the name of the product you wish to yoink: ");
            string name = _utils.GetValidatedStringInput();
            _inv.DeleteProduct(name);
            _utils.Delay(2);
            Console.Clear();
        }

        private void SearchProduct() {
            Console.WriteLine("Enter the name of the product you wish to look up for: ");
            string name = _utils.GetValidatedStringInput();
            _inv.SearchProduct(name);
            ContinueSearching();
        }
        
        private void ShowGetBacktToMenu() {
            Console.WriteLine("Get back to menu? [(y)es/(n)o]");
        }
        private void GetBackToMenu() {
            ShowGetBacktToMenu();
            string userResponse = _utils.GetValidetdYesOrNoString();
            bool decision = _utils.getDecision(userResponse);
            if(decision) {
                Console.Clear();
            } else {
                Console.Clear();
                ShowProducts();
            } // hmmm probably edit later to be recursive? not optimal but cooler.
        }

        private void DisplayContinueSearching() {
            Console.WriteLine("Do you wish to continue your search?");
        }
        private void ContinueSearching() {
            DisplayContinueSearching();
            string userResponse = _utils.GetValidetdYesOrNoString();
            bool decision = _utils.getDecision(userResponse);
            if(decision) {
                Console.Clear();
                SearchProduct();
            } else {
                Console.Clear();
            }
        }
    }
}