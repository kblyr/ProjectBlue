using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace JIL.EFCore;

public static class DependencyExtensions
{
    public static IServiceCollection AddDbContextFactory<T>(this IServiceCollection services, Assembly entityConfigAssembly, Action<DbContextOptionsBuilder>? configure = null) where T : DbContext
    {
        services.AddDbContextFactory<T>(configure);
        services.AddSingleton<IEntityConfigAssemblyProvider<T>>(sp => new EntityConfigAssemblyProvider<T>(entityConfigAssembly));
        return services;
    }
}