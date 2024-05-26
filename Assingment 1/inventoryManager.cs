using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assingment_1
{
    public class InventoryManager
    {
        private List<Items> management = new List<Items>();

        public List<Items> GetInventory()
        {
            return management;
        }

        public void ManageInventory()
        {
            while (true)
            {
                Console.WriteLine("1. Create an Item in the inventory");
                Console.WriteLine("2. List all Items");
                Console.WriteLine("3. Update an Item");
                Console.WriteLine("4. Delete an Item");
                Console.WriteLine("5. back to menu");

                string choice = Console.ReadLine() ?? string.Empty;
                if (int.TryParse(choice, out int intChoice))
                {
                    switch (intChoice)
                    {
                        case 1:
                            AddNewItem();
                            break;

                        case 2:
                            ListAllItems();
                            break;

                        case 3:
                            UpdateItem();
                            break;

                        case 4:
                            DeleteItem();
                            break;

                        case 5:
                            Console.WriteLine("Returning to the main menu.");
                            Menu menu = new Menu(this); // Pass the current InventoryManager instance
                            menu.ShowMainMenu();
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                }
            }
        }

        private void AddNewItem()
        {
            Console.WriteLine("Adding a new item to the inventory:");
            Console.Write("Enter the name: ");
            string name = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter the description: ");
            string description = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter the price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Invalid price. Please enter a valid decimal number.");
                return;
            }
            Console.Write("Enter the ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID. Please enter a valid integer number.");
                return;
            }
            Console.Write("Enter the quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Invalid quantity. Please enter a valid integer number.");
                return;
            }

            Items newItem = new Items(name, description, price, id, quantity);
            management.Add(newItem);
            Console.WriteLine("Item added successfully!");
        }

        private void ListAllItems()
        {
            Console.WriteLine("Current Items:");
            foreach (var item in management)
            {
                Console.WriteLine($"Name: {item.Name}");
                Console.WriteLine($"Description: {item.Description}");
                Console.WriteLine($"Price: {item.Price}");
                Console.WriteLine($"ID: {item.ID}");
                Console.WriteLine($"Quantity: {item.Quantity}");
                Console.WriteLine();
            }
        }

        private void UpdateItem()
        {
            Console.WriteLine("Enter the ID of the item to update:");
            if (!int.TryParse(Console.ReadLine(), out int updateItemId))
            {
                Console.WriteLine("Invalid ID. Please enter a valid integer number.");
                return;
            }

            Items? itemToUpdate = management.Find(item => item.ID == updateItemId);
            if (itemToUpdate != null)
            {
                Console.WriteLine($"Updating item with ID: {itemToUpdate.ID}");
                Console.Write("Enter the new name: ");
                itemToUpdate.Name = Console.ReadLine() ?? string.Empty;
                Console.Write("Enter the new description: ");
                itemToUpdate.Description = Console.ReadLine() ?? string.Empty;
                Console.Write("Enter the new price: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal newPrice))
                {
                    Console.WriteLine("Invalid price. Please enter a valid decimal number.");
                    return;
                }
                itemToUpdate.Price = newPrice;

                Console.Write("Enter the new quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int newQuantity))
                {
                    Console.WriteLine("Invalid quantity. Please enter a valid integer number.");
                    return;
                }
                itemToUpdate.Quantity = newQuantity;
                Console.WriteLine("Item updated successfully!");
            }
            else
            {
                Console.WriteLine($"Item with ID {updateItemId} not found.");
            }
        }

        private void DeleteItem()
        {
            Console.WriteLine("Enter the ID of the item to delete:");
            if (!int.TryParse(Console.ReadLine(), out int deleteItemId))
            {
                Console.WriteLine("Invalid ID. Please enter a valid integer number.");
                return;
            }

            Items? itemToDelete = management.Find(item => item.ID == deleteItemId);
            if (itemToDelete != null)
            {
                management.Remove(itemToDelete);
                Console.WriteLine("Item deleted successfully!");
            }
            else
            {
                Console.WriteLine($"Item with ID {deleteItemId} not found.");
            }
        }

        public void AddToInventory(Items item)
        {
            Items? existingItem = management.Find(i => i.ID == item.ID);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                management.Add(item);
            }
        }
    }



}
