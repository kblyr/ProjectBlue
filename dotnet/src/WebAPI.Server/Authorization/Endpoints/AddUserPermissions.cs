using JIL.Authorization.Contracts;

namespace JIL.Authorization.Endpoints;

sealed class AddUserPermissionsEndpoint : ApiEndpoint<AddUserPermissions.Request, AddUserPermissionsCommand>
{
    public override void Configure()
    {
        Post("auth/user-permission/list");
    }
}