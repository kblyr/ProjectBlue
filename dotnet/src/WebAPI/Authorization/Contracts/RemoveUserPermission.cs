namespace JIL.Authorization.Contracts;

public static class RemoveUserPermission
{
    [SchemaId(RequestSchemaIds.RemoveUserPermission)]
    public sealed record Request : IApiRequest
    {
        public required string UserId { get; init; }
        public required string PermissionId { get; init; }
    }

    [SchemaId(ResponseSchemaIds.RemoveUserPermission)]
    public sealed record Response : IApiResponse
    {
        public required string Id { get; init; }
        public required LoggedUser.Response RemovedBy { get; init; }
    }
}