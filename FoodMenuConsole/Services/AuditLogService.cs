
using FoodMenuConsole.Data;
using FoodMenuConsole.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodMenuConsole.Services
{
    public class AuditLogService
    {
        private readonly AppDbContext _context;

        public AuditLogService(AppDbContext context)
        {
            _context = context;
        }
        
        public void CreateAuditLogTransaction(
            string action,
            int? foodItemId,
            string foodName,
            string details)
        {
            using var transaction =
                _context.Database.BeginTransaction();

            try
            {
                AuditLog log = new AuditLog
                {
                    Action = action,
                    FoodItemId = foodItemId,
                    FoodName = foodName,
                    Details = details,
                    CreatedAt = DateTime.Now
                };

                _context.AuditLogs.Add(log);

                _context.SaveChanges();

                transaction.Commit();

                Console.WriteLine("Audit Log Saved Successfully!");
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                Console.WriteLine("Transaction Failed!");
                Console.WriteLine(ex.Message);

                throw;
            }
        }
    }
}

