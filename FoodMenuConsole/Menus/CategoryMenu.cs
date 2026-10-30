using FoodMenuConsole.Helper;
using FoodMenuConsole.Models;

namespace FoodMenuConsole.Menus
{
    public static class CategoryMenu
    {
        public static void ShowCategories()
        {
            Console.WriteLine("\nFood Categories");

            string[] categories = FoodCategory.GetAll();

            for (int i = 0; i < categories.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {categories[i]}");
            }
        }

        public static string ChooseCategory()
        {
            string[] categories = FoodCategory.GetAll();

            while (true)
            {
                ShowCategories();

                int choice = ConsoleHelper.ReadInt("Choose category number: ");

                if (choice >= 1 && choice <= categories.Length)
                    return categories[choice - 1];

                Console.WriteLine("Invalid category choice.");
            }
        }
    }
}
