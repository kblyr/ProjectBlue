using JIL.Authorization.Contracts;

namespace JIL.Authorization.Endpoints;

sealed class RemoveUserRolesEndpoint : ApiEndpoint<RemoveUserRoles.Request,  RemoveUserRolesCommand>
{
    public override void Configure()
    {
        Delete("auth/user-role/list");
    }
}