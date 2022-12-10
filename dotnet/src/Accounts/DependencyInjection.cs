using JIL.Accounts.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JIL.Accounts;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    DependencyInjector(IServiceCollection services) : base(services) { }

    static DependencyInjector? _instance;
    public static DependencyInjector GetInstance(IServiceCollection services) => _instance ??= new(services);
}

public sealed record DependencyOptions
{
    internal DependencyOptions() { }

    public IConfiguration? Configuration { get; set; }
    public Security.DependencyOptions Security { get; } = new();
}

public static class DependencyExtensions
{
    public static IServiceCollection AddJILAccounts(this IServiceCollection services, InjectDependencies<DependencyInjector> injectDependencies)
    {
        var injector = DependencyInjector.GetInstance(services);
        injectDependencies(injector);
        return services;
    }

    public static IServiceCollection AddJILAccounts(this IServiceCollection services, Action<DependencyOptions>? configure = null)
    {
        var options = new DependencyOptions();
        configure?.Invoke(options);

        return services.AddJILAccounts(injector => injector
            .AddUtilities()
        );
    }
}
