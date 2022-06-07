using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.EntityFramework;

public class GreenFluxDbContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public GreenFluxDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
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
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
    }
}