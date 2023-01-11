namespace JIL.Utilities;

public interface ICurrentTimestampProvider
{
    DateTimeOffset Current { get; }
}

sealed class CurrentTimestampProvider : ICurrentTimestampProvider
{
    public DateTimeOffset Current => DateTimeOffset.UtcNow;
}
