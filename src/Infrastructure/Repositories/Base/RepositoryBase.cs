using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Infrastructure.Repositories.Base;

public abstract class RepositoryBase<TEntity, TKey, TDbContext> : RepositoryBase<TDbContext>,
    IRepositoryBase<TEntity, TKey>, IDisposable
    where TEntity : class, IEntity<TKey>
    where TDbContext : DbContext
{
    protected DbSet<TEntity> DbSet => DbContext.Set<TEntity>();
    protected virtual IQueryable<TEntity> Query => _ignoreQueryFilters ? DbSet.IgnoreQueryFilters() : DbSet;
    private bool _ignoreQueryFilters;

    public RepositoryBase(TDbContext dbContext) : base(dbContext)
    {
    }


    public void Dispose()
    {
        _ignoreQueryFilters = false;
    }
}

public class RepositoryBase<TDbContext> : IRepository, IScopedDependency where TDbContext : DbContext
{
    protected TDbContext DbContext { get; }

    public RepositoryBase(TDbContext dbContext)
    {
        DbContext = dbContext;
    }
}