using Microsoft.EntityFrameworkCore;
using WebApi3.Models;

namespace WebApi3.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<User> Users { get; set; }

    
    }
}
