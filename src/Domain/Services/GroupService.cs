using Domain.Contracts.Repository;
using Domain.Contracts.Services;
using Domain.Entities;

namespace Domain.Services;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;
    private readonly IChargeStationRepository _chargeStationRepository;

    public GroupService(IGroupRepository groupRepository, IChargeStationRepository chargeStationRepository)
    {
        _groupRepository = groupRepository;
        _chargeStationRepository = chargeStationRepository;
    }

    public async Task RemoveAsync(long id)
    {
        var group = await _groupRepository.GetAsync(id);
        if (group == null)
            throw new ArgumentException($"Group with Id{id} not found.");
        
        foreach (var station in group.ChargeStations)
            await _chargeStationRepository.RemoveAsync(station);
        await _groupRepository.RemoveAsync(group);
    }

    public async Task<Group> UpdateAsync(Group group)
    {
        var groupFromDb = await _groupRepository.GetAsync(group.Id);
        if (groupFromDb == null)
            throw new ArgumentException($"Group with Id{group.Id} not found.");

        return await _groupRepository.UpdateAsync(group);
    }

    public async Task<Group> CreateAsync(Group group)
    {
        if (group.ChargeStations is {Count: > 1})
            throw new ArgumentException("Only one Charge Station can be added/removed to a Group in one call.");

        return await _groupRepository.CreateAsync(group);
    }
}