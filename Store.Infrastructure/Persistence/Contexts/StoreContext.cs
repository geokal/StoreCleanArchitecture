using Microsoft.EntityFrameworkCore;
using Store.ApplicationCore.Entities;

namespace Store.Infrastructure.Persistence
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}