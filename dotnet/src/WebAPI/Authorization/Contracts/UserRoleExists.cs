namespace JIL.Authorization.Contracts;

public static class UserRoleExists
{
    [SchemaId(ResponseSchemaIds.UserRoleExists)]
    public sealed record Response : IApiErrorResponse
    {
        public required string UserId { get; init; }
        public required string RoleId { get; init; }
    }
}