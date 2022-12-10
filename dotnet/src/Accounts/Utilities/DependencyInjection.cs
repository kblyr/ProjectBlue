using Microsoft.Extensions.DependencyInjection;

namespace JIL.Accounts.Utilities;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    DependencyInjector(IServiceCollection services) : base(services) { }

    static DependencyInjector? _instance;
    public static DependencyInjector GetInstance(Accounts.DependencyInjector parent) => _instance ??= new(parent.Services);
}

public static class DependencyExtensions
{
    public static Accounts.DependencyInjector AddUtilities(this Accounts.DependencyInjector parentInjector, InjectDependencies<DependencyInjector> injectDependencies)
    {
        var injector = DependencyInjector.GetInstance(parentInjector);
        injectDependencies(injector);
        return parentInjector;
    }

    public static Accounts.DependencyInjector AddUtilities(this Accounts.DependencyInjector parentInjector)
    {
        return parentInjector.AddUtilities(injector => injector
            .AddUserFullNameBuilder()
        );
    }

    public static DependencyInjector AddUserFullNameBuilder(this DependencyInjector injector)
    {
        injector.Services.AddSingleton<IUserFullNameBuilder, UserFullNameBuilder>();
        return injector;
    }
}
