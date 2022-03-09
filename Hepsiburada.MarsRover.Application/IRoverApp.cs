using Hepsiburada.MarsRover.Application.Dtos;
using System;
using System.Threading.Tasks;

namespace Hepsiburada.MarsRover.Application
{
    public interface IRoverApp
    {
        Task<RoverDto> AddRover(RoverDto roverDto);
        Task<RoverDto> ControlRover(Guid roverId, string commandLetter);
    }
}
