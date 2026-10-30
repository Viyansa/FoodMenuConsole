using FoodMenuConsole.Helper;
using FoodMenuConsole.Models;
using FoodMenuConsole.Services;

namespace FoodMenuConsole.Menus
{
    public class FoodMenu
    {
        private readonly FoodService _service;

        public FoodMenu(FoodService service)
        {
            _service = service;
        }

        public void Run()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("        FOOD MENU MANAGEMENT");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Add Food Item");
                Console.WriteLine("2. View All Food Items");
                Console.WriteLine("3. Search Food by ID");
                Console.WriteLine("4. Search Food by Category");
                Console.WriteLine("5. Update Food Item");
                Console.WriteLine("6. Delete Food Item");
                Console.WriteLine("7. View Available Food Items");
                Console.WriteLine("0. Exit");
                Console.WriteLine("========================================");

                int choice = ConsoleHelper.ReadInt("\nEnter your choice: ");

                try
                {
                    switch (choice)
                    {
                        case 1:
                            AddFoodItem();
                            break;
                        case 2:
                            ViewAllFoodItems();
                            break;
                        case 3:
                            SearchById();
                            break;
                        case 4:
                            SearchByCategory();
                            break;
                        case 5:
                            UpdateFoodItem();
                            break;
                        case 6:
                            DeleteFoodItem();
                            break;
                        case 7:
                            ViewAvailableFoodItems();
                            break;
                        case 0:
                            running = false;
                            Console.WriteLine("\nExiting application. Goodbye!");
                            break;
                        default:
                            Console.WriteLine("\nInvalid choice. Please select 0 to 7.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nAn unexpected error occurred: {ex.Message}");
                }

                if (running)
                {
                    ConsoleHelper.Pause();
                }
            }
        }

        private void AddFoodItem()
        {
            Console.WriteLine("\n--- Add Food Item ---");

            string name = ConsoleHelper.ReadRequired("Enter food name: ");
            string category = CategoryMenu.ChooseCategory();
            string description = ConsoleHelper.ReadOptional("Enter description (optional): ");
            decimal price = ConsoleHelper.ReadDecimal("Enter price: ");
            bool isAvailable = ConsoleHelper.ReadYesNo("Is it available? (y/n): ");

            var item = new FoodItem
            {
                Name = name,
                Category = category,
                Description = string.IsNullOrWhiteSpace(description) ? null : description,
                Price = price,
                IsAvailable = isAvailable
            };

            if (_service.Add(item, out string message))
            {
                Console.WriteLine($"\nSuccess: {message} (Assigned ID: {item.Id})");
            }
            else
            {
                Console.WriteLine($"\nFailed: {message}");
            }
        }

        private void ViewAllFoodItems()
        {
            Console.WriteLine("\n--- All Food Items ---");
            var items = _service.GetAll();
            DisplayItemsTable(items);
        }

        private void SearchById()
        {
            Console.WriteLine("\n--- Search Food by ID ---");
            int id = ConsoleHelper.ReadInt("Enter Food ID: ");
            var item = _service.GetById(id);

            if (item == null)
            {
                Console.WriteLine("\nFood item not found.");
                return;
            }

            Console.WriteLine("\nFood Item Details:");
            DisplayItemCard(item);
        }

        private void SearchByCategory()
        {
            Console.WriteLine("\n--- Search Food by Category ---");
            string category = CategoryMenu.ChooseCategory();
            var items = _service.GetByCategory(category);

            Console.WriteLine($"\n--- Items in Category: {category} ---");
            DisplayItemsTable(items);
        }

