namespace JIL.Accounts.Contracts;

public static class CreateUser
{
    [SchemaId(RequestSchemaIds.CreateUser)]
    public sealed record Request : IApiRequest
    {
        public string FirstName { get; init; } = "";
        public string LastName { get; init; } = "";
        public string Username { get; init; } = "";
        public string CipherPassword { get; init; } = "";
    }

    [SchemaId(ResponseSchemaIds.CreateUser)]
    public sealed record Response : IApiResponse
    {
        public required string Id { get; init; }
    }
}