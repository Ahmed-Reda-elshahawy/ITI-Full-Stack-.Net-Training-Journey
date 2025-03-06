using WebApplication1.Models;

namespace WebApplication1.Services;

public interface IDepartmentService : ICrudService<Department>
{
    public bool CheckNameExistance(string name, int? id);
}
