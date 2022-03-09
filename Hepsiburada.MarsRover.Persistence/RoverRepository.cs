using Hepsiburada.MarsRover.Domain.RoverManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hepsiburada.MarsRover.Persistence
{
    public class RoverRepository : IRoverRepository
    {
        private RoverContext dbContext { get; set; }
        public RoverRepository(RoverContext roverContext)
        {
            dbContext = roverContext;
        }
        public async Task<Rover> GetByIdAsync(Guid roverId)
        {
            var rover = dbContext.Rovers.Local.SingleOrDefault(f => f.Id == roverId);
            if (rover == null)
                rover = await dbContext.Rovers.SingleAsync(f => f.Id == roverId);
            else
                await dbContext.Entry(rover).ReloadAsync();

            return rover;
        }

        public async Task<Rover> SaveAsync(Rover rover)
        {
            var entity = await dbContext.Entry(rover).GetDatabaseValuesAsync();

            if (entity == null)
                await dbContext.Rovers.AddAsync(rover);
            else
                dbContext.Rovers.Update(rover);

            await dbContext.SaveChangesAsync();
            return rover;
        }
    }
}
