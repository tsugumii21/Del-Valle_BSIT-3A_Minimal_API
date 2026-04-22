using Microsoft.EntityFrameworkCore;
using ProductAPIDemo.Models;

namespace ProductAPIDemo.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
    }
}
