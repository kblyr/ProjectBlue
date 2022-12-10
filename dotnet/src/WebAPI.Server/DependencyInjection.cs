using System.Reflection;
using JIL.WebAPI.Server.Auditing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JIL.WebAPI.Server;

public sealed record DependencyOptions
{
    internal DependencyOptions() { }
    
    public FeaturesObj Features { get; } = new();
    public Auditing.DependencyOptions? Auditing { get; set; } = new();

    public sealed record FeaturesObj
    {
        internal FeaturesObj() { }

        public bool ApiMediator { get; set; } = true;
        public bool ResponseMapper { get; set; } = true;
        public ResponseTypeMapRegistryObj? ResponseTypeMapRegistry { get; set; } = new();

        public sealed record ResponseTypeMapRegistryObj
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

        if (options.Features.ApiMediator)
        {
            services.TryAddTransient<IApiMediator, ApiMediator>();
        }

        if (options.Features.ResponseMapper)
        {
            services.TryAddTransient<IResponseMapper, ResponseMapper>();
        }

        if (options.Features.ResponseTypeMapRegistry is not null)
        {
            services.TryAddSingleton<IResponseTypeMapRegistry, ResponseTypeMapRegistry>();
            services.TryAddSingleton<ResponseTypeMapAssemblyScanner>(sp => new ResponseTypeMapAssemblyScanner(options.Features.ResponseTypeMapRegistry.Assemblies));
        }

        if (options.Auditing is not null)
        {
            services.AddAuditing(options.Auditing);
        }

        return services;
    }
}
