namespace JIL.Authorization.Contracts;

public sealed record UserRoleExistsError : IErrorResponse
{
    public int UserId { get; init; }
    public int RoleId { get; init; }
}