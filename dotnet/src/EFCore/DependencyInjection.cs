using System.Reflection;
using JIL.EFCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace JIL.EFCore;

public sealed record DependencyOptions : DependencyOptionsBase
{
    internal DependencyOptions() { }

    public Authorization.DependencyOptions Authorization { get; } = new();
}

public static class DependencyExtensions
{
    public static IServiceCollection AddJILEFCore(this IServiceCollection services, Action<DependencyOptions>? configure = null)
    {
        var options = new DependencyOptions();
        configure?.Invoke(options);

        if (options.IsIgnored)
        {
            return services;
        }

        return services
            .AddAuthorization(options.Authorization);
    }

    public static IServiceCollection AddDbContextFactory<T>(this IServiceCollection services, Assembly entityConfigAssembly, Action<DbContextOptionsBuilder>? configure = null) where T : DbContext
    {
        services.AddDbContextFactory<T>(configure);
        services.AddSingleton<IEntityConfigAssemblyProvider<T>>(sp => new EntityConfigAssemblyProvider<T>(entityConfigAssembly));
        return services;
    }
}