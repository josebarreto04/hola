using Assingment_1;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Assingment_1
{

    class Program
    {
        static void Main()
        {
            InventoryManager inventoryManager = new InventoryManager();
            Menu menu = new Menu(inventoryManager);
            menu.ShowMainMenu();
        }
    }

   

       
}






