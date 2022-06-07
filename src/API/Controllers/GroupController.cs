using Domain.Contracts.Repository;
using Domain.Contracts.Services;
using Domain.Entities;
using Domain.Services;
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

    [HttpPost]
    public async Task<Group> Create([FromBody] Group group)
    {
        return await _service.CreateAsync(group);
    }

    [HttpPut]
    public async Task<Group?> Update([FromBody] Group? group)
    {
        return await _service.UpdateAsync(group);
    }

    [HttpDelete("{id:long}")]
    public async Task Remove(long id)
    {
        await _service.RemoveAsync(id);
    }
}