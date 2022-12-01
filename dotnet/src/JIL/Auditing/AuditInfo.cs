namespace JIL.Auditing;

public sealed record AuditInfo
{
    public required UserObj User { get; init; }
    public DateTime Timestamp { get; init; }

    public sealed record UserObj
    {
        public int Id { get; init; }
        public required string Username { get; init; }
        public required string FullName { get; init; }
    }
}