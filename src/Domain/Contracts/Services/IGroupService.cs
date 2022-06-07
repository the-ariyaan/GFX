using Domain.Entities;

namespace Domain.Contracts.Services;

public interface IGroupService
{
    Task<Group> CreateAsync(Group group);
    Task<Group> UpdateAsync(Group group);
    Task RemoveAsync(long id);
}