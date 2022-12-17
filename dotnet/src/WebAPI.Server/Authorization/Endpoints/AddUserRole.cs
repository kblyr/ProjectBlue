using JIL.Authorization.Contracts;

namespace JIL.Authorization.Endpoints;

sealed class AddUserRoleEndpoint : ApiEndpoint<AddUserRole.Request, AddUserRoleCommand>
{
    public override void Configure()
    {
        Post("auth/user-role");
    }
}