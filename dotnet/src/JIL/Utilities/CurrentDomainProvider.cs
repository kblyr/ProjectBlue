namespace JIL.Utilities;

public interface ICurrentDomainProvider
{
    string Current { get; }
}

sealed class CurrentDomainProvider : ICurrentDomainProvider
{
    public string Current { get; }

    public CurrentDomainProvider(string current)
    {
        Current = current;
    }
}