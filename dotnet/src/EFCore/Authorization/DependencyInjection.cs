using JIL.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JIL.EFCore.Authorization;

public sealed record DependencyOptions : DependencyOptionsBase
{
    internal DependencyOptions() { }

    public FeaturesObj Features { get; } = new();

    public sealed record FeaturesObj
    {
        internal FeaturesObj() { }
            
        public bool RolesLoader { get; set; } = true;
        public bool PermissionsLoader { get; set; } = true;
    }
}

static class DependencyExtensions
{
    public static IServiceCollection AddAuthorization(this IServiceCollection services, DependencyOptions options)
    {
        if (options.IsIgnored)
        {
            return services;
        }

        if (options.Features.RolesLoader)
        {
            services.TryAddScoped<IRolesLoader, RolesLoader>();
        }

        if (options.Features.PermissionsLoader)
        {
            services.TryAddScoped<IPermissionsLoader, PermissionsLoader>();
        }

        return services;
    }
}