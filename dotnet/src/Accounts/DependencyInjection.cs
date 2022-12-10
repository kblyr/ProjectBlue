using JIL.Accounts.Security;
using JIL.Accounts.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JIL.Accounts;

public sealed record DependencyOptions
{
    internal DependencyOptions() { }

    public Security.DependencyOptions? Security { get; set; } = new();
    public Utilities.DependencyOptions? Utilities { get; set; } = new();
}

public static class DependencyExtensions
{
    public static IServiceCollection AddJILAccounts(this IServiceCollection services, Action<DependencyOptions>? configure = null)
    {
        var options = new DependencyOptions();
        configure?.Invoke(options);

        if (options.Security is not null)
        {
            services.AddSecurity(options.Security);
        }

        if (options.Utilities is not null)
        {
            services.AddUtilities(options.Utilities);
        }

        return services;
    }
}
