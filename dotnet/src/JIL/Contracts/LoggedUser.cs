namespace JIL.Contracts;

public sealed record LoggedUserResponse
{
    public int Id { get; init; }
    public required string FullName { get; init; }
}