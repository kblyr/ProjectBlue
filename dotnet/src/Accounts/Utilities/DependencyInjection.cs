using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JIL.Accounts.Utilities;

public sealed record DependencyOptions : DependencyOptionsBase
{
    internal DependencyOptions() { }

    public FeaturesObj Features { get; } = new();

    public sealed record FeaturesObj
    {
        internal FeaturesObj() { }

        public bool UserFullNameBuilder { get; set; } = true;
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

        if (options.Features.UserFullNameBuilder)
        {
            services.TryAddSingleton<IUserFullNameBuilder, UserFullNameBuilder>();
        }

        return services;
    }
}
