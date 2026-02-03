using System.Reflection;
using Carter;

namespace BuildingBlocks.Extensions;

public static class CarterExtensions
{
    public static void RegisterEndpointsFromAssemblies(this CarterConfigurator config, Assembly[] assemblies)
    {
        var moduleTypes = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(ICarterModule).IsAssignableFrom(t)
                        && t is { IsInterface: false, IsAbstract: false });

        config.WithModules(moduleTypes.ToArray());
    }
}