namespace JIL.Contracts;

public sealed record RoleVerificationFailedError : IErrorResponse
{
    public int Id { get; init; }
}