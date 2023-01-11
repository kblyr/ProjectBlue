namespace JIL.Authorization.Contracts;

public static class PermissionNotFound
{
    [SchemaId(ResponseSchemaIds.PermissionNotFound)]
    public sealed record Response : IApiErrorResponse
    {
        public required string Id { get; init; }
    }
}