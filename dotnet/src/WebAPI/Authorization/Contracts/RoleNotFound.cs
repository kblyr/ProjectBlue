namespace JIL.Authorization.Contracts;

public static class RoleNotFound
{
    [SchemaId(ResponseSchemaIds.RoleNotFound)]
    public sealed record Response : IApiErrorResponse
    {
        public required string Id { get; init; }
    }
}