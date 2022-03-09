using Hepsiburada.MarsRover.Domain.RoverManagement;
using Microsoft.EntityFrameworkCore;

namespace Hepsiburada.MarsRover.Persistence
{
    public class RoverContext : DbContext
    {
        public DbSet<Rover> Rovers { get; set; }
        public RoverContext(DbContextOptions<RoverContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Rover>(b =>
            {
                b.HasKey(k => k.Id);

                b.OwnsOne(o => o.Plateau, p =>
                {
                    p.Property(bp => bp.CoordinateY).HasConversion(from => from.Value, to => new CoordinateLine(to));
                    p.Property(bp => bp.CoordinateX).HasConversion(from => from.Value, to => new CoordinateLine(to));

                });

                b.Property(bp => bp.LocationX).HasConversion(from => from.Value, to => new CoordinateLine(to));
                b.Property(bp => bp.LocationY).HasConversion(from => from.Value, to => new CoordinateLine(to));
                b.Property(bp => bp.RoverHead).HasConversion(from => from.HeadValue, to => RoverHead.BuildWith(to));

            });
        }
    }
}
