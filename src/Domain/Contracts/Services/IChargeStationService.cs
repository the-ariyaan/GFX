using Domain.Entities;

namespace Domain.Contracts.Services;

public interface IChargeStationService
{
    Task<ChargeStation> CreateAsync(ChargeStation chargeStation);
    Task<ChargeStation> UpdateAsync(ChargeStation chargeStation);
    Task RemoveAsync(long id);
}