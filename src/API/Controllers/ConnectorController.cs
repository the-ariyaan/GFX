using Domain.Contracts.Repository;
using Domain.Contracts.Services;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
public class ConnectorController : ControllerBase
{
    private readonly IConnectorService _service;

    public ConnectorController(IConnectorService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<Connector> Create([FromBody] Connector chargeStation)
    {
        return await _service.CreateAsync(chargeStation);
    }

    [HttpPatch]
    public async Task<Connector> Update([FromBody] Connector chargeStation)
    {
        return await _service.UpdateAsync(chargeStation);
    }

    [HttpDelete("{id:long}")]
    public async Task Remove(long id)
    {
        await _service.RemoveAsync(id);
    }
}