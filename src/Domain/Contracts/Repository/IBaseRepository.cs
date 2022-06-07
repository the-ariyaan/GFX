using Domain.Entities;

namespace Domain.Contracts.Repository;

public interface IBaseRepository<TEntity>
    where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
    Task<TEntity?> GetAsync(long id);
    Task<int> SaveAsync();
}