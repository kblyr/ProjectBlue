namespace JIL.Contracts;

public static class LoggedUser
{
    public sealed record Response
    {
        public required string Id { get; init; }
        public required string FullName { get; init; }
    }
}