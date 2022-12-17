namespace JIL.Authorization.Contracts;

public static class UserRoleNotFound
{
    [SchemaId(ResponseSchemaIds.UserRoleNotFound)]
    public sealed record Response : IApiErrorResponse
    {
        public required string UserId { get; init; }
        public required string RoleId { get; init; }
    }
}