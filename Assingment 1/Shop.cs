using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assingment_1
{
    public class Shop
    {
        public void manageShop(InventoryManager inventoryManager)
        {
            List<Items> cart = new List<Items>();

            while (true)
            {

                Console.WriteLine("1.add item to the cart");
                Console.WriteLine("2.remove item from cart");
                Console.WriteLine("3.Checkout");
                Console.WriteLine("4. menu options");
                Console.WriteLine("5.back to menu");

                string inshop = Console.ReadLine() ?? "";
                Console.WriteLine();



                switch (inshop)
                {
                    case "1":
                        AddItemToCart(inventoryManager, cart);
                        break;
                    case "2":
                        RemoveItemFromCart(inventoryManager, cart);

                        break;
                    case "3":
                        Checkout(cart);
                        Environment.Exit(0);
                        return;
                    case "4":
                        Console.WriteLine("Current Inventory:");
                        List<Items> inventory = inventoryManager.GetInventory();
                        foreach (var item in inventory)
                        {
                            Console.WriteLine($"Name: {item.Name}");
                            Console.WriteLine($"Description: {item.Description}");
                            Console.WriteLine($"Price: {item.Price}");
                            Console.WriteLine($"ID: {item.ID}");
                            Console.WriteLine($"Quantity: {item.Quantity}");
                            Console.WriteLine(); //

                        }
                        break;
                    case "5":
                        return;


                }
            }



        }
        private static void AddItemToCart(InventoryManager inventoryManager, List<Items> cart)
        {
            List<Items> inventory = inventoryManager.GetInventory();
            if (inventory == null || inventory.Count == 0)
            {
                Console.WriteLine("No items available in the inventory.");
                return;
            }

            Console.WriteLine("Available items:");
            foreach (var item in inventory)
            {
                Console.WriteLine($"ID: {item.ID}, Name: {item.Name}, Price: {item.Price}, Quantity: {item.Quantity}");
            }

            Console.Write("Enter the ID of the item to add to the cart: ");
            int itemId;
            while (!int.TryParse(Console.ReadLine(), out itemId))
            {
                Console.WriteLine("Invalid ID. Please enter a valid integer number.");
                Console.Write("Enter the ID of the item to add to the cart: ");
            }

            Items? selectedItem = inventory.Find(item => item.ID == itemId);
            if (selectedItem != null && selectedItem.Quantity > 0)
            {
                Items? itemInCart = cart.Find(item => item.ID == itemId);
                if (itemInCart != null)
                {
                    itemInCart.Quantity++;
                }
                else
                {
                    Items itemToAdd = new Items(selectedItem.Name, selectedItem.Description, selectedItem.Price, selectedItem.ID, 1);
                    cart.Add(itemToAdd);
                }
                selectedItem.Quantity--; // Decrement quantity in inventory
                Console.WriteLine("Item added to the cart successfully!");
            }
            else if (selectedItem != null && selectedItem.Quantity == 0)
            {
                Console.WriteLine("Item out of stock.");
            }
            else
            {
                Console.WriteLine($"Item with ID {itemId} not found.");
            }
        }

        private static void RemoveItemFromCart(InventoryManager inventoryManager, List<Items> cart)
        {
            if (cart.Count == 0)
            {
                Console.WriteLine("No items in the cart.");
                return;
            }

            Console.WriteLine("Items in the cart:");
            foreach (var item in cart)
            {
                Console.WriteLine($"ID: {item.ID}, Name: {item.Name}, Price: {item.Price}, Quantity: {item.Quantity}");
            }

            Console.Write("Enter the ID of the item to remove from the cart: ");
            int itemId;
            while (!int.TryParse(Console.ReadLine(), out itemId))
            {
                Console.WriteLine("Invalid ID. Please enter a valid integer number.");
                Console.Write("Enter the ID of the item: ");
            }

            // Find the item in the cart
            Items? itemToRemove = cart.Find(item => item.ID == itemId);
            if (itemToRemove != null)
            {
                itemToRemove.Quantity--;
                if (itemToRemove.Quantity == 0)
                {
                    cart.Remove(itemToRemove); // Remove from cart if quantity is zero
                }

                // Add the removed quantity back to inventory
                Items? itemInInventory = inventoryManager.GetInventory().Find(item => item.ID == itemId);
                if (itemInInventory != null)
                {
                    itemInInventory.Quantity++;
                }
                else
                {
                    // If for some reason the item is not found in the inventory (shouldn't happen), add it back
                    inventoryManager.AddToInventory(new Items(itemToRemove.Name, itemToRemove.Description, itemToRemove.Price, itemToRemove.ID, 1));
                }

                Console.WriteLine("Item removed from the cart successfully!");
            }
            else
            {
                Console.WriteLine($"Item with ID {itemId} not found in the cart.");
            }
        }
        private static void Checkout(List<Items> cart)
        {
            if (cart.Count == 0)
            {
                Console.WriteLine("No items in the cart. Cannot proceed to checkout.");
                return;
            }

            decimal subtotal = 0;
            foreach (var item in cart)
            {
                subtotal += item.Price * item.Quantity;
            }

            decimal tax = subtotal * 0.07m; // Calculating tax (7%)
            decimal total = subtotal + tax;

            Console.WriteLine("Checkout Summary:");
            Console.WriteLine("Itemized Receipt:");
            foreach (var item in cart)
            {
                Console.WriteLine($"Name: {item.Name}, Price: {item.Price}, Quantity: {item.Quantity}");
            }

            Console.WriteLine($"Subtotal: {subtotal:C}");
            Console.WriteLine($"Tax (7%): {tax:C}");
            Console.WriteLine($"Total: {total:C}");

            // Clearing the cart after checkout
            cart.Clear();
            Console.WriteLine("Thank you for shopping with us!");

        }
    }
}
