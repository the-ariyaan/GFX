using Domain.Entities;

namespace Domain.Contracts.Repository;

public interface IEntityRepository<TEntity, TKey> : IRepositoryBase<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
    Task<TEntity> GetAsync(TKey id);
    Task<int> SaveAsync();
}

public interface IEntityRepository<TEntity> : IEntityRepository<TEntity, long>
    where TEntity : class, IEntity
{
}