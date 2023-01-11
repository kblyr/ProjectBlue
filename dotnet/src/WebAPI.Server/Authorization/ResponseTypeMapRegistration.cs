using JIL.Authorization.Contracts;

namespace JIL.Authorization;

sealed class ResponseTypeMapRegistration : IResponseTypeMapRegistration
{
    public void Register(IResponseTypeMapRegistry registry)
    {
        registry
            .RegisterCreated<AddUserPermissionCommand.Response, AddUserPermission.Response>()
            .RegisterCreated<AddUserPermissionsCommand.Response, AddUserPermissions.Response>()
            .RegisterCreated<AddUserRoleCommand.Response, AddUserRole.Response>()
            .RegisterCreated<AddUserRolesCommand.Response, AddUserRoles.Response>()
            .RegisterNotFound<PermissionNotFoundError, PermissionNotFound.Response>()
            .RegisterUnauthorized<PermissionVerificationFailedError, PermissionVerificationFailed.Response>()
            .RegisterOK<RemoveUserPermissionCommand.Response, RemoveUserPermission.Response>()
            .RegisterOK<RemoveUserPermissionsCommand.Response, RemoveUserPermissions.Response>()
            .RegisterOK<RemoveUserRoleCommand.Response, RemoveUserRole.Response>()
            .RegisterOK<RemoveUserRolesCommand.Response, RemoveUserRoles.Response>()
            .RegisterNotFound<RoleNotFoundError, RoleNotFound.Response>()
            .RegisterUnauthorized<RoleVerificationFailedError, RoleVerificationFailed.Response>()
            .RegisterBadRequest<UserPermissionExistsError, UserPermissionExists.Response>()
            .RegisterNotFound<UserPermissionNotFoundError, UserPermissionNotFound.Response>()
            .RegisterBadRequest<UserRoleExistsError, UserRoleExists.Response>()
            .RegisterNotFound<UserRoleNotFoundError, UserRoleNotFound.Response>()
            ;
    }
}
