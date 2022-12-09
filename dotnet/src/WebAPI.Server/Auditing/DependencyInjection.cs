using JIL.Auditing;
using Microsoft.Extensions.DependencyInjection;

namespace JIL.WebAPI.Server.Auditing;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    DependencyInjector(IServiceCollection services) : base(services) { }

    static DependencyInjector? _instance;
    public static DependencyInjector GetInstance(Server.DependencyInjector parent) => _instance ??= new(parent.Services);
}

public static class DependencyExtensions
{
    public static Server.DependencyInjector AddAuditing(this Server.DependencyInjector parentInjector, InjectDependencies<DependencyInjector> injectDependencies)
    {
        var injector = DependencyInjector.GetInstance(parentInjector);
        injectDependencies(injector);
        return parentInjector;
    }

    public static Server.DependencyInjector AddAuditing(this Server.DependencyInjector parentInjector)
    {
        return parentInjector.AddAuditing(injector => injector
            .AddCurrentAuditInfoProvider()
        );
    }

    public static DependencyInjector AddCurrentAuditInfoProvider(this DependencyInjector injector)
    {
        injector.Services.AddHttpContextAccessor();
        injector.Services.AddScoped<ICurrentAuditInfoProvider, CurrentAuditInfoProvider>();
        return injector;
    }
}
