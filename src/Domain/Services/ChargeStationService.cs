using AutoMapper;
using Domain.Contracts.Repository;
using Domain.Contracts.Services;
using Domain.DTOs;
using Domain.Entities;

namespace Domain.Services;

public class ChargeStationService : IChargeStationService
{
    private readonly IChargeStationRepository _repository;
    private readonly IGroupRepository _groupRepository;
    private readonly IConnectorRepository _connectorRepository;
    private readonly IMapper _mapper;

    public ChargeStationService(IChargeStationRepository repository, IGroupRepository groupRepository,
        IConnectorRepository connectorRepository, IMapper mapper)
    {
        _repository = repository;
        _groupRepository = groupRepository;
        _connectorRepository = connectorRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ChargeStationDTO>> GetAllAsync()
    {
        var result = await _repository.GetAllAsync();
        return _mapper.Map<List<ChargeStationDTO>>(result);
    }

    public async Task<ChargeStationDTO> CreateAsync(ChargeStationDTO chargeStation)
    {
        var group = await _groupRepository.GetAsync(chargeStation.GroupId);
        if (group is null)
            throw new ArgumentException("The Charge Station cannot exist in the domain without Group");

        var groupChargeStationCount = await _groupRepository.GetChargeStationsCountAsync(chargeStation.GroupId);
        if (groupChargeStationCount + chargeStation.Connectors.Count > 5)
            throw new Exception("Maximum charge stations for a group is 5, so you can not add more charge stations.");

        var groupChargeStationCurrent = await _groupRepository.GetChargeStationsCurrentAsync(chargeStation.GroupId);

        if (group.Capacity < groupChargeStationCurrent + chargeStation.Connectors.Sum(c => c.MaxCurrent))
            throw new ArgumentException("The capacity of the Group is not enough to add new connector");

        if (chargeStation.Connectors is null || !chargeStation.Connectors.Any())
            throw new ArgumentException("Charge station should have at least one connector.");

        var result = await _repository.CreateAsync(_mapper.Map<ChargeStation>(chargeStation));
        return _mapper.Map<ChargeStationDTO>(result);
    }

    public async Task<ChargeStationDTO> UpdateAsync(ChargeStationDTO chargeStation)
    {
        var station = await _repository.GetAsync(chargeStation.Id);
        if (station == null)
            throw new ArgumentException($"Charge Station with Id{chargeStation.Id} not found.");
        if (chargeStation.GroupId == 0)
            throw new ArgumentException("The Charge Station cannot exist in the domain without Group");
        if (station.Connectors is null || !station.Connectors.Any())
            throw new ArgumentException("Charge station should have at least one connector.");

        station.Name = chargeStation.Name;
        var result = await _repository.UpdateAsync(station);
        return _mapper.Map<ChargeStationDTO>(result);
    }

    public async Task RemoveAsync(long id)
    {
        var data = await _repository.GetAsync(id);
        if (data == null)
            throw new ArgumentException($"Charge Station with Id{id} not found.");

        await _repository.RemoveAsync(data);
    }
}