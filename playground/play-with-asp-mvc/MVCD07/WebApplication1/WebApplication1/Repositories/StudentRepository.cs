using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Repositories
{
    public class StudentRepository(MVCD07DbContext _dbContext) : Repository<Student>(_dbContext)
    {
        public override List<Student> GetAll()
        {
            return _dbContext.Students.Include(s => s.Department).ToList();
        }
    }
}
