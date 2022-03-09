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

        [Fact]
        public async void RoverApp_Command_And_Move_Test()
        {
            var plateau = new PlateauDto() { CoordinateX = 30, CoordinateY = 40 };
            var roverDto = new RoverDto()
            {
                LocationX = 5,
                LocationY = 10,
                Plateau = plateau,
                RoverHead = 'N'
            };

            var rover = await roverApp.AddRover(roverDto);

            rover = await roverApp.ControlRover(rover.Id, "LLMRRML");

            Assert.Equal("5,10,W", rover.Coordinate);
        }
    }
}
