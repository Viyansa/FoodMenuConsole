using FoodMenuConsole.Models;

namespace FoodMenuConsole.Repositories
{
    public interface IFoodRepository
    {
        List<FoodItem> GetAll();

        FoodItem? GetById(int id);

        List<FoodItem> GetByCategory(string category);

        void Add(FoodItem item);

        bool Update(FoodItem item);

        bool Delete(int id);
    }
}
