using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JIL.Authorization;

public sealed record DependencyOptions
{
    internal DependencyOptions() { }

    public FeaturesObj Features { get; } = new();

    public sealed record FeaturesObj
    {
        internal FeaturesObj() { }

        public bool RoleVerifier { get; set; } = true;
        public bool PermissionVerifier { get; set; } = true;
    }
}

static class DependencyExtensions
{
    public static IServiceCollection AddAuthorization(this IServiceCollection services, DependencyOptions options)
    {
        if (options.Features.RoleVerifier)
        {
            services.TryAddSingleton<IRoleVerifier, RoleVerifier>();
        }

        if (options.Features.PermissionVerifier)
        {
            services.TryAddSingleton<IPermissionVerifier, PermissionVerifier>();
        }

        return services;
    }
}
