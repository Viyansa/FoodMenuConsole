
using FoodMenuConsole.Data;
using FoodMenuConsole.Menus;
using FoodMenuConsole.Models;
using FoodMenuConsole.Repositories;
using FoodMenuConsole.Services;
using Microsoft.EntityFrameworkCore;

namespace FoodMenuConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Food Menu Console Application";

            string connectionString =
                "Server=(localdb)\\mssqllocaldb;Database=FoodMenuDb;Trusted_Connection=True;TrustServerCertificate=True;";

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseSqlServer(connectionString);

            try
            {
                using var context = new AppDbContext(optionsBuilder.Options);

                // Connect to database and apply migrations
                context.Database.Migrate();

                Console.WriteLine("Database Connected Successfully!");

                // Seed initial sample data
                if (!context.FoodItems.Any())
                {
                    context.FoodItems.AddRange(
                        new FoodItem
                        {
                            Name = "Pancakes with Syrup",
                            Category = FoodCategory.Breakfast,
                            Description = "Fluffy pancakes served with pure maple syrup",
                            Price = 6.99m,
                            IsAvailable = true
                        },
                        new FoodItem
                        {
                            Name = "Grilled Chicken Salad",
                            Category = FoodCategory.Lunch,
                            Description = "Fresh garden salad topped with grilled chicken breast",
                            Price = 10.50m,
                            IsAvailable = true
                        },
                        new FoodItem
                        {
                            Name = "Crispy French Fries",
                            Category = FoodCategory.Snacks,
                            Description = "Golden salted crispy potato fries",
                            Price = 3.50m,
                            IsAvailable = true
                        },
                        new FoodItem
                        {
                            Name = "Ribeye Steak Dinner",
                            Category = FoodCategory.Dinner,
                            Description = "Prime ribeye steak with mashed potatoes and asparagus",
                            Price = 24.99m,
                            IsAvailable = false
                        },
                        new FoodItem
                        {
                            Name = "Iced Caramel Macchiato",
                            Category = FoodCategory.Drinks,
                            Description = "Chilled espresso with milk and vanilla syrup topped with caramel",
                            Price = 4.75m,
                            IsAvailable = true
                        }
                    );

                    context.SaveChanges();

                    Console.WriteLine("Sample Food Items Added Successfully!");
                }

                // Repository
                IFoodRepository repository = new FoodRepository(context);

                // Service
                var service = new FoodService(repository);

                // Menu
                var menu = new FoodMenu(service);

                // Run application
                menu.Run();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("\nDatabase Connection Error!");
                Console.WriteLine(ex.Message);

                Console.ResetColor();

                Console.WriteLine("\nPlease check SQL Server LocalDB.");
                Console.WriteLine("Press Enter to exit...");

                Console.ReadLine();
            }
        }
    }
}
