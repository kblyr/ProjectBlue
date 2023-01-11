namespace JIL.Authorization.Contracts;

public static class AddUserPermissions
{
    [SchemaId(RequestSchemaIds.AddUserPermissions)]
    public sealed record Request : IApiRequest
    {
        public string UserId { get; init; } = "";
        public IEnumerable<string> PermissionIds { get; init; } = Enumerable.Empty<string>();
    }

    [SchemaId(ResponseSchemaIds.AddUserPermissions)]
    public sealed record Response : IApiResponse
    {
        public required IEnumerable<string> Ids { get; init; }
        public required LoggedUser.Response AddedBy { get; init; }
    }
}