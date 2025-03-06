using WebApplication1.Database;

namespace WebApplication1.Services;

public class EmailExistenceService : IEmailExistenceService
{
    MVCD03DbContext _dbContext;
    public EmailExistenceService(MVCD03DbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public bool CheckEmailExistence(string email, int? id)
    {
        return (id == null
            ? _dbContext.Students.Any(s => s.Email == email)
            : _dbContext.Students.Any(s => s.Email == email && s.Id != id));
    }
}
