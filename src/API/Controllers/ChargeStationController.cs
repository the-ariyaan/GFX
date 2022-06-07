using Domain.Contracts.Repository;
using Domain.Contracts.Services;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
public class ChargeStationController : ControllerBase
{
    private readonly IChargeStationService _service;

    public ChargeStationController(IChargeStationService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ChargeStation> Create([FromBody] ChargeStation? chargeStation)
    {
        return await _service.CreateAsync(chargeStation);
    }

    [HttpPatch]
    public async Task<ChargeStation?> Update([FromBody] ChargeStation? chargeStation)
    {
        return await _service.UpdateAsync(chargeStation);
    }

    [HttpDelete("{id:long}")]
    public async Task Remove(long id)
    {
        await _service.RemoveAsync(id);
    }
}