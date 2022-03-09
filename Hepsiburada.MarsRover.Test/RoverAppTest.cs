using AutoMapper;
using Hepsiburada.MarsRover.Application;
using Hepsiburada.MarsRover.Application.Dtos;
using Hepsiburada.MarsRover.Domain.RoverManagement;
using Hepsiburada.MarsRover.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Hepsiburada.MarsRover.Test
{
    public class RoverAppTest
    {
        private readonly IRoverApp roverApp;

        public RoverAppTest()
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<RoverContext>();
            dbContextOptionsBuilder.UseInMemoryDatabase("RoverDb");

            var roverContext = new RoverContext(dbContextOptionsBuilder.Options);
            IRoverRepository roverRepository = new RoverRepository(roverContext);

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperRoverProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            IRoverApp roverApp = new RoverApp(roverRepository, mapper);

            this.roverApp = roverApp;
        }

        [
        Theory,
        InlineData(1, 2, 'N', "LMLMLMLMM", "1,3,N"),
        InlineData(3, 3, 'E', "MMRMMRMRRM", "5,1,E")
        ]
        public async void RoverApp_Command_And_Move_Test(int roverLocationX, int roverLocationY, char roverOrientation, string command, string expected)
        {
            var plateau = new PlateauDto() { CoordinateX = 5, CoordinateY = 5 };

            var roverDto = new RoverDto()
            {
                LocationX = roverLocationX,
                LocationY = roverLocationY,
                Plateau = plateau,
                RoverHead = roverOrientation
            };

            var rover = await roverApp.AddRover(roverDto);

            rover = await roverApp.ControlRover(rover.Id, command);

            Assert.Equal(expected, rover.Coordinate);
        }
    }
}
