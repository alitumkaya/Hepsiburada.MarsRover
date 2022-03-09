using Hepsiburada.MarsRover.Domain.RoverManagement;
using System;
using Xunit;

namespace Hepsiburada.MarsRover.Test
{
    public class RoverTest
    {
        public RoverTest()
        {

        }
        [Fact]
        public void Rover_Command_And_Move_Test()
        {
            var plateau = new Plateau(new CoordinateLine(30), new CoordinateLine(40));
            var rover = new Rover(Guid.NewGuid(), plateau, new CoordinateLine(5), new CoordinateLine(10), RoverHead.BuildWith('N'));

            rover.TurnLeft();
            rover.Move();
            rover.Move();
            rover.Move();
            rover.TurnRight();
            rover.TurnRight();

            var coordinateResult = rover.GetCoordinate();

            Assert.Equal("2,10,E", coordinateResult);
        }
    }
}