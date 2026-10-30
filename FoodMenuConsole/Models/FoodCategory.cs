namespace FoodMenuConsole.Models
{
    public static class FoodCategory
    {
        public const string Breakfast = "Breakfast";
        public const string Lunch = "Lunch";
        public const string Snacks = "Snacks";
        public const string Dinner = "Dinner";
        public const string Drinks = "Drinks";

        public static string[] GetAll()
        {
            return new string[]
            {
                Breakfast,
                Lunch,
                Snacks,
                Dinner,
                Drinks
            };
        }
    }
}
