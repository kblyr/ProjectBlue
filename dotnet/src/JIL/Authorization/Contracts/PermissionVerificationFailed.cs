namespace JIL.Authorization.Contracts;

public sealed record PermissionVerificationFailedError : IErrorResponse
{
    public int Id { get; init; }
}