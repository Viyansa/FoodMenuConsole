namespace FoodMenuConsole.Helper
{
    public static class ConsoleHelper
    {
        public static string ReadRequired(string message)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                    return input.Trim();

                Console.WriteLine("This field cannot be empty.");
            }
        }

        public static string ReadOptional(string message)
        {
            Console.Write(message);
            return Console.ReadLine()?.Trim() ?? "";
        }

        public static int ReadInt(string message)
        {
            while (true)
            {
                Console.Write(message);

                if (int.TryParse(Console.ReadLine(), out int value))
                    return value;

                Console.WriteLine("Enter a valid whole number.");
            }
        }

        public static decimal ReadDecimal(string message)
        {
            while (true)
            {
                Console.Write(message);

                if (decimal.TryParse(Console.ReadLine(), out decimal value))
                    return value;

                Console.WriteLine("Enter a valid price.");
            }
        }

        public static bool ReadYesNo(string message)
        {
            while (true)
            {
                Console.Write(message);
                string answer = Console.ReadLine()?.Trim().ToLower() ?? "";

                if (answer == "y" || answer == "yes")
                    return true;

                if (answer == "n" || answer == "no")
                    return false;

                Console.WriteLine("Enter y or n.");
            }
        }

        public static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
