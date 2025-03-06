using WebApplication1.Database;

namespace WebApplication1.Repository;

public class Repository<T>(MVCD07DbContext _dbContext) : IRepository<T> where T : class
{
    public void Create(T entity)
    {
        _dbContext.Add(entity);
    }

    public void Delete(T entity)
    {
        _dbContext.Remove(entity);
    }

    public T DeleteById(object id)
    {
        T entity = GetById(id);
        if (entity == null)
        {
            return null;
        }
        Delete(entity);
        return entity;
    }

    virtual public List<T> GetAll()
    {
        return _dbContext.Set<T>().ToList();
    }

    public T GetById(object id)
    {
        return _dbContext.Set<T>().Find(id);
    }

    public void Update(T entity)
    {
        _dbContext.Update(entity);
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }
}
