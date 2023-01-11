namespace JIL.Authorization.Contracts;

public sealed record RoleVerificationFailedError : IErrorResponse
{
    public int Id { get; init; }
}