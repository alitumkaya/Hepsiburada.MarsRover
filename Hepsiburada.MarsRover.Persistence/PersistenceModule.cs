using Hepsiburada.MarsRover.Domain;
using Hepsiburada.MarsRover.Domain.RoverManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hepsiburada.MarsRover.Persistence
{
    public static class PersistenceModule
    {
        public static void AddPersistenceDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDomainDependencies(configuration);

            services.AddDbContext<RoverContext>(options => options.UseInMemoryDatabase("RoverDb"));

            services.AddScoped<IRoverRepository, RoverRepository>();
        }
    }
}
