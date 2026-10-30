using System.ComponentModel.DataAnnotations;

namespace FoodMenuConsole.Models
{
    public class FoodItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(typeof(decimal), "0.01", "999999999.99")]
        public decimal Price { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}
