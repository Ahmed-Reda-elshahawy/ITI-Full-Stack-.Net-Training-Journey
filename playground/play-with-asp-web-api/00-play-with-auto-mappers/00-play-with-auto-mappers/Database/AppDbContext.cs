using _00_play_with_auto_mappers.Models;
using Microsoft.EntityFrameworkCore;

namespace _00_play_with_auto_mappers.Database
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Category> Categories{ get; set; }
        public DbSet<Product> Products{ get; set; }
    }
}
