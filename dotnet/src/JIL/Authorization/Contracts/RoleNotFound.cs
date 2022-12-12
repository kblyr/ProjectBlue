namespace JIL.Authorization.Contracts;

public sealed record RoleNotFoundError : IErrorResponse
{
    public int Id { get; init; }
}