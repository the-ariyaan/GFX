using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Infrastructure.Repositories.Base;

public interface IRepositoryBase<TEntity, TKey> : IRepository
    where TEntity : class, IEntity<TKey>
{
}

public interface IRepository
{
}