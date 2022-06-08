using Domain.Contracts.Services;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
public class GroupController : ControllerBase
{
    private readonly IGroupService _service;

    public GroupController(IGroupService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<GroupDTO>> GetAll()
    {
        return await _service.GetAllAsync();
    }

    [HttpPost]
    public async Task<GroupDTO> Create([FromBody] GroupDTO group)
    {
        return await _service.CreateAsync(group);
    }

    [HttpPatch]
    public async Task<GroupDTO> Update([FromBody] GroupDTO group)
    {
        return await _service.UpdateAsync(group);
    }

    [HttpDelete("{id:long}")]
    public async Task Remove(long id)
    {
        await _service.RemoveAsync(id);
    }
}