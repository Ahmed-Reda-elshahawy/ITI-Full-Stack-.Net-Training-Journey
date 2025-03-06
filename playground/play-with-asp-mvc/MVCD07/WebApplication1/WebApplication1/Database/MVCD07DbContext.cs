using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Database;
public class MVCD07DbContext : DbContext
{
    public MVCD07DbContext(DbContextOptions options) : base(options) { }

    public DbSet<Department> Departments { get; set; }
    public DbSet<Student> Students { get; set; }
}
