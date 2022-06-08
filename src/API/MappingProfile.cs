using AutoMapper;
using Domain.DTOs;
using Domain.Entities;

namespace Api;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Group, GroupDTO>().ReverseMap();
        CreateMap<Connector, ConnectorDTO>().ReverseMap();
        CreateMap<ChargeStation, ChargeStationDTO>().ReverseMap();
    }
}