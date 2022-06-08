using AutoMapper;
using Domain.Contracts.Repository;
using Domain.Contracts.Services;
using Domain.DTOs;
using Domain.Entities;

namespace Domain.Services;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;
    private readonly IChargeStationRepository _chargeStationRepository;
    private readonly IMapper _mapper;

    public GroupService(IGroupRepository groupRepository, IChargeStationRepository chargeStationRepository,
        IMapper mapper)
    {
        _groupRepository = groupRepository;
        _chargeStationRepository = chargeStationRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GroupDTO>> GetAllAsync()
    {
        var result = await _groupRepository.GetAllAsync();
        return _mapper.Map<List<GroupDTO>>(result);
    }

    public async Task<GroupDTO> CreateAsync(GroupDTO group)
    {
        var groupToAdd = _mapper.Map<Group>(group);
        var result = await _groupRepository.CreateAsync(groupToAdd);
        return _mapper.Map<GroupDTO>(result);
    }

    public async Task<GroupDTO> UpdateAsync(GroupDTO group)
    {
        var groupFromDb = await _groupRepository.GetAsync(group.Id);
        if (groupFromDb == null)
            throw new ArgumentException($"Group with Id{group.Id} not found.");
        groupFromDb.Capacity = group.Capacity;
        groupFromDb.Name = group.Name;
        var result = await _groupRepository.UpdateAsync(groupFromDb);
        return _mapper.Map<GroupDTO>(result);
    }

    public async Task RemoveAsync(long id)
    {
        var group = await _groupRepository.GetAsync(id);
        if (group == null)
            throw new ArgumentException($"Group with Id{id} not found.");
        //Note: If a Group is removed, all Charge Stations in the Group should be removed as well.
        foreach (var station in group.ChargeStations)
            await _chargeStationRepository.RemoveAsync(station);
        await _groupRepository.RemoveAsync(group);
    }
}