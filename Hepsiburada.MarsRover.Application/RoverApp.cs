using AutoMapper;
using Hepsiburada.MarsRover.Application.Dtos;
using Hepsiburada.MarsRover.Domain.Common;
using Hepsiburada.MarsRover.Domain.RoverManagement;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hepsiburada.MarsRover.Application
{
    public class RoverApp : IRoverApp
    {
        private readonly IRoverRepository roverRepository;
        private readonly IMapper mapper;
        private readonly char[] availableCommands = new[] { 'L', 'R', 'M' };
        public RoverApp(IRoverRepository roverRepository,
                        IMapper mapper)
        {
            this.roverRepository = roverRepository;
            this.mapper = mapper;
        }

        public async Task<RoverDto> AddRover(RoverDto roverDto)
        {
            var plateau = new Plateau(new CoordinateLine(roverDto.Plateau.CoordinateX),
                                      new CoordinateLine(roverDto.Plateau.CoordinateY));

            var rover = new Rover(Guid.NewGuid(),
                                  plateau,
                                  new CoordinateLine(roverDto.LocationX),
                                  new CoordinateLine(roverDto.LocationY),
                                  RoverHead.BuildWith(roverDto.RoverHead));

            await roverRepository.SaveAsync(rover);

            return mapper.Map<RoverDto>(rover);
        }

        public async Task<RoverDto> ControlRover(Guid roverId, string commandLetter)
        {
            var commands = commandLetter.ToCharArray()
               .Where(w => !char.IsWhiteSpace(w));

            if (commands.Any(a => !availableCommands.Contains(a)))
                throw new ArgumentException("The position is made up of two integers and a letter separated by spaces, corresponding to the x and y co-ordinates and the rover's orientation.");

            var rover = await roverRepository.GetByIdAsync(roverId);

            try
            {
                foreach (var command in commands)
                {
                    switch (command)
                    {
                        case 'L':
                            rover.TurnLeft();
                            break;
                        case 'R':
                            rover.TurnRight();
                            break;
                        case 'M':
                            rover.Move();
                            break;
                        default: break;
                    }
                }
            }
            catch (NegativeOrNullException)
            {
                throw new Exception("Cannot execute command because it exceeds plateau limits.");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            await roverRepository.SaveAsync(rover);

            return mapper.Map<RoverDto>(rover);
        }
    }
}
