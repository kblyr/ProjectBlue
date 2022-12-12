namespace JIL.Authorization.Contracts;

public sealed record UserRoleNotFoundError : IErrorResponse
{
    public int UserId { get; init; }
    public int RoleId { get; init; }
}