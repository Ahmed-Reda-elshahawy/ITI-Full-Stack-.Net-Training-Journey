using ApiDemo.Database;
using ApiDemo.Repositories;

namespace ApiDemo.UnitOfWorks;

public class UnitOfWork(ITIDbContext _dbContext) : IUnitOfWork
{
    Dictionary<Type, object> _repoContainer; // (EntityType, RepoInstance)

    public IRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var entityType = typeof(TEntity);

        if (_repoContainer == null)
        {
            _repoContainer = new();
        }

        if (!_repoContainer.ContainsKey(entityType))
        {
            var repoType = typeof(Repository<>).MakeGenericType(entityType);

            var repoInstance = Activator.CreateInstance(repoType, _dbContext);

            _repoContainer[entityType] = repoInstance;
        }

        return (IRepository<TEntity>)_repoContainer[entityType];
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
