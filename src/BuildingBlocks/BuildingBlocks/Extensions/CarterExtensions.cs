using System.Reflection;
using Carter;

namespace BuildingBlocks.Extensions;

public static class CarterExtensions
{
    public static void RegisterEndpointsFromAssemblies(this CarterConfigurator config, Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var typesImplementingICarterModule = assembly.GetTypes()
                .Where(type => typeof(ICarterModule).IsAssignableFrom(type) && !type.IsAbstract)
                .ToArray();

            config.WithModules(typesImplementingICarterModule);
        }
    }
}