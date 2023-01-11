using System.Reflection;
using JIL.WebAPI.Server.Auditing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JIL.WebAPI.Server;

public sealed record DependencyOptions : DependencyOptionsBase
{
    internal DependencyOptions() { }
    
    public FeaturesObj Features { get; } = new();
    public Auditing.DependencyOptions Auditing { get; } = new();

    public sealed record FeaturesObj
    {
        internal FeaturesObj() { }

        public bool ApiMediator { get; set; } = true;
        public bool ResponseMapper { get; set; } = true;
        public ResponseTypeMapRegistryObj ResponseTypeMapRegistry { get; } = new();

        public sealed record ResponseTypeMapRegistryObj : DependencyOptionsBase
        {
            Assembly[] _assemblies = Array.Empty<Assembly>();
            public Assembly[] Assemblies 
            {
                get => _assemblies;
                set => _assemblies = value is null ? Array.Empty<Assembly>() : value;
            }
        }
    }
}

public static class DependencyExtensions
{
    public static IServiceCollection AddJILWebAPIServer(this IServiceCollection services, Action<DependencyOptions>? configure = null)
    {
        var options = new DependencyOptions();
        configure?.Invoke(options);

        if (options.IsIgnored)
        {
            return services;
        }

        if (options.Features.ApiMediator)
        {
            services.TryAddTransient<IApiMediator, ApiMediator>();
        }

        if (options.Features.ResponseMapper)
        {
            services.TryAddTransient<IResponseMapper, ResponseMapper>();
        }

        if (options.Features.ResponseTypeMapRegistry.IsIncluded)
        {
            services.TryAddSingleton<IResponseTypeMapRegistry, ResponseTypeMapRegistry>();
            services.TryAddSingleton<ResponseTypeMapAssemblyScanner>(sp => new ResponseTypeMapAssemblyScanner(options.Features.ResponseTypeMapRegistry.Assemblies));
        }

        return services
            .AddAuditing(options.Auditing);
    }
}
