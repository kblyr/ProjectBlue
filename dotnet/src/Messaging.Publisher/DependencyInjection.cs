using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JIL.Messaging.Publisher;

public sealed record DependencyOptions : DependencyOptionsBase
{
    internal DependencyOptions() { }

    public FeaturesObj Features { get; } = new();

    public sealed record FeaturesObj
    {
        internal FeaturesObj() { }

        public bool BusAdapter { get; set; } = true;
        public PublishFailureHandlerObj PublishFailureHandler { get; } = new();

        public sealed record PublishFailureHandlerObj : DependencyOptionsBase
        {
            internal PublishFailureHandlerObj() { }
            public bool Log { get; set; } = true;
            public string? Directory { get; set; }
            public Func<IServiceProvider, IPublishFailureHandler>? Custom { get; set; }
        }
    }
}

public static class DependencyExtensions
{
    public static IServiceCollection AddJILMessagingPublisher(this IServiceCollection services, Action<DependencyOptions>? configure = null)
    {
        var options = new DependencyOptions();
        configure?.Invoke(options);

        if (options.IsIgnored)
        {
            return services;
        }

        if (options.Features.BusAdapter)
        {
            services.TryAddScoped<IBusAdapter, BusAdapter>();
        }

        if (options.Features.PublishFailureHandler.IsIncluded)
        {
            if (options.Features.PublishFailureHandler.Custom is not null)
            {
                services.TryAddScoped<IPublishFailureHandler>(options.Features.PublishFailureHandler.Custom);
            }
            else if (options.Features.PublishFailureHandler.Directory is not null)
            {
                services.TryAddScoped<IPublishFailureHandler>(sp => new SaveToFilePublishFailureHandler(options.Features.PublishFailureHandler.Directory));
            }
            else if (options.Features.PublishFailureHandler.Log)
            {
                services.TryAddScoped<IPublishFailureHandler, LogPublishFailureHandler>();
            }
        }

        return services;
    }
}