namespace JIL;

public interface IDependencyInjector
{
    IServiceCollection Services { get; }
}

public abstract class DependencyInjectorBase
{
    readonly IServiceCollection _services;
    public IServiceCollection Services => _services;

    protected DependencyInjectorBase(IServiceCollection services)
    {
        _services = services;
    }
}

public delegate void InjectDependencies<TInjector>(TInjector injector) where TInjector : IDependencyInjector;