namespace JIL.Authorization.Contracts;

public static class AddUserPermission
{
    [SchemaId(RequestSchemaIds.AddUserPermission)]
    public sealed record Request : IApiRequest
    {
        public required string UserId { get; init; }
        public required string PermissionId { get; init; }
    }

    [SchemaId(ResponseSchemaIds.AddUserPermission)]
    public sealed record Response : IApiResponse
    {
        public required string Id { get; init; }
        public required LoggedUserResponse AddedBy { get; init; }
    }
}