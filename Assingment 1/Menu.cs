using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assingment_1
{
    public class Menu
    {
        private InventoryManager inventoryManager;

        public Menu(InventoryManager inventoryManager)
        {
            this.inventoryManager = inventoryManager;
        }
        public void ShowMainMenu()
        {

            while (true)
            {
                Console.WriteLine("Menu options");
                Console.WriteLine("1.Inventory Managment");
                Console.WriteLine("2.Shop");
                string inmenu = Console.ReadLine() ?? "";
                Console.WriteLine();
                var management = new List<string>() { };
                switch (inmenu)
                {
                    case "1":
                        inventoryManager.ManageInventory();
                        break;
                    case "2":
                        Shop shop = new Shop();
                        shop.manageShop(inventoryManager);
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }
    }
}
