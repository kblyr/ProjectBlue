using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace JIL.WebAPI.Server;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    DependencyInjector(IServiceCollection services) : base(services) { }

    static DependencyInjector? _instance;
    public static DependencyInjector GetInstance(IServiceCollection services) => _instance ??= new(services);
}

public sealed record DependencyOptions
{
    DependencyOptions() { }
    
    Assembly[] _responseTypeMapAssemblies = Array.Empty<Assembly>();
    public Assembly[] ResponseTypeMapAssemblies 
    {
        get => _responseTypeMapAssemblies;
        set => _responseTypeMapAssemblies = value is null ? Array.Empty<Assembly>() : value;
    }

    internal static DependencyOptions CreateInstance() => new();
}

public static class DependencyExtensions
{
    public static IServiceCollection AddJILWebAPIServer(this IServiceCollection services, InjectDependencies<DependencyInjector> injectDependencies)
    {
        var injector = DependencyInjector.GetInstance(services);
        injectDependencies(injector);
        return services;
    }

    public static IServiceCollection AddJILWebAPIServer(this IServiceCollection services, Action<DependencyOptions>? configure = null)
    {
        var options = DependencyOptions.CreateInstance();
        configure?.Invoke(options);

        return services.AddJILWebAPIServer(injector => injector
            .AddApiMediator()
            .AddResponseMapper()
            .AddResponseTypeMapRegistry(options.ResponseTypeMapAssemblies)
        );
    }

    public static DependencyInjector AddApiMediator(this DependencyInjector injector)
    {
        injector.Services.AddTransient<IApiMediator, ApiMediator>();
        return injector;
    }

    public static DependencyInjector AddResponseMapper(this DependencyInjector injector)
    {
        injector.Services.AddTransient<IResponseMapper, ResponseMapper>();
        return injector;
    }

    public static DependencyInjector AddResponseTypeMapRegistry(this DependencyInjector injector, Assembly[] assemblies)
    {
        injector.Services.AddSingleton<IResponseTypeMapRegistry, ResponseTypeMapRegistry>();
        injector.Services.AddSingleton<ResponseTypeMapAssemblyScanner>(sp => new ResponseTypeMapAssemblyScanner(assemblies));
        return injector;
    }
}
