using JIL.Authorization;
using JIL.Utilities;

namespace JIL;

public abstract record DependencyOptionsBase
{
    public bool IsIncluded { get; private set; } = true;
    public bool IsIgnored  => !IsIncluded;

    public void Ignore()
    {
        IsIncluded = false;
    }
}

public sealed record DependencyOptions : DependencyOptionsBase
{
    internal DependencyOptions() { }
    
    public Authorization.DependencyOptions Authorization { get; } = new();
    public Utilities.DependencyOptions Utilities { get; } = new();
}

public static class DependencyExtensions
{
    public static IServiceCollection AddJIL(this IServiceCollection services, Action<DependencyOptions>? configure = null)
    {
        var options = new DependencyOptions();
        configure?.Invoke(options);

        if (options.IsIgnored)
        {
            return services;
        }

        return services
            .AddAuthorization(options.Authorization)
            .AddUtilities(options.Utilities);
    }
}
