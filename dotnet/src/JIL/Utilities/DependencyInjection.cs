namespace JIL.Utilities;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    DependencyInjector(IServiceCollection services) : base(services) { }
    
    static DependencyInjector? _instance;
    public static DependencyInjector GetInstance(JIL.DependencyInjector parent) => _instance ??= new(parent.Services);
}

public static class DependencyExtensions
{
    public static JIL.DependencyInjector AddUtilities(this JIL.DependencyInjector parentInjector, InjectDependencies<DependencyInjector> injectDependencies)
    {
        var injector = DependencyInjector.GetInstance(parentInjector);
        injectDependencies(injector);
        return parentInjector;
    }

    public static JIL.DependencyInjector AddUtilities(this JIL.DependencyInjector parentInjector)
    {
        return parentInjector.AddUtilities(injector => injector
            .AddCurrentTimestampProvider()
            .AddRandomStringGenerator()
        );
    }

    public static DependencyInjector AddCurrentTimestampProvider(this DependencyInjector injector)
    {
        injector.Services.AddSingleton<ICurrentTimestampProvider, CurrentTimestampProvider>();
        return injector;
    }

    public static DependencyInjector AddRandomStringGenerator(this DependencyInjector injector)
    {
        injector.Services.AddSingleton<IRandomStringGenerator, RandomStringGenerator>();
        return injector;
    }
}
