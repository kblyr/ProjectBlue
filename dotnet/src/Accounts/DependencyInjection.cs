using JIL.Accounts.Security;
using JIL.Accounts.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace JIL.Accounts;

public sealed record DependencyOptions : DependencyOptionsBase
{
    internal DependencyOptions() { }

    public Security.DependencyOptions Security { get; } = new();
    public Utilities.DependencyOptions Utilities { get; } = new();
}

public static class DependencyExtensions
{
    public static IServiceCollection AddJILAccounts(this IServiceCollection services, Action<DependencyOptions>? configure = null)
    {
        var options = new DependencyOptions();
        configure?.Invoke(options);

        if (options.IsIgnored)
        {
            return services;
        }

        return services
            .AddSecurity(options.Security)
            .AddUtilities(options.Utilities);
    }
}
