using JIL.Authorization.Contracts;

namespace JIL.Authorization.Endpoints;

sealed class AddUserPermissionEndpoint : ApiEndpoint<AddUserPermission.Request, AddUserPermissionCommand>
{
    public override void Configure()
    {
        Post("user-permission");
        Group<AuthorizationGroup>();
    }
}