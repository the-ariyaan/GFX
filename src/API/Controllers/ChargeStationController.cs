using Domain.Contracts.Repository;
using Domain.Contracts.Services;
using Domain.DTOs;
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

    [HttpGet]
    public async Task<IEnumerable<ChargeStationDTO>> GetAll()
    {
        return await _service.GetAllAsync();
    }

    [HttpPost]
    public async Task<ChargeStationDTO> Create([FromBody] ChargeStationDTO chargeStation)
    {
        return await _service.CreateAsync(chargeStation);
    }

    [HttpPatch]
    public async Task<ChargeStationDTO> Update([FromBody] ChargeStationDTO chargeStation)
    {
        return await _service.UpdateAsync(chargeStation);
    }

    [HttpDelete("{id:long}")]
    public async Task Remove(long id)
    {
        await _service.RemoveAsync(id);
    }
}