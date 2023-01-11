namespace JIL.Authorization.Contracts;

public sealed record PermissionNotFoundError : IErrorResponse
{
    public int Id { get; init; }
}