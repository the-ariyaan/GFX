using Api;
using AutoMapper;
using Domain.Contracts.Repository;
using Domain.Contracts.Services;
using Domain.Services;
using Infrastructure.EntityFramework;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add repository dependencies
services.AddDbContext<GreenFluxDbContext>();
services.AddScoped<IGroupRepository, GroupRepository>();
services.AddScoped<IConnectorRepository, ConnectorRepository>();
services.AddScoped<IChargeStationRepository, ChargeStationRepository>();
services.AddScoped<IGroupService, GroupService>();
services.AddScoped<IChargeStationService, ChargeStationService>();
services.AddScoped<IConnectorService, ConnectorService>();

// Add services to the container.
builder.Services.AddControllers();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure AutoMapper
var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
builder.Services.AddSingleton(config.CreateMapper());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<GreenFluxDbContext>();
    dataContext.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();