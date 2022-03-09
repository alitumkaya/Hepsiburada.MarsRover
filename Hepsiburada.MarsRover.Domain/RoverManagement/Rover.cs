using Hepsiburada.MarsRover.Domain.Common;
using System;
using System.Collections.Generic;

namespace Hepsiburada.MarsRover.Domain.RoverManagement
{
    public class Rover
    {

        public Guid Id { get; protected set; }
        public Plateau Plateau { get; protected set; }
        public CoordinateLine LocationX { get; protected set; }
        public CoordinateLine LocationY { get; protected set; }
        public RoverHead RoverHead { get; protected set; }


        private IDictionary<char, Action> _movementDirectives = new Dictionary<char, Action>();
        protected Rover()
        {

        }
        public Rover(Guid id,
                     Plateau plateau,
                     CoordinateLine locationX,
                     CoordinateLine locationY,
                     RoverHead roverHead)
        {
            if (locationX.Value < 0 || locationX > plateau.CoordinateX)
                throw new CoordinateOutOfRangeException();
            if (locationY.Value < 0 || locationY > plateau.CoordinateY)
                throw new CoordinateOutOfRangeException();

            Id = id;
            Plateau = plateau;
            LocationX = locationX;
            LocationY = locationY;
            RoverHead = roverHead;

            _movementDirectives.Add('E', () => { LocationX = LocationX.MoveForward(); });
            _movementDirectives.Add('N', () => { LocationY = LocationY.MoveForward(); });
            _movementDirectives.Add('W', () => { LocationX = LocationX.MoveBack(); });
            _movementDirectives.Add('S', () => { LocationY = LocationY.MoveBack(); });
        }
        public void TurnLeft()
        {
            RoverHead = this.RoverHead.TurnLeft();
        }
        public void TurnRight()
        {
            RoverHead = this.RoverHead.TurnRight();
        }
        public void Move()
        {
            _movementDirectives[RoverHead.HeadValue].Invoke();
        }
        public string GetCoordinate()
        {
            return $"{LocationX.Value},{LocationY.Value},{RoverHead.HeadValue}";
        }
    }
}
