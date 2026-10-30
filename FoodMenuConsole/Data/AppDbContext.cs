using FoodMenuConsole.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodMenuConsole.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<FoodItem> FoodItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FoodItem>()
                .Property(f => f.Price)
                .HasPrecision(11, 2);
        }
    }
}
