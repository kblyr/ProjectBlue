namespace JIL.Authorization.Contracts;

public static class UserPermissionExists
{
    [SchemaId(ResponseSchemaIds.UserPermissionExists)]
    public sealed record Response : IApiErrorResponse
    {
        public required string UserId { get; init; }
        public required string PermissionId { get; init; }
    }
}