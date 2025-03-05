using ApiDemo.Dtos.Shared;

namespace ApiDemo.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    public Task<List<TEntity>> GetAllAsync(PaginationDto pagination);

    public Task<TEntity?> GetByIdAsync(int id);

    public Task<int> CountAsync();

    public Task<TEntity> CreateAsync(TEntity entity);

    public Task<TEntity> UpdateASync(TEntity entity);

    public Task DeleteAsync(TEntity entity);

    public Task SaveChangesAsync();
}
