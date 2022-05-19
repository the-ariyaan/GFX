using Domain.Contracts.Repository;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
public class GroupController : ControllerBase
{
    private readonly ILogger<GroupController> _logger;
    private readonly IGroupRepository _groupRepository;

    public GroupController(ILogger<GroupController> logger, IGroupRepository groupRepository)
    {
        _logger = logger;
        _groupRepository = groupRepository;
    }

    [HttpGet("{id:long}")]
    public async Task<Group> Get(long id)
    {
        return await _groupRepository.GetAsync(id);
    }
}