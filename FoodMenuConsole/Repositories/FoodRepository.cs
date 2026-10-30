using FoodMenuConsole.Data;
using FoodMenuConsole.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodMenuConsole.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private readonly AppDbContext _context;

        public FoodRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<FoodItem> GetAll()
        {
            return _context.FoodItems
                .AsNoTracking()
                .OrderBy(f => f.Id)
                .ToList();
        }

        public FoodItem? GetById(int id)
        {
            return _context.FoodItems
                .AsNoTracking()
                .FirstOrDefault(f => f.Id == id);
        }

        public List<FoodItem> GetByCategory(string category)
        {
            return _context.FoodItems
                .AsNoTracking()
                .Where(f => f.Category == category)
                .OrderBy(f => f.Id)
                .ToList();
        }

        public void Add(FoodItem item)
        {
            _context.FoodItems.Add(item);
            _context.SaveChanges();
        }

        public bool Update(FoodItem item)
        {
            var existing = _context.FoodItems
                .FirstOrDefault(f => f.Id == item.Id);

            if (existing == null)
            {
                return false;
            }

            existing.Name = item.Name;
            existing.Category = item.Category;
            existing.Description = item.Description;
            existing.Price = item.Price;
            existing.IsAvailable = item.IsAvailable;

            _context.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var item = _context.FoodItems.Find(id);

            if (item == null)
            {
                return false;
            }

            _context.FoodItems.Remove(item);
            _context.SaveChanges();

            return true;
        }
    }
}
