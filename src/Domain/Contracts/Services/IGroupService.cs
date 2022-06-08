using Domain.DTOs;
using Domain.Entities;

namespace Domain.Contracts.Services;

public interface IGroupService
{
    Task<IEnumerable<GroupDTO>> GetAllAsync();
    Task<GroupDTO> CreateAsync(GroupDTO group);
    Task<GroupDTO> UpdateAsync(GroupDTO group);
    Task RemoveAsync(long id);
}