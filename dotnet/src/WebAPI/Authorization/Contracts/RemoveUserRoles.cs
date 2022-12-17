namespace JIL.Authorization.Contracts;

public static class RemoveUserRoles
{
    [SchemaId(RequestSchemaIds.RemoveUserRoles)]
    public sealed record Request : IApiRequest
    {
        public required string UserId { get; init; }
        public required IEnumerable<string> RoleIds { get; init; }
    }

    [SchemaId(ResponseSchemaIds.RemoveUserRoles)]
    public sealed record Response : IApiResponse
    {
        public required IEnumerable<string> Ids { get; init; }
        public required LoggedUser.Response RemovedBy { get; init; }
    }
}