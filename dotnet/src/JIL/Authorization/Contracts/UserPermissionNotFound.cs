namespace JIL.Authorization.Contracts;

public sealed record UserPermissionNotFoundError : IErrorResponse
{
    public int UserId { get; init; }
    public int PermissionId { get; init; }
}