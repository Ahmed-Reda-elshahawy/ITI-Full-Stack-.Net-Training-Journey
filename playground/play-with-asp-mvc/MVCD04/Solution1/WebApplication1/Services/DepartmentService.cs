using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class DepartmentService : IDepartmentService
    {
        MVCD03DbContext _dbContext;
        public DepartmentService(MVCD03DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CheckNameExistance(string name, int? id)
        {
            return (id == null
                ? _dbContext.Departments.Any(d => d.Name == name)
                : _dbContext.Departments.Any(d => d.Name == name && d.Id != id));
        }

        public Department Create(Department dto)
        {
            _dbContext.Departments.Add(dto);
            _dbContext.SaveChanges();
            return _dbContext.Departments.SingleOrDefault(d => d.Name == dto.Name);
        }

        public Department Delete(int id)
        {
            var dept = _dbContext.Departments.FirstOrDefault(s => s.Id == id);

            if (dept == null)
            {
                return null;
            }

            _dbContext.Departments.Remove(dept);
            _dbContext.SaveChanges();

            return dept;
        }

        public List<Department> GetAll()
        {
            return _dbContext.Departments.ToList();
        }

        public Department? GetById(int id)
        {
            return _dbContext.Departments.Include(d => d.Students).SingleOrDefault(d => d.Id == id);
        }

        public Department Update(Department dto)
        {
            _dbContext.Departments.Update(dto);
            _dbContext.SaveChanges();
            return dto;
        }
    }
}
