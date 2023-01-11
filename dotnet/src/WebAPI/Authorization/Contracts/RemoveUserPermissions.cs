namespace JIL.Authorization.Contracts;

public static class RemoveUserPermissions
{
    [SchemaId(RequestSchemaIds.RemoveUserPermissions)]
    public sealed record Request : IApiRequest
    {
        public string UserId { get; init; } = "";
        public IEnumerable<string> PermissionIds { get; init; } = Enumerable.Empty<string>();
    }

    [SchemaId(ResponseSchemaIds.RemoveUserPermissions)]
    public sealed record Response : IApiResponse
    {
        public required IEnumerable<string> Ids { get; init; }
        public required LoggedUser.Response RemovedBy { get; init; }
    }
}