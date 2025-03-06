namespace WebApplication1.Services;

public interface ICrudService<T>
{
    public List<T> GetAll();

    public T? GetById(int id);

    public T Create(T dto);

    public T Update(T dto);

    public T? Delete(int id);
}
