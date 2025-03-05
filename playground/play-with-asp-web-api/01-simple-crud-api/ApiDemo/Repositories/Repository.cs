using ApiDemo.Database;
using ApiDemo.Dtos.Shared;
using Microsoft.EntityFrameworkCore;

namespace ApiDemo.Repositories;

public class Repository<TEntity>(ITIDbContext _dbContext) : IRepository<TEntity> where TEntity : class
{
    public async Task<int> CountAsync()
    {
        return await _dbContext.Set<TEntity>().CountAsync();
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await _dbContext.AddAsync(entity);
        return entity;
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _dbContext.Remove(entity);
    }

    public async Task<List<TEntity>> GetAllAsync(PaginationDto pagination)
    {
        return  await _dbContext.Set<TEntity>()
                .Skip(pagination.Skip)
                .Take(pagination.Take)
                .ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<TEntity> UpdateASync(TEntity entity)
    {
        _dbContext.Update(entity);
        return entity;
    }
}
