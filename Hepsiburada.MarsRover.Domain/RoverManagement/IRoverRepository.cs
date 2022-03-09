using System;
using System.Threading.Tasks;

namespace Hepsiburada.MarsRover.Domain.RoverManagement
{
    public interface IRoverRepository
    {
        Task<Rover> GetByIdAsync(Guid roverId);
        Task<Rover> SaveAsync(Rover rover);
    }
}
