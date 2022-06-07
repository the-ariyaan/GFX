using Domain.Contracts.Repository;
using Domain.Contracts.Services;
using Domain.Entities;

namespace Domain.Services;

public class ChargeStationService : IChargeStationService
{
    private readonly IChargeStationRepository _repository;
    private readonly IGroupRepository _groupRepository;

    public ChargeStationService(IChargeStationRepository repository, IGroupRepository groupRepository)
    {
        _repository = repository;
        _groupRepository = groupRepository;
    }

    public async Task RemoveAsync(long id)
    {
        var data = await _repository.GetAsync(id);
        if (data == null)
            throw new ArgumentException($"Charge Station with Id{id} not found.");

        await _repository.RemoveAsync(data);
    }

    public async Task<ChargeStation?> UpdateAsync(ChargeStation? chargeStation)
    {
        var data = await _repository.GetAsync(chargeStation.Id);
        if (data == null)
            throw new ArgumentException($"Charge Station with Id{chargeStation.Id} not found.");
        if (chargeStation.GroupId == 0)
            throw new ArgumentException("The Charge Station cannot exist in the domain without Group");

        return await _repository.UpdateAsync(chargeStation);
    }

    public async Task<ChargeStation> CreateAsync(ChargeStation? chargeStation)
    {
        if (chargeStation.GroupId == 0)
            throw new ArgumentException("The Charge Station cannot exist in the domain without Group");
        var groupChargeStationCount = await _groupRepository.GetChargeStationsCountAsync(chargeStation.GroupId);


        if (groupChargeStationCount >= 5)
            throw new Exception("Maximum charge stations for a group is 5, so you can not add more charge stations.");

        return await _repository.CreateAsync(chargeStation);
    }
}