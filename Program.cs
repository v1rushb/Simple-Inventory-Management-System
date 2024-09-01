using System;

namespace InventoryApplication {
    class Program {
        internal static void Main() {
            Menu menu = new(new Inventory(), new MenuUtils());
            menu.Start();
        }
    }

}
