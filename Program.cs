using System;
using InventoryApplication.Repositories;

namespace InventoryApplication {
    class Program {
        internal static void Main() {
            Menu menu = new(new Inventory(new DBProductInventory()), new MenuUtils());
            menu.Start();
        }
    }

}
