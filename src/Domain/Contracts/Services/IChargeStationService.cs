using Domain.Entities;

namespace Domain.Contracts.Services;

public interface IChargeStationService
{
    Task<IEnumerable<ChargeStation>> GetAllAsync();
    Task<ChargeStation> CreateAsync(ChargeStation chargeStation);
    Task<ChargeStation?> UpdateAsync(ChargeStation chargeStation);
    Task RemoveAsync(long id);
}