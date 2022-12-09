using JIL.Authorization;
using JIL.Utilities;

namespace JIL;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    DependencyInjector(IServiceCollection services) : base(services) { }

    static DependencyInjector? _instance;
    public static DependencyInjector GetInstance(IServiceCollection services) => _instance ??= new(services);
}

public static class DependencyExtensions
{
    public static IServiceCollection AddJIL(this IServiceCollection services, InjectDependencies<DependencyInjector> injectDependencies)
    {
        var injector = DependencyInjector.GetInstance(services);
        injectDependencies(injector);
        return services;
    }

    public static IServiceCollection AddJIL(this IServiceCollection services)
    {
        return services.AddJIL(injector => injector
            .AddAuthorization()
            .AddUtilities()
        );
    }
}
