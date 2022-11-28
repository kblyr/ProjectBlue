namespace JIL.Accounts.Contracts;

public sealed record UsernameAlreadyExistsError : IErrorResponse
{
    public required string Username { get; init; }
}