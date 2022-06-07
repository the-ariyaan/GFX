using Domain.Contracts.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Base;

public class EntityRepositoryBase<TEntity, TDbContext> : IBaseRepository<TEntity>
    where TEntity : class
    where TDbContext : DbContext
{
    protected TDbContext DbContext { get; }

    protected EntityRepositoryBase(TDbContext dbContext)
    {
        DbContext = dbContext;
    }

    /// <summary>
    /// DBSet of Entity
    /// </summary>
    public DbSet<TEntity> DbSet => DbContext.Set<TEntity>();

    /// <summary>
    /// Looks for an entity by its Id
    /// </summary>
    /// <param name="id">Id of the entity</param>
    /// <returns><see cref="TEntity"/> object</returns>
    public virtual async Task<TEntity?> GetAsync(long id)
    {
        return await DbSet.FindAsync(id);
    }

    /// <summary>
    /// Inserts an entity to database
    /// </summary>
    /// <param name="entity"><see cref="TEntity"/> object to be inserted</param>
    /// <returns></returns>
    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        DbSet.Add(entity);
        await SaveAsync();
        return entity;
    }

    /// <summary>
    /// Removes an entity from database
    /// </summary>
    /// <param name="entity"><see cref="TEntity"/> object to be removed</param>
    public async Task RemoveAsync(TEntity entity)
    {
        DbSet.Remove(entity);
        await SaveAsync();
    }

    /// <summary>
    /// Updates an entity
    /// </summary>
    /// <param name="entity"><see cref="TEntity"/> object to be updated</param>
    /// <returns></returns>
    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        DbSet.Update(entity);
        await SaveAsync();
        return entity;
    }

    /// <summary>
    /// Saves changes
    /// </summary>
    /// <returns>Quantity of changes(rows affected)</returns>
    public async Task<int> SaveAsync()
    {
        return await DbContext.SaveChangesAsync();
    }
}