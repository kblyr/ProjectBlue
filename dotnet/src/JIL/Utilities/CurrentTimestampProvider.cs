namespace JIL.Utilities;

public interface ICurrentTimestampProvider
{
    DateTimeOffset Current { get; }
}