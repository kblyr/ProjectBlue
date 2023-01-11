namespace JIL.Authorization.Contracts;

public static class UserPermissionNotFound
{
    [SchemaId(ResponseSchemaIds.UserPermissionNotFound)]
    public sealed record Response : IApiErrorResponse
    {
        public required string UserId { get; init; }
        public required string PermissionId { get; init; }
    }
}