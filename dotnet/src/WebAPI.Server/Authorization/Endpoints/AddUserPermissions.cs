using JIL.Authorization.Contracts;

namespace JIL.Authorization.Endpoints;

sealed class AddUserPermissionsEndpoint : ApiEndpoint<AddUserPermissions.Request, AddUserPermissionsCommand>
{
    public override void Configure()
    {
        Post("user-permission/list");
        Group<AuthorizationGroup>();
    }
}