using System.Linq.Expressions;
using System.Xml.Schema;
using Domain.Contracts.Repository;
using Domain.Entities;
using Domain.Utils;
using Infrastructure.EntityFramework;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Connector?> GetConnectorIncludingGroupGroup(long id)
    {
        return await DbContext.Connectors.Include(x => x.ChargeStation).ThenInclude(x => x.Group)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}