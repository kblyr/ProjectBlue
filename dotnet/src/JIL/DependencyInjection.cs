using JIL.Authorization;
using JIL.Utilities;

namespace JIL;

public sealed record DependencyOptions
{
    internal DependencyOptions() { }
    
    public Authorization.DependencyOptions? Authorization { get; set; } = new();
    public Utilities.DependencyOptions? Utilities { get; set; } = new();
}

public static class DependencyExtensions
{
    public static IServiceCollection AddJIL(this IServiceCollection services, Action<DependencyOptions>? configure = null)
    {
        var options = new DependencyOptions();
        configure?.Invoke(options);

        if (options.Authorization is not null)
        {
            services.AddAuthorization(options.Authorization);
        }

        if (options.Utilities is not null)
        {
            services.AddUtilities(options.Utilities);
        }

        return services;
    }
}
