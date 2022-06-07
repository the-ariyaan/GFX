using Domain.Entities;

namespace Domain.Contracts.Repository;

public interface IGroupRepository : IBaseRepository<Group>
{
    Group GetByStationIdAsync(long connectorChargeStationId);
    Task<int> GetChargeStationsCountAsync(long chargeStationGroupId);
}