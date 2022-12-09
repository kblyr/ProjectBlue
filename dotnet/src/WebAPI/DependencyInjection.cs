namespace JIL.WebAPI;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    DependencyInjector(IServiceCollection services) : base(services) { }

    static DependencyInjector? _instance;
    public static DependencyInjector GetInstance(IServiceCollection services) => _instance ??= new(services);
}

public static class DependencyExtensions
{
    public static IServiceCollection AddJILWebAPI(this IServiceCollection services, InjectDependencies<DependencyInjector> injectDependencies)
    {
        var injector = DependencyInjector.GetInstance(services);
        injectDependencies(injector);
        return services;
    }

    public static IServiceCollection AddJILWebAPI(this IServiceCollection services)
    {
        return services.AddJILWebAPI(injector => injector
            .AddApiResponseTypeRegistryKeyProvider()
        );
    }

    public static DependencyInjector AddApiResponseTypeRegistryKeyProvider(this DependencyInjector injector)
    {
        injector.Services.AddSingleton<IApiResponseTypeRegistryKeyProvider, ApiResponseTypeRegistryKeyProvider>();
        return injector;
    }
}