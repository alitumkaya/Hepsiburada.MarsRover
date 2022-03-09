using Hepsiburada.MarsRover.Domain.Common;
using System;
using System.Collections.Generic;

namespace Hepsiburada.MarsRover.Domain.RoverManagement
{
    public class CoordinateLine : ValueObject
    {
        public int Value { get; private set; }
        protected CoordinateLine()
        {

        }
        public CoordinateLine(int value)
        {
            Check.NotNullOrNegative(value, nameof(value));
            Value = value;
        }
        public CoordinateLine MoveForward() { return new CoordinateLine(Value + 1); }
        public CoordinateLine MoveBack() { return new CoordinateLine(Value - 1); }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static bool operator <(CoordinateLine exp1, CoordinateLine exp2) => exp1.Value < exp2.Value;
        public static bool operator >(CoordinateLine exp1, CoordinateLine exp2) => exp1.Value > exp2.Value;
    }

    public class CoordinateOutOfRangeException : ArgumentOutOfRangeException
    {
        public CoordinateOutOfRangeException() : base()
        {

        }
        public override string Message => "Coordinate is out of range!";
    }
}
