using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class StudentService : IStudentService
    {
        MVCD03DbContext _dbContext;
        public StudentService(MVCD03DbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Student Create(Student dto)
        {
            _dbContext.Students.Add(dto);
            _dbContext.SaveChanges();
            return _dbContext.Students.SingleOrDefault(s => s.Email == dto.Email);
        }

        public Student Delete(int id)
        {
            var std = _dbContext.Students.FirstOrDefault(s => s.Id == id);

            if (std == null)
            {
                return null;
            }

            _dbContext.Students.Remove(std);
            _dbContext.SaveChanges();

            return std;
        }

        public List<Student> GetAll()
        {
            return _dbContext.Students.Include(s => s.Department).ToList();
        }

        public Student? GetById(int id)
        {
            return _dbContext.Students.SingleOrDefault(s => s.Id == id);
        }

        public Student Update(Student dto)
        {
            _dbContext.Students.Update(dto);
            _dbContext.SaveChanges();
            return dto;
        }
    }
}
