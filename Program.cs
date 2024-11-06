using System;
using InventoryApplication.Repositories;
using InventoryApplication.Services;

namespace InventoryApplication {
    class Program {
        internal static async Task Main() {
            var menu = new Menu(new InventoryService(new MySQLProductRepository()), new MenuUtils());
            await menu.StartAsync();
        }
    }

}