        private void UpdateFoodItem()
        {
            Console.WriteLine("\n--- Update Food Item ---");
            int id = ConsoleHelper.ReadInt("Enter Food ID to update: ");
            var existing = _service.GetById(id);

            if (existing == null)
            {
                Console.WriteLine("\nFood item not found.");
                return;
            }

            Console.WriteLine("\nCurrent Details:");
            DisplayItemCard(existing);

            Console.WriteLine("\nEnter updated information (leave empty to keep current):");

            Console.Write($"Name [{existing.Name}]: ");
            string? nameInput = Console.ReadLine()?.Trim();
            string name = string.IsNullOrWhiteSpace(nameInput) ? existing.Name : nameInput;

            bool changeCategory = ConsoleHelper.ReadYesNo($"Change category '{existing.Category}'? (y/n): ");
            string category = changeCategory ? CategoryMenu.ChooseCategory() : existing.Category;

            Console.Write($"Description [{(string.IsNullOrWhiteSpace(existing.Description) ? "None" : existing.Description)}]: ");
            string? descInput = Console.ReadLine()?.Trim();
            string? description = string.IsNullOrWhiteSpace(descInput) ? existing.Description : descInput;

            Console.Write($"Price [${existing.Price:F2}]: ");
            string? priceInput = Console.ReadLine()?.Trim();
            decimal price = existing.Price;
            if (!string.IsNullOrWhiteSpace(priceInput))
            {
                if (decimal.TryParse(priceInput, out decimal parsedPrice))
                {
                    price = parsedPrice;
                }
                else
                {
                    Console.WriteLine("Invalid price input. Keeping current price.");
                }
            }

            bool isAvailable = ConsoleHelper.ReadYesNo($"Is it available? (y/n) [{(existing.IsAvailable ? "y" : "n")}]: ");

            var updatedItem = new FoodItem
            {
                Id = existing.Id,
                Name = name,
                Category = category,
                Description = description,
                Price = price,
                IsAvailable = isAvailable
            };

            if (_service.Update(updatedItem, out string message))
            {
                Console.WriteLine($"\nSuccess: {message}");
            }
            else
            {
                Console.WriteLine($"\nFailed: {message}");
            }
        }

        private void DeleteFoodItem()
        {
            Console.WriteLine("\n--- Delete Food Item ---");
            int id = ConsoleHelper.ReadInt("Enter Food ID to delete: ");
            var existing = _service.GetById(id);

            if (existing == null)
            {
                Console.WriteLine("\nFood item not found.");
                return;
            }

            DisplayItemCard(existing);

            bool confirm = ConsoleHelper.ReadYesNo("\nAre you sure you want to delete this food item? (y/n): ");
            if (!confirm)
            {
                Console.WriteLine("Deletion cancelled.");
                return;
            }

            if (_service.Delete(id, out string message))
            {
                Console.WriteLine($"\nSuccess: {message}");
            }
            else
            {
                Console.WriteLine($"\nFailed: {message}");
            }
        }

        private void ViewAvailableFoodItems()
        {
            Console.WriteLine("\n--- Available Food Items ---");
            var items = _service.GetAvailable();
            DisplayItemsTable(items);
        }

        private void DisplayItemsTable(List<FoodItem> items)
        {
            if (items == null || items.Count == 0)
            {
                Console.WriteLine("No food items found.");
                return;
            }

            Console.WriteLine(new string('-', 85));
            Console.WriteLine($"{"ID",-5} | {"Name",-25} | {"Category",-12} | {"Price",-10} | {"Available",-10} | {"Description"}");
            Console.WriteLine(new string('-', 85));

            foreach (var item in items)
            {
                string desc = item.Description ?? "";
                if (desc.Length > 20) desc = desc.Substring(0, 17) + "...";
                Console.WriteLine($"{item.Id,-5} | {item.Name,-25} | {item.Category,-12} | ${item.Price,-9:F2} | {(item.IsAvailable ? "Yes" : "No"),-10} | {desc}");
            }

            Console.WriteLine(new string('-', 85));
            Console.WriteLine($"Total items: {items.Count}");
        }

        private void DisplayItemCard(FoodItem item)
        {
            Console.WriteLine($"ID:          {item.Id}");
            Console.WriteLine($"Name:        {item.Name}");
            Console.WriteLine($"Category:    {item.Category}");
            Console.WriteLine($"Price:       ${item.Price:F2}");
            Console.WriteLine($"Available:   {(item.IsAvailable ? "Yes" : "No")}");
            Console.WriteLine($"Description: {(string.IsNullOrWhiteSpace(item.Description) ? "None" : item.Description)}");
        }
    }
}
