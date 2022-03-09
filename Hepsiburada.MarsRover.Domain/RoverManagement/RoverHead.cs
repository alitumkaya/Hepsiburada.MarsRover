using Hepsiburada.MarsRover.Domain.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Hepsiburada.MarsRover.Domain.RoverManagement
{
    public class RoverHead : ValueObject
    {
        private static ConcurrentDictionary<int, char> _angleMaps = new ConcurrentDictionary<int, char>();
        private static ConcurrentDictionary<char, int> _headingMaps = new ConcurrentDictionary<char, int>();
        static RoverHead()
        {
            _angleMaps.TryAdd(0, 'E');
            _angleMaps.TryAdd(90, 'N');
            _angleMaps.TryAdd(180, 'W');
            _angleMaps.TryAdd(270, 'S');

            _headingMaps.TryAdd('E', 0);
            _headingMaps.TryAdd('N', 90);
            _headingMaps.TryAdd('W', 180);
            _headingMaps.TryAdd('S', 270);
        }
       
        private int _angle = 0;
        public char HeadValue => _angleMaps[_angle];

        protected RoverHead()
        {

        }
        private RoverHead(int angle) { _angle = Math.Abs((_angle + angle) % 360); }
        public RoverHead TurnLeft() => new RoverHead(_angle + 90);
        public RoverHead TurnRight() => new RoverHead(_angle - 90);
        public static RoverHead BuildWith(char roverHead)
        {
            if (!_headingMaps.ContainsKey(roverHead))
                throw new RoverHeadBuildException();

            return new RoverHead(_headingMaps[roverHead]);
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return new object[HeadValue];
        }
    }
    public class RoverHeadBuildException : Exception
    {
        public override string Message => $"Invalid parameter! Parameter can be 'E', 'W', 'N' or 'S'!";
    }
}
