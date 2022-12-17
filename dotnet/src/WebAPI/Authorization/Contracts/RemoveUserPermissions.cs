namespace JIL.Authorization.Contracts;

public static class RemoveUserPermissions
{
    [SchemaId(RequestSchemaIds.RemoveUserPermissions)]
    public sealed record Request : IApiRequest
    {
        public required string UserId { get; init; }
        public required IEnumerable<string> PermissionIds { get; init; }
    }

    [SchemaId(ResponseSchemaIds.RemoveUserPermissions)]
    public sealed record Response : IApiResponse
    {
        public required IEnumerable<string> Ids { get; init; }
        public required LoggedUser.Response RemovedBy { get; init; }
    }
}