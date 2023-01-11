namespace JIL.Accounts.Contracts;

public static class UsernameAlreadyExists
{
    [SchemaId(ResponseSchemaIds.UsernameAlreadyExists)]
    public sealed record Response : IApiErrorResponse
    {
        public required string Username { get; init; }
    }
}