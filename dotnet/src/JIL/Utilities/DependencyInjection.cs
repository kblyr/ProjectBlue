using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JIL.Utilities;

public sealed record DependencyOptions : DependencyOptionsBase
{
    internal DependencyOptions() { }

    public FeaturesObj Features { get; } = new();

    public sealed record FeaturesObj
    {
        internal FeaturesObj() { }

        public bool CurrentTimestampProvider { get; set; } = true;
        public bool RandomStringGenerator { get; set; } = true;
    }
}

static class DependencyExtensions
{
    public static IServiceCollection AddUtilities(this IServiceCollection services, DependencyOptions options)
    {
        if (options.IsIgnored)
        {
            return services;
        }

        if (options.Features.CurrentTimestampProvider)
        {
            services.TryAddSingleton<ICurrentTimestampProvider, CurrentTimestampProvider>();
        }

        if (options.Features.RandomStringGenerator)
        {
            services.TryAddSingleton<IRandomStringGenerator, RandomStringGenerator>();
        }

        return services;
    }
}
