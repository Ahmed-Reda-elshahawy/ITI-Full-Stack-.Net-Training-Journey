using ApiDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiDemo.Database;

public class ITIDbContext:DbContext
{
    public ITIDbContext(DbContextOptions opt) : base(opt) { }

    public DbSet<Student> Students{ get; set; }
    public DbSet<Department> Departments { get; set; }
}
