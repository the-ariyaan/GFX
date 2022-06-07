using Domain.Contracts.Repository;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GroupRepository : EntityRepositoryBase<Group, GreenFluxDbContext>, IGroupRepository
{
    public GroupRepository(GreenFluxDbContext dbContext) : base(dbContext)
    {
    }

    public Group GetByStationIdAsync(long id)
    {
        var station = DbContext.ChargeStations.Include(x => x.Group).FirstOrDefault(c => c.Id == id);
        if (station == null)
            throw new Exception("Station Id not found.");

        return station.Group;
    }

    public async Task<int> GetChargeStationsCountAsync(long id)
    {
        return await DbContext.ChargeStations.CountAsync(c => c.GroupId == id);
    }
}