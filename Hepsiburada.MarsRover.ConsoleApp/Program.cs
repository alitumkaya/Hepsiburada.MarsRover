// See https://aka.ms/new-console-template for more information

using Hepsiburada.MarsRover.Application;
using Hepsiburada.MarsRover.Application.Dtos;
using Hepsiburada.MarsRover.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
       {
           services.AddApplicationDependencies(context.Configuration);
           services.AddPersistenceDependencies(context.Configuration);
       })
        .Build();


await StartAppAsync(host.Services);
await host.RunAsync();

static async Task StartAppAsync(IServiceProvider services)
{
    using IServiceScope serviceScope = services.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;

    IRoverApp roverApp = provider.GetRequiredService<IRoverApp>();

    var plateauCoordinates = await ReadPlateauCoordinatesAsync();
    var plateau = new PlateauDto() { CoordinateX = plateauCoordinates.Item1, CoordinateY = plateauCoordinates.Item2 };

    var roverCount = 2;
    for (int i = 1; i <= roverCount; i++)
    {
        var roverCoordinates = await ReadRoverCoordinatesAndOrientationAsync();
        var roverDto = new RoverDto()
        {
            LocationX = roverCoordinates.Item1,
            LocationY = roverCoordinates.Item2,
            Plateau = plateau,
            RoverHead = roverCoordinates.Item3
        };
        var rover = await roverApp.AddRover(roverDto);


        var roverCoordinate = await CommadRoverAsync(roverApp, rover.Id);

        Console.WriteLine("Final Coordinate : " + roverCoordinate);

    }
}

static async Task<(int, int)> ReadPlateauCoordinatesAsync()
{
    while (true)
    {
        try
        {
            Console.WriteLine(@"Please enter is the upper-right coordinates of the plateau, 
the lower-left coordinates are assumed to be 0, 0.");
            var coordinates = Console.ReadLine()
                ?.Split(' ')
                .Where(w => !string.IsNullOrWhiteSpace(w))
                .ToArray();

            if (coordinates?.Length != 2
                        || !int.TryParse(coordinates[0].ToString(), out var tryX)
                        || !int.TryParse(coordinates[1].ToString(), out var tryY)
                        )
                throw new Exception("The coordinates is made up of two integers separated by spaces.");

            return await Task.FromResult((tryX, tryY));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception : {ex.Message}");
        }
    }
}
static Task<(int, int, char)> ReadRoverCoordinatesAndOrientationAsync()
{
    while (true)
    {
        try
        {
            Console.WriteLine(@"Please enter the position that made up of two integers and a letter separated by spaces, corresponding to the x
and y co-ordinates and the rover's orientation.");
            var coordinates = Console.ReadLine()
                ?.Split(' ')
                .Where(w => !string.IsNullOrWhiteSpace(w))
                .ToArray();

            if (coordinates?.Length != 3
                    || !int.TryParse(coordinates[0].ToString(), out var tryX)
                    || !int.TryParse(coordinates[1].ToString(), out var tryY)
                    || !char.TryParse(coordinates[2], out var orientation)
                    )
                throw new Exception("The position is made up of two integers and a letter separated by spaces, corresponding to the x and y co-ordinates and the rover's orientation.");


            return Task.FromResult((tryX, tryY, orientation));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception : {ex.Message}");
        }
    }
}
static async Task<string> CommadRoverAsync(IRoverApp roverApp, Guid roverId)
{
    while (true)
    {
        try
        {
            Console.WriteLine(@"Please enter string of letter to control rover. The possible letters are 'L', 'R' and
'M'. 'L' and 'R' makes the rover spin 90 degrees left or right respectively, without moving from its
current spot. 'M' means move forward one grid point, and maintain the same heading.");

            var command = Console.ReadLine() ?? "";

            var rover = await roverApp.ControlRover(roverId, command);

            return rover.Coordinate;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception : {ex.Message}");
        }
    }
}