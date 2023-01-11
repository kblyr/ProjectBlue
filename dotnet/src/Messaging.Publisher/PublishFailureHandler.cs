using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace JIL;

public interface IPublishFailureHandler
{
    ValueTask Handle<T>(T message, Exception exception, CancellationToken cancellationToken = default) where T : class;
}

sealed class LogPublishFailureHandler : IPublishFailureHandler
{
    readonly ILogger<IBusAdapter> _logger;

    public LogPublishFailureHandler(ILogger<IBusAdapter> logger)
    {
        _logger = logger;
    }

    public ValueTask Handle<T>(T message, Exception exception, CancellationToken cancellationToken = default) where T : class
    {
        if (_logger.IsEnabled(LogLevel.Error))
        {
            _logger.LogError("Failed to publish event: {event}", message);
            _logger.LogError("With Exception: {exception}", exception);
        }

        return ValueTask.CompletedTask;
    }
}

sealed class SaveToFilePublishFailureHandler : IPublishFailureHandler
{
    string _directory;

    public SaveToFilePublishFailureHandler(string directory)
    {
        _directory = directory;
    }

    public async ValueTask Handle<T>(T message, Exception exception, CancellationToken cancellationToken = default) where T : class
    {
        var messageType = typeof(T).AssemblyQualifiedName;

        if (string.IsNullOrWhiteSpace(messageType))
        {
            return;
        }

        var model = new Model
        {
            MessageType = messageType,
            Data = message,
            Error = new()
            {
                Type = exception.GetType().Name,
                Message = exception.Message
            }
        };

        try
        {
            Directory.CreateDirectory(_directory);
            await File.WriteAllTextAsync(Path.Combine(_directory, $"{DateTime.Now:yyyyMMddmmss}.json"), JsonSerializer.Serialize(model), cancellationToken);
        }
        finally { }
    }

    record Model
    {
        public string MessageType { get; init; } = "";
        public object? Data { get; init; }
        public ErrorObj Error { get; init; } = default!;

        public record ErrorObj
        {
            public string Type { get; init; } = "";
            public string Message { get; init; } = "";
        }
    }
}