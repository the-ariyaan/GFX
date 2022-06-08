using Api;
using API.Controllers;
using AutoMapper;
using Domain.DTOs;
using Domain.Services;
using Infrastructure.EntityFramework;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace API.Tests.Integration;

public class GroupTests
{
    private readonly GreenFluxDbContext _mockDbContext;
    private readonly Mock<GroupService> _mockService;
    private readonly Mock<ChargeStationRepository> _mockChargeStationRepository;
    private readonly Mock<GroupRepository> _mockGroupRepository;
    private readonly Mock<ConnectorRepository> _connectorRepository;
    private readonly IMapper _mockMapper;

    //Setup Mocks
    public GroupTests()
    {
        //AutoMapper configuration
        var mockMapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        _mockMapper = mockMapperConfiguration.CreateMapper();
        var options = new DbContextOptionsBuilder<GreenFluxDbContext>()
            .UseInMemoryDatabase(databaseName: $"db-{Guid.NewGuid()}")
            .Options;

        // Insert seed data into the database using one instance of the context
        _mockDbContext = new GreenFluxDbContext(options, null);
        _mockDbContext.Groups.AddRange(MockProfile.Groups);
        _mockDbContext.ChargeStations.AddRange(MockProfile.ChargeStations);
        _mockDbContext.Connectors.AddRange(MockProfile.Connectors);
        _mockDbContext.SaveChanges();

        //Configure repositories
        _mockChargeStationRepository = new Mock<ChargeStationRepository>(_mockDbContext) {CallBase = true};
        _mockGroupRepository = new Mock<GroupRepository>(_mockDbContext) {CallBase = true};
        _connectorRepository = new Mock<ConnectorRepository>(_mockDbContext) {CallBase = true};

        //configure service
        _mockService = new Mock<GroupService>(_mockGroupRepository.Object, _mockChargeStationRepository.Object,
                _mockMapper)
            {CallBase = true};
    }

    [Fact]
    public async Task GetAll_Will_Return_All_Items_In_Database()
    {
        // Arrange
        var controller = new GroupController(_mockService.Object);
        var expected = _mockDbContext.Groups.Count();

        // Act
        var result = await controller.GetAll();

        // Assert
        var viewResult = Assert.IsAssignableFrom<IEnumerable<GroupDTO>>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<GroupDTO>>(viewResult);
        Assert.Equal(expected, model.Count());
    }

    [Fact]
    public async Task Create_Will_Add_New_Item_To_Database()
    {
        // Arrange
        var controller = new GroupController(_mockService.Object);
        var expected = _mockDbContext.Groups.Count() + 1;
        var newItem = new GroupDTO()
        {
            Name = "Test",
            Capacity = 10
        };
        // Act
        var result = await controller.Create(newItem);

        // Assert
        Assert.IsAssignableFrom<GroupDTO>(result);
        var actual = _mockDbContext.Groups.Count();
        Assert.Equal(expected, actual);
    }


    [Fact]
    public async Task Remove_Should_Delete_One_Item_From_Database()
    {
        // Arrange
        var controller = new GroupController(_mockService.Object);
        var expected = _mockDbContext.Groups.Count() - 1;

        // Act
        await controller.Remove(100);

        // Assert
        var actual = _mockDbContext.Groups.Count();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task Remove_Should_Throw_Exception_When_Id_Is_Invalid()
    {
        // Arrange
        var controller = new GroupController(_mockService.Object);
        var invalidId = -1000L;

        // Act
        var action = async () => await controller.Remove(invalidId);

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(action);
    }
}