using Domain.Entities;

namespace Domain.Contracts.Services;

public interface IConnectorService
{
    Task<IEnumerable<Connector>> GetAllAsync();
    Task<Connector> CreateAsync(Connector group);
    Task<Connector> UpdateAsync(Connector connector);
    Task RemoveAsync(long id);
}