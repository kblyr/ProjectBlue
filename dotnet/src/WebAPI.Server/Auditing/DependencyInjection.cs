using JIL.Auditing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JIL.WebAPI.Server.Auditing;

public sealed record DependencyOptions
{
    internal DependencyOptions() { }

    public FeaturesObj Features { get; } = new();

    public sealed record FeaturesObj
    {
        internal FeaturesObj() { }

        public bool CurrentAuditInfoProvider { get; set; } = true;
    }
}

static class DependencyExtensions
{
    public static IServiceCollection AddAuditing(this IServiceCollection services, DependencyOptions options)
    {
        if (options.Features.CurrentAuditInfoProvider)
        {
            services.AddHttpContextAccessor();
            services.TryAddScoped<ICurrentAuditInfoProvider, CurrentAuditInfoProvider>();
        }

        return services;
    }
}
