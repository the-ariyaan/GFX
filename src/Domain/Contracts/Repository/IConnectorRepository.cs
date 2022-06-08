using Domain.Entities;

namespace Domain.Contracts.Repository;

public interface IConnectorRepository : IBaseRepository<Connector>
{
    Task<Connector?> GetConnectorIncludingGroup(long id);
}