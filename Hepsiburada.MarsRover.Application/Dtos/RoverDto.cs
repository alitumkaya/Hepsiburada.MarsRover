using System;

namespace Hepsiburada.MarsRover.Application.Dtos
{
    public class RoverDto
    {
        public Guid Id { get; set; }
        public PlateauDto Plateau { get; set; }
        public int LocationX { get; set; }
        public int LocationY { get; set; }
        public char RoverHead { get; set; }

        public string Coordinate { get; set; }
    }
}
