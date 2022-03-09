using Hepsiburada.MarsRover.Domain.Common;
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
        [Theory]
        [InlineData(5, 10, 'E', "6,9,S")]
        [InlineData(10, 15, 'N', "11,16,E")]
        public void Should_Rover_Move_Calculated_As_Axpected(int locationX, int locationY, char roverOrientation, string expected)
        {
            var plateau = new Plateau(new CoordinateLine(30), new CoordinateLine(40));
            var rover = new Rover(Guid.NewGuid(),
                                  plateau,
                                  new CoordinateLine(locationX),
                                  new CoordinateLine(locationY),
                                  RoverHead.BuildWith(roverOrientation));

            rover.Move();
            rover.TurnLeft();
            rover.TurnLeft();
            rover.TurnLeft();
            rover.Move();

            var coordinateResult = rover.GetCoordinate();

            Assert.Equal(expected, coordinateResult);
        }

        [Theory]
        [InlineData(5, 10, 'E', "5,10,S")]
        [InlineData(5, 10, 'N', "5,10,E")]
        public void Should_Rover_TurnLeft_Calculated_As_Axpected(int locationX, int locationY, char roverOrientation, string expected)
        {
            var plateau = new Plateau(new CoordinateLine(30), new CoordinateLine(40));
            var rover = new Rover(Guid.NewGuid(),
                                  plateau,
                                  new CoordinateLine(locationX),
                                  new CoordinateLine(locationY),
                                  RoverHead.BuildWith(roverOrientation));

            rover.TurnLeft();
            rover.TurnLeft();
            rover.TurnLeft();

            var coordinateResult = rover.GetCoordinate();

            Assert.Equal(expected, coordinateResult);
        }

        [Theory]
        [InlineData(5,10,'E',"5,10,W")]
        [InlineData(5,10,'N',"5,10,S")]
        public void Should_Rover_TurnRight_Calculated_As_Axpected(int locationX, int locationY, char roverOrientation,string expected)
        {
            var plateau = new Plateau(new CoordinateLine(30), new CoordinateLine(40));
            var rover = new Rover(Guid.NewGuid(),
                                  plateau,
                                  new CoordinateLine(locationX),
                                  new CoordinateLine(locationY),
                                  RoverHead.BuildWith(roverOrientation));

            rover.TurnRight();
            rover.TurnRight();

            var coordinateResult = rover.GetCoordinate();

            Assert.Equal(expected, coordinateResult);
        }

        [Fact]
        public void Should_RoverHead_BuildWith_Throw_RoverHeadBuildException()
        {
            Assert.Throws<RoverHeadBuildException>(() => RoverHead.BuildWith('H'));
        }
        [Theory]
        [InlineData(31, 29)]
        [InlineData(29, 41)]
        public void Should_OutOfPlateau_Throw_CoordinateOutOfRangeException(int locationX, int locationY)
        {
            var plateau = new Plateau(new CoordinateLine(30), new CoordinateLine(40));
            var roverlocationX = new CoordinateLine(locationX);
            var roverlocationY = new CoordinateLine(locationY);

            Assert.Throws<CoordinateOutOfRangeException>(() =>
            {
                var rover = new Rover(Guid.NewGuid(), plateau, roverlocationX, roverlocationY, RoverHead.BuildWith('N'));
            });
        }
        [Fact]
        public void Should_Negative_CoordinateLine_Throw_NegativeOrNullException()
        {
            Assert.Throws<NegativeOrNullException>(() => new CoordinateLine(-9));
        }
    }
}