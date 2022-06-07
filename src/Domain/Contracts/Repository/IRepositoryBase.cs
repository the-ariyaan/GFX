using Domain.Entities;

namespace Domain.Contracts.Repository;

public interface IRepositoryBase<TEntity, TKey> : IRepository
    where TEntity : class, IEntity<TKey>
{
}

public interface IRepository
{
}