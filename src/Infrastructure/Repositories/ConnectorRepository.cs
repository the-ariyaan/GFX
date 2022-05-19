using System.Linq.Expressions;
using Domain.Contracts.Repository;
using Domain.Entities;
using Domain.Utils;
using Infrastructure.EntityFramework;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories;

public class ConnectorRepository : EntityRepositoryBase<Connector, GreenFluxDbContext>, IConnectorRepository
{
    public ConnectorRepository(GreenFluxDbContext dbContext) : base(dbContext)
    {
    }

    public Task<EntityQueryable<Connector>> QueryWithCountNoTracking(Expression<Func<Connector, bool>> predicate = null)
    {
        throw new NotImplementedException();
    }
}