using AutoMapper;
using Domain.Contracts.Repository;
using Domain.Contracts.Services;
using Domain.DTOs;
using Domain.Entities;

namespace Domain.Services;

public class ConnectorService : IConnectorService
{
    private readonly IConnectorRepository _repository;
    private readonly IGroupRepository _groupRepository;
    private readonly IMapper _mapper;

    public ConnectorService(IConnectorRepository repository, IGroupRepository groupRepository, IMapper mapper)
    {
        _repository = repository;
        _groupRepository = groupRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ConnectorDTO>> GetAllAsync()
    {
        var result = await _repository.GetAllAsync();
        return _mapper.Map<List<ConnectorDTO>>(result);
    }

    public async Task<ConnectorDTO> CreateAsync(ConnectorDTO connector)
    {
        if (connector.ChargeStationId == 0)
            throw new ArgumentException("A Connector cannot exist in the domain without a Charge Station.");

        var group = await _groupRepository.GetByStationIdAsync(connector.ChargeStationId);
        var groupChargeStationCount = await _groupRepository.GetChargeStationsCountAsync(group.Id);
        if (groupChargeStationCount >= 5)
            throw new Exception("Maximum charge stations for a group is 5, so you can not add more charge stations.");

        var groupChargeStationCurrent = await _groupRepository.GetChargeStationsCurrentAsync(group.Id);
        if (group.Capacity < groupChargeStationCurrent + connector.MaxCurrent)
            throw new ArgumentException("The capacity of the Group is not enough to add new connector");

        var result = await _repository.CreateAsync(_mapper.Map<Connector>(connector));
        return _mapper.Map<ConnectorDTO>(result);
    }

    public async Task<ConnectorDTO> UpdateAsync(ConnectorDTO connector)
    {
        var connectorFromDb = await _repository.GetConnectorIncludingGroup(connector.Id);
        if (connectorFromDb == null)
            throw new ArgumentException($"Connector with Id{connector.Id} not found.");
        if (connector.ChargeStationId == 0)
            throw new ArgumentException("A Connector cannot exist in the domain without a Charge Station.");

        var group = connectorFromDb.ChargeStation.Group;
        var groupChargeStationCurrent = await _groupRepository.GetChargeStationsCurrentAsync(group.Id);
        if (group.Capacity < groupChargeStationCurrent + connector.MaxCurrent)
            throw new ArgumentException(
                "The capacity of the Group is not enough to update the connectors max current.");

        connectorFromDb.MaxCurrent = connector.MaxCurrent;
        var result = await _repository.UpdateAsync(connectorFromDb);
        return _mapper.Map<ConnectorDTO>(result);
    }

    public async Task RemoveAsync(long id)
    {
        var connector = await _repository.GetAsync(id);
        if (connector == null)
            throw new ArgumentException($"Connector with Id{id} not found.");

        var group = _groupRepository.GetByStationIdAsync(connector.ChargeStationId);
        var groupChargeStationCount = await _groupRepository.GetChargeStationsCountAsync(group.Id);
        if (groupChargeStationCount <= 1)
            throw new ArgumentException("Charge station should have at least one connector.");

        await _repository.RemoveAsync(connector);
    }
}