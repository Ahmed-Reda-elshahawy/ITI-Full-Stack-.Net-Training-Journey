namespace WebApplication1.Repository;

public interface IRepository<T> where T : class
{
    List<T> GetAll();
    T GetById(object id);
    void Create(T entity);
    void Update(T entity);
    T DeleteById(object id);
    void Delete(T entity);
    void Save();
}
