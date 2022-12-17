using JIL.Authorization.Contracts;

namespace JIL.Authorization.Endpoints;

sealed class RemoveUserPermissionEndpoint : ApiEndpoint<RemoveUserPermission.Request, RemoveUserPermissionCommand>
{
    public override void Configure()
    {
        Delete("auth/user-permission");
    }
}