using FoodMenuConsole.Models;
using FoodMenuConsole.Repositories;

namespace FoodMenuConsole.Services
{
    public class FoodService
    {
        private readonly IFoodRepository _repository;

        public FoodService(IFoodRepository repository)
        {
            _repository = repository;
        }

        public List<FoodItem> GetAll()
        {
            return _repository.GetAll();
        }

        public List<FoodItem> GetAvailable()
        {
            return _repository.GetAll()
                .Where(f => f.IsAvailable)
                .ToList();
        }

        public FoodItem? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public List<FoodItem> GetByCategory(string category)
        {
            return _repository.GetByCategory(category);
        }

        public bool Add(FoodItem item, out string message)
        {
            if (!Validate(item, out message))
                return false;

            _repository.Add(item);
            message = "Food item added successfully.";
            return true;
        }

        public bool Update(FoodItem item, out string message)
        {
            if (!Validate(item, out message))
                return false;

            if (_repository.GetById(item.Id) == null)
            {
                message = "Food item not found.";
                return false;
            }

            bool updated = _repository.Update(item);

            message = updated
                ? "Food item updated successfully."
                : "Could not update food item.";

            return updated;
        }

        public bool Delete(int id, out string message)
        {
            bool deleted = _repository.Delete(id);

            message = deleted
                ? "Food item deleted successfully."
                : "Food item not found.";

            return deleted;
        }

        private bool Validate(FoodItem item, out string message)
        {
            item.Name = item.Name.Trim();
            item.Category = item.Category.Trim();

            if (string.IsNullOrWhiteSpace(item.Name))
            {
                message = "Food name cannot be empty.";
                return false;
            }

            if (item.Name.Length > 100)
            {
                message = "Food name cannot exceed 100 characters.";
                return false;
            }

            string? category = FoodCategory.GetAll()
                .FirstOrDefault(c =>
                    c.Equals(item.Category, StringComparison.OrdinalIgnoreCase));

            if (category == null)
            {
                message = "Please select a valid category.";
                return false;
            }

            item.Category = category;

            if (item.Description?.Length > 500)
            {
                message = "Description cannot exceed 500 characters.";
                return false;
            }

            if (item.Price <= 0 || item.Price > 999999999.99m)
            {
                message = "Price must be greater than zero.";
                return false;
            }

            message = "";
            return true;
        }
    }
}
