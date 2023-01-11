namespace JIL.Authorization.Contracts;

public static class PermissionVerificationFailed
{
    [SchemaId(ResponseSchemaIds.PermissionVerificationFailed)]
    public sealed class Response : IApiErrorResponse
    {
        public required string Id { get; init; }
    }
}