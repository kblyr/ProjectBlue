using JIL.Authorization.Contracts;

namespace JIL.Authorization.Endpoints;

sealed class RemoveUserRoleEndpoint : ApiEndpoint<RemoveUserRole.Request, RemoveUserRoleCommand>
{
    public override void Configure()
    {
        Delete("auth/user-role");
    }
}