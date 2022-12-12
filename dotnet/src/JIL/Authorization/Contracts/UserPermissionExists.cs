namespace JIL.Authorization.Contracts;

public sealed record UserPermissionExistsError : IErrorResponse
{
    public int UserId { get; init; }
    public int PermissionId { get; init; }
}