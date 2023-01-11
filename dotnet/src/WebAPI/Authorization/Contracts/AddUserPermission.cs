namespace JIL.Authorization.Contracts;

public static class AddUserPermission
{
    [SchemaId(RequestSchemaIds.AddUserPermission)]
    public sealed record Request : IApiRequest
    {
        public string UserId { get; init; } = "";
        public string PermissionId { get; init; } = "";
    }

    [SchemaId(ResponseSchemaIds.AddUserPermission)]
    public sealed record Response : IApiResponse
    {
        public required string Id { get; init; }
        public required LoggedUserResponse AddedBy { get; init; }
    }
}