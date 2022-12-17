namespace JIL.Authorization.Contracts;

public static class RoleVerificationFailed
{
    [SchemaId(ResponseSchemaIds.RoleVerificationFailed)]
    public sealed record Response : IApiErrorResponse
    {
        public required string Id { get; init; }
    }
}