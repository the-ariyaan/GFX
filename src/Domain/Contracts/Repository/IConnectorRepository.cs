using Domain.Entities;

namespace Domain.Contracts.Repository;

public interface IConnectorRepository : IEntityRepository<Connector>
{
    Task<Connector?> GetConnectorIncludingGroupGroup(long id);
}