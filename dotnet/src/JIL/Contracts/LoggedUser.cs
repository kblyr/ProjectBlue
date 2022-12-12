namespace JIL.Contracts;

public sealed record LoggedUser
{
    public int Id { get; init; }
    public required string FullName { get; init; }
}