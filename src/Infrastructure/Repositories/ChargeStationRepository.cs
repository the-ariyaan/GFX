using Domain.Contracts.Repository;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories;

public class ChargeStationRepository : BaseRepository<ChargeStation, GreenFluxDbContext>,
    IChargeStationRepository
{
    public ChargeStationRepository(GreenFluxDbContext dbContext) : base(dbContext)
    {
    }
}