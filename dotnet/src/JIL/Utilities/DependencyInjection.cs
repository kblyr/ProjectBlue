using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JIL.Utilities;

public sealed record DependencyOptions : DependencyOptionsBase
{
    internal DependencyOptions() { }

    public FeaturesObj Features { get; } = new();

    public sealed record FeaturesObj
    {
        internal FeaturesObj() { }

        public CurrentDomainProviderObj CurrentDomainProvider { get; } = new();
        public bool CurrentTimestampProvider { get; set; } = true;
        public bool RandomStringGenerator { get; set; } = true;

        public sealed record CurrentDomainProviderObj : DependencyOptionsBase
        {
            internal CurrentDomainProviderObj() { }

            public string? Current { get; set; }
        }
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

        if (options.Features.CurrentDomainProvider.IsIncluded)
        {
            services.TryAddSingleton<ICurrentDomainProvider>(sp => new CurrentDomainProvider(options.Features.CurrentDomainProvider.Current ?? throw new Exception("Current Domain is unspecified")));
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
