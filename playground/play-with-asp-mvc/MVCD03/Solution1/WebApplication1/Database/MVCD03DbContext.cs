using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Database;

public class MVCD03DbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(@"Data Source=M7MOUDGADALLAH\SQLEXPRESS;Initial Catalog=MVCD03;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
    public DbSet<Student> Students { get; set; }
    public DbSet<Department> Departments { get; set; }
}
