namespace JIL.Authorization.Contracts;

public static class AddUserRole
{
    [SchemaId(RequestSchemaIds.AddUserRole)]
    public sealed record Request : IApiRequest
    {
        public required string UserId { get; init; }
        public required string RoleId { get; init; }
    }

    [SchemaId(ResponseSchemaIds.AddUserRole)]
    public sealed record Response : IApiResponse
    {
        public required string Id { get; init; }
        public required LoggedUser.Response AddedBy { get; init; }
    }
}