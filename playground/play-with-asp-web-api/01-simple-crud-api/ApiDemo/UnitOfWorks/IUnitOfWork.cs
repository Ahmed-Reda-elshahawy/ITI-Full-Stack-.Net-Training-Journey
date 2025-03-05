using ApiDemo.Repositories;
using AutoMapper;

namespace ApiDemo.UnitOfWorks;

public interface IUnitOfWork
{
    public IRepository<TEntity> Repository<TEntity>() where TEntity : class;
    public Task SaveChangesAsync();
}
