namespace JIL.Authorization.Contracts;

public static class AddUserRoles
{
    [SchemaId(RequestSchemaIds.AddUserRoles)]
    public sealed record Request : IApiRequest
    {
        public string UserId { get; init; } = "";
        public IEnumerable<string> RoleIds { get; init; } = Enumerable.Empty<string>();
    }

    [SchemaId(ResponseSchemaIds.AddUserRoles)]
    public sealed record Response : IApiResponse
    {
        public required IEnumerable<string> Ids { get; init; }
        public required LoggedUser.Response AddedBy { get; init; }
    }
}