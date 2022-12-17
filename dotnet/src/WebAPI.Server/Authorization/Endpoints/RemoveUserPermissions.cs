using JIL.Authorization.Contracts;

namespace JIL.Authorization.Endpoints;

sealed class RemoveUserPermissionsEndpoint : ApiEndpoint<RemoveUserPermissions.Request, RemoveUserPermissionsCommand>
{
    public override void Configure()
    {
        Delete("user-permission/list");
        Group<AuthorizationGroup>();
    }
}