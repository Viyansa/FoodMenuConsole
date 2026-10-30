

namespace FoodMenuConsole.Models
{
    public class AuditLog
    {
        public int Id { get; set; }

        public string Action { get; set; } = string.Empty;

        public int? FoodItemId { get; set; }

        public string FoodName { get; set; } = string.Empty;

        public string Details { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}

