using InventoryApplication.Repositories;
using InventoryApplication.Services;

namespace InventoryApplication {
    class Menu {
        private readonly InventoryService _inv;
        private readonly MenuUtils _utils;

        public Menu(InventoryService inv, MenuUtils utils) {
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


        public async Task StartAsync() {
            Console.Clear();
            while(true) {
                DisplayMenu();
                int choice = _utils.GetValidatedInput();

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        await AddProductAsync();
                        break;
                    case 2:
                        await ShowProductsAsync();
                        break;
                    case 3:
                        Console.Clear();
                        await EditProductAsync();
                        break;
                    case 4:
                        Console.Clear();
                        await DeleteProductAsync();
                        break;
                    case 5:
                        Console.Clear();
                        await SearchProductAsync();
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

        private async Task AddProductAsync() {
            Console.WriteLine("Please enter product name: ");
            string name = _utils.GetValidatedStringInput();
            Console.WriteLine("Please enter the price: ");
            decimal price = _utils.GetValidatedDecimalInput();
            Console.WriteLine("Please enter the quantity of that product: ");
            int quantity = _utils.GetValidatedIntInput();
            Console.Clear();
            await _inv.AddProductAsync(new Product { Name = name, Price = price, Quantity = quantity}); // probs simplify? ask later.
            _utils.Delay(2);
            Console.Clear();
        }

        private async Task ShowProductsAsync() {
            Console.Clear();
            await _inv.ShowProductsAsync();
            GetBackToMenu();
        }

        private async Task EditProductAsync()
        {
            Console.WriteLine("Enter the name of the product: ");
            string name = _utils.GetValidatedStringInput();

            Console.WriteLine("What would you like to edit?");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Price");
            Console.WriteLine("3. Quantity");
            Console.Write("Choose an option: ");
            int option = _utils.GetValidatedInput();

            switch (option)
            {
                case 1:
                    Console.WriteLine("Enter the new name:");
                    string newName = _utils.GetValidatedStringInput();
                    await _inv.EditProductNameAsync(name, newName);
                    break;
                case 2:
                    Console.WriteLine("Enter the new price:");
                    decimal newPrice = _utils.GetValidatedDecimalInput();
                    await _inv.EditProductPriceAsync(name, newPrice);
                    break;
                case 3:
                    Console.WriteLine("Enter the new quantity:");
                    int newQuantity = _utils.GetValidatedIntInput();
                    await _inv.EditProductQuantityAsync(name, newQuantity);
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
            _utils.Delay(2);
            Console.Clear();
        }

        private async Task DeleteProductAsync() {
            Console.WriteLine("Enter the name of the product you wish to yoink: ");
            string name = _utils.GetValidatedStringInput();
            await _inv.DeleteProductAsync(name);
            _utils.Delay(2);
            Console.Clear();
        }

        private async Task SearchProductAsync() {
            Console.WriteLine("Enter the name of the product you wish to look up for: ");
            string name = _utils.GetValidatedStringInput();
            await _inv.SearchProductAsync(name);
            await ContinueSearchingAsync();
        }
        
        private void ShowGetBacktToMenu() {
            Console.WriteLine("Get back to menu? [(y)es/(n)o]");
        }
        private async Task GetBackToMenu() {
            ShowGetBacktToMenu();
            string userResponse = _utils.GetValidetdYesOrNoString();
            bool decision = _utils.getDecision(userResponse);
            if(decision) {
                Console.Clear();
            } else {
                Console.Clear();
                await ShowProductsAsync();
            } // hmmm probably edit later to be recursive? not optimal but cooler.
        }

        private void DisplayContinueSearching() {
            Console.WriteLine("Do you wish to continue your search?");
        }
        private async Task ContinueSearchingAsync() {
            DisplayContinueSearching();
            string userResponse = _utils.GetValidetdYesOrNoString();
            bool decision = _utils.getDecision(userResponse);
            if(decision) {
                Console.Clear();
                await SearchProductAsync();
            } else {
                Console.Clear();
            }
        }
    }
}