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

    public static IServiceCollection AddHashIdConverter<T>(this IServiceCollection services, Action<HashIdConverterOptions>? configure = null)
    {
        var options = new HashIdConverterOptions();
        configure?.Invoke(options);

        if (options.IsIgnored)
        {
            return services;
        }

        var converterType = typeof(T);
        var converter = Activator.CreateInstance(converterType, options.Salt, options.MinHashLength);

        if (converter is not null)
        {
            services.AddSingleton(converterType, sp => converter);
        }

        return services;
    }
}

public sealed record HashIdConverterOptions : DependencyOptionsBase
{
    internal HashIdConverterOptions() { }

    public string Salt { get; set; } = "";
    public int MinHashLength { get; set; } = 4;
}