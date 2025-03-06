using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication2.Database
{
    public class MVCD02DbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Data Source=M7MOUDGADALLAH\\SQLEXPRESS;Initial Catalog=MVCD02;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
