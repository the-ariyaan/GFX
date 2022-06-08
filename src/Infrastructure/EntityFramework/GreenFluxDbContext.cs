using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.EntityFramework;

public class GreenFluxDbContext : DbContext
{
    protected readonly IConfiguration _configuration;

    public GreenFluxDbContext(DbContextOptions<GreenFluxDbContext> dbContextOptions, IConfiguration? configuration) :
        base(
            dbContextOptions)
    {
        _configuration = configuration!;
    }


    public virtual DbSet<ChargeStation> ChargeStations { get; set; }
    public virtual DbSet<Connector> Connectors { get; set; }
    public virtual DbSet<Group> Groups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //The schema for the database
        const string schema = "gfx";
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sql server with connection string from app settings
        if (_configuration != null)
            options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
    }
}