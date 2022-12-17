namespace JIL.Authorization.Contracts;

public static class RemoveUserRole
{
    [SchemaId(RequestSchemaIds.RemoveUserRole)]
    public sealed record Request : IApiRequest
    {
        public required string UserId { get; init; }
        public required string RoleId { get; init; }
    }

    [SchemaId(ResponseSchemaIds.RemoveUserRole)]
    public sealed record Response : IApiResponse
    {
        public required string Id { get; init; }
        public required LoggedUser.Response RemovedBy { get; init; }
    }
}