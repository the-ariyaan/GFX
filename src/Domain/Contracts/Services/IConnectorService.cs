using Domain.DTOs;
using Domain.Entities;

namespace Domain.Contracts.Services;

public interface IConnectorService
{
    Task<IEnumerable<ConnectorDTO>> GetAllAsync();
    Task<ConnectorDTO> CreateAsync(ConnectorDTO group);
    Task<ConnectorDTO> UpdateAsync(ConnectorDTO connector);
    Task RemoveAsync(long id);
}