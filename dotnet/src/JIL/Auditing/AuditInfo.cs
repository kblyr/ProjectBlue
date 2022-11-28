namespace JIL.Auditing;

public sealed record AuditInfo
{
    public int UserId { get; init; }
    public required string Username { get; init; }
    public DateTime Timestamp { get; init; }
}