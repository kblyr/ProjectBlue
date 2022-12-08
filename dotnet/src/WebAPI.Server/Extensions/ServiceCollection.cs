using System.Reflection;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace JIL;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddMapster(this IServiceCollection services, params Assembly[] assemblies)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(assemblies);
        return services
            .AddSingleton(config)
            .AddScoped<MapsterMapper.IMapper, ServiceMapper>();
    }
}