using JIL.Authorization.Contracts;

namespace JIL.Authorization.Endpoints;

sealed class AddUserRolesEndpoint : ApiEndpoint<AddUserRoles.Request, AddUserRolesCommand>
{
    public override void Configure()
    {
        Post("auth/user-role/list");
    }
}