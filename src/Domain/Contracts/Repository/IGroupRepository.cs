using Domain.Entities;

namespace Domain.Contracts.Repository;

public interface IGroupRepository : IBaseRepository<Group>
{
    Task<Group>  GetByStationIdAsync(long connectorChargeStationId);
    Task<int> GetChargeStationsCountAsync(long chargeStationGroupId);
    Task<int> GetChargeStationsCurrentAsync(long chargeStationGroupId);
}