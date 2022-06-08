using Domain.Entities;

namespace API.Tests;

public static class MockProfile
{
    public static List<Group> Groups =>
        new()
        {
            new() {Id = 1, Name = "Group 30 Amps", Capacity = 30},
            new() {Id = 2, Name = "Group 20 Amps", Capacity = 20},
            new() {Id = 3, Name = "Group 10 Amps", Capacity = 10},
            new() {Id = 100, Name = "To Be Removed", Capacity = 100},
        };

    public static List<ChargeStation> ChargeStations =>
        new()
        {
            new() {Id = 1, Name = "Station with three 5 Amps connector", GroupId = 1},
            new() {Id = 2, Name = "Station with one 10 Amps connector", GroupId = 1},
            new() {Id = 3, Name = "Station without one connector", GroupId = 2},
            new() {Id = 4, Name = "Station without connector", GroupId = 2},
            new() {Id = 100, Name = "To Be Removed", GroupId = 3},
        };

    public static List<Connector> Connectors =>
        new()
        {
            new() {Id = 1, MaxCurrent = 2, ChargeStationId = 1},
            new() {Id = 2, MaxCurrent = 2, ChargeStationId = 1},
            new() {Id = 3, MaxCurrent = 5, ChargeStationId = 1},
            new() {Id = 4, MaxCurrent = 10, ChargeStationId = 2},
            new() {Id = 5, MaxCurrent = 10, ChargeStationId = 2},
            new() {Id = 6, MaxCurrent = 15, ChargeStationId = 3},
            new() {Id = 100, MaxCurrent = 0, ChargeStationId = 4},
        };
}