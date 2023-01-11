namespace JIL.Authorization.Contracts;

public static class RemoveUserRoles
{
    [SchemaId(RequestSchemaIds.RemoveUserRoles)]
    public sealed record Request : IApiRequest
    {
        public string UserId { get; init; } = "";
        public IEnumerable<string> RoleIds { get; init; } = Enumerable.Empty<string>();
    }

    [SchemaId(ResponseSchemaIds.RemoveUserRoles)]
    public sealed record Response : IApiResponse
    {
        public required IEnumerable<string> Ids { get; init; }
        public required LoggedUser.Response RemovedBy { get; init; }
    }
}