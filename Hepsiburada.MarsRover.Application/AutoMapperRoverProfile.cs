using AutoMapper;
using Hepsiburada.MarsRover.Application.Dtos;
using Hepsiburada.MarsRover.Domain.RoverManagement;

namespace Hepsiburada.MarsRover.Application
{
    public class AutoMapperRoverProfile : Profile
    {
        public AutoMapperRoverProfile()
        {
            CreateMap<Plateau, PlateauDto>()
                .ForMember(f => f.CoordinateX, opt => opt.MapFrom(f => f.CoordinateX.Value))
                .ForMember(f => f.CoordinateY, opt => opt.MapFrom(f => f.CoordinateY.Value));
            CreateMap<Rover, RoverDto>()
                .ForMember(f => f.LocationX, opt => opt.MapFrom(f => f.LocationX.Value))
                .ForMember(f => f.LocationY, opt => opt.MapFrom(f => f.LocationY.Value))
                .ForMember(f => f.RoverHead, opt => opt.MapFrom(f => f.RoverHead.HeadValue))
                .ForMember(f => f.Coordinate, opt => opt.MapFrom(f => f.GetCoordinate()));
        }
    }
}
