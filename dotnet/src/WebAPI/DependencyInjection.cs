using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JIL.WebAPI;

public sealed record DependencyOptions : DependencyOptionsBase
{
    internal DependencyOptions() { }

    public FeaturesObj Features { get; } = new();

    public sealed record FeaturesObj
    {
        internal FeaturesObj() { }

        public bool ApiResponseTypeRegistryKeyProvider { get; set; } = true; 
    }
}

public static class DependencyExtensions
{
    public static IServiceCollection AddJILWebAPI(this IServiceCollection services, Action<DependencyOptions>? configure = null)
    {
        var options = new DependencyOptions();
        configure?.Invoke(options);

        if (options.IsIgnored)
        {
            return services;
        }

        if (options.Features.ApiResponseTypeRegistryKeyProvider)
        {
            services.TryAddSingleton<IApiResponseTypeRegistryKeyProvider, ApiResponseTypeRegistryKeyProvider>();
        }

        return services;
    }
}