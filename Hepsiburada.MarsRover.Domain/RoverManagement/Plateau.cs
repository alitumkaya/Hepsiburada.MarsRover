using Hepsiburada.MarsRover.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hepsiburada.MarsRover.Domain.RoverManagement
{
    public class Plateau : ValueObject
    {
        public CoordinateLine CoordinateX { get; }

        public CoordinateLine CoordinateY { get; }

        protected Plateau()
        {

        }
        public Plateau(CoordinateLine coordinateX, CoordinateLine coordinateY)
        {
            CoordinateX = coordinateX;
            CoordinateY = coordinateY;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return new object[] { CoordinateX, CoordinateY };
        }
    }
}
