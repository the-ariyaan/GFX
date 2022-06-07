using Domain.Contracts.Repository;
using Domain.Contracts.Services;
using Domain.Entities;

namespace Domain.Services;

public class ConnectorService : IConnectorService
{
    private readonly IConnectorRepository _repository;
    private readonly IGroupRepository _groupRepository;

    public ConnectorService(IConnectorRepository repository, IGroupRepository groupRepository)
    {
        _repository = repository;
        _groupRepository = groupRepository;
    }

    public async Task<IEnumerable<Connector>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Connector> CreateAsync(Connector connector)
    {
        if (connector.ChargeStationId == 0)
            throw new ArgumentException("A Connector cannot exist in the domain without a Charge Station.");

        var group = _groupRepository.GetByStationIdAsync(connector.ChargeStationId);

        if (group.Capacity < group.ChargeStations.Sum(c => c.Connectors.Sum(x => x.MaxCurrent)))
            throw new ArgumentException(
                "The capacity of the Group is not enough to add new connector");

        return await _repository.CreateAsync(connector);
    }

    public async Task<Connector> UpdateAsync(Connector connector)
    {
        var data = await _repository.GetConnectorIncludingGroupGroup(connector.Id);
        if (data == null)
            throw new ArgumentException($"Connector with Id{connector.Id} not found.");
        if (connector.ChargeStationId == 0)
            throw new ArgumentException("A Connector cannot exist in the domain without a Charge Station.");

        var group = data.ChargeStation.Group;
        if (group.Capacity < group.ChargeStations.Sum(c => c.Connectors.Sum(x => x.MaxCurrent)))
            throw new ArgumentException(
                "The capacity in Amps of a Group should always be great or equal to the sum of the Max current in Amps of the Connector of all Charge Stations in the Group.");

        return await _repository.UpdateAsync(connector);
    }

    public async Task RemoveAsync(long id)
    {
        var connector = await _repository.GetAsync(id);
        if (connector == null)
            throw new ArgumentException($"Connector with Id{id} not found.");

        await _repository.RemoveAsync(connector);
    }
}