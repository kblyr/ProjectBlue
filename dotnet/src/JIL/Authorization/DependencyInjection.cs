namespace JIL.Authorization;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    DependencyInjector(IServiceCollection services) : base(services) { }

    static DependencyInjector? _instance;
    public static DependencyInjector GetInstance(JIL.DependencyInjector parent) => _instance ??= new(parent.Services);
}

public static class DependencyExtensions
{
    public static JIL.DependencyInjector AddAuthorization(this JIL.DependencyInjector parentInjector, InjectDependencies<DependencyInjector> injectDependencies)
    {
        var injector = DependencyInjector.GetInstance(parentInjector);
        injectDependencies(injector);
        return parentInjector;
    }

    public static JIL.DependencyInjector AddAuthorization(this JIL.DependencyInjector parentInjector)
    {
        return parentInjector.AddAuthorization(injector => injector
            .AddRoleVerifier()
            .AddPermissionVerifier()
        );
    }

    public static DependencyInjector AddRoleVerifier(this DependencyInjector injector)
    {
        injector.Services.AddSingleton<IRoleVerifier, RoleVerifier>();
        return injector;
    }

    public static DependencyInjector AddPermissionVerifier(this DependencyInjector injector)
    {
        injector.Services.AddSingleton<IPermissionVerifier, PermissionVerifier>();
        return injector;
    }
}
