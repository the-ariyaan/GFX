using Domain.DTOs;
using Domain.Entities;

namespace Domain.Contracts.Services;

public interface IChargeStationService
{
    Task<IEnumerable<ChargeStationDTO>> GetAllAsync();
    Task<ChargeStationDTO> CreateAsync(ChargeStationDTO chargeStation);
    Task<ChargeStationDTO> UpdateAsync(ChargeStationDTO chargeStation);
    Task RemoveAsync(long id);
}