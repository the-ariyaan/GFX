using System.Xml.Schema;
using Domain.Contracts.Repository;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ConnectorRepository : BaseRepository<Connector, GreenFluxDbContext>, IConnectorRepository
{
    public ConnectorRepository(GreenFluxDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Connector?> GetConnectorIncludingGroup(long id)
    {
        return await DbContext.Connectors
            .Include(x => x.ChargeStation)
            .ThenInclude(x => x.Group)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}