using JIL.Authorization.Converters;
using Mapster;

namespace JIL.Authorization.Contracts;

sealed class Mapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<AddUserPermission.Request, AddUserPermissionCommand>()
            .Map(dest => dest.UserId, src => MapConverters.UserId.Convert(src.UserId))
            .Map(dest => dest.PermissionId, src => MapConverters.PermissionId.Convert(src.PermissionId));

        config.ForType<AddUserPermissionCommand.Response, AddUserPermission.Response>()
            .Map(dest => dest.Id, src => MapConverters.UserPermissionId.Convert(src.Id));

        config.ForType<AddUserPermissions.Request, AddUserPermissionsCommand>()
            .Map(dest => dest.UserId, src => MapConverters.UserId.Convert(src.UserId))
            .Map(dest => dest.PermissionIds, src => MapConverters.PermissionId.Convert(src.PermissionIds));

        config.ForType<AddUserPermissionsCommand.Response, AddUserPermissions.Response>()
            .Map(dest => dest.Ids, src => MapConverters.UserPermissionId.Convert(src.Ids));

        config.ForType<AddUserRole.Request, AddUserRoleCommand>()
            .Map(dest => dest.UserId, src => MapConverters.UserId.Convert(src.UserId))
            .Map(dest => dest.RoleId, src => MapConverters.RoleId.Convert(src.RoleId));

        config.ForType<AddUserRoleCommand.Response, AddUserRole.Response>()
            .Map(dest => dest.Id, src => MapConverters.UserRoleId.Convert(src.Id));

        config.ForType<AddUserRoles.Request, AddUserRolesCommand>()
            .Map(dest => dest.UserId, src => MapConverters.UserId.Convert(src.UserId))
            .Map(dest => dest.RoleIds, src => MapConverters.RoleId.Convert(src.RoleIds));

        config.ForType<AddUserRolesCommand.Response, AddUserRoles.Response>()
            .Map(dest => dest.Ids, src => MapConverters.UserRoleId.Convert(src.Ids));
        
        config.ForType<PermissionNotFoundError, PermissionNotFound.Response>()
            .Map(dest => dest.Id, src => MapConverters.PermissionId.Convert(src.Id));

        config.ForType<PermissionVerificationFailedError, PermissionVerificationFailed.Response>()
            .Map(dest => dest.Id, src => MapConverters.PermissionId.Convert(src.Id));
        
        config.ForType<RemoveUserPermission.Request, RemoveUserPermissionCommand>()
            .Map(dest => dest.UserId, src => MapConverters.UserId.Convert(src.UserId))
            .Map(dest => dest.PermissionId, src => MapConverters.PermissionId.Convert(src.PermissionId));

        config.ForType<RemoveUserPermissionCommand.Response, RemoveUserPermission.Response>()
            .Map(dest => dest.Id, src => MapConverters.UserPermissionId.Convert(src.Id));

        config.ForType<RemoveUserPermissions.Request, RemoveUserPermissionsCommand>()
            .Map(dest => dest.UserId, src => MapConverters.UserId.Convert(src.UserId))
            .Map(dest => dest.PermissionIds, src => MapConverters.PermissionId.Convert(src.PermissionIds));

        config.ForType<RemoveUserPermissionsCommand.Response, RemoveUserPermissions.Response>()
            .Map(dest => dest.Ids, src => MapConverters.UserPermissionId.Convert(src.Ids));

        config.ForType<RemoveUserRole.Request, RemoveUserRoleCommand>()
            .Map(dest => dest.UserId, src => MapConverters.UserId.Convert(src.UserId))
            .Map(dest => dest.RoleId, src => MapConverters.RoleId.Convert(src.RoleId));

        config.ForType<RemoveUserRoleCommand.Response, RemoveUserRole.Response>()
            .Map(dest => dest.Id, src => MapConverters.UserRoleId.Convert(src.Id));

        config.ForType<RemoveUserRoles.Request, RemoveUserRolesCommand>()
            .Map(dest => dest.UserId, src => MapConverters.UserId.Convert(src.UserId))
            .Map(dest => dest.RoleIds, src => MapConverters.RoleId.Convert(src.RoleIds));

        config.ForType<RemoveUserRolesCommand.Response, RemoveUserRoles.Response>()
            .Map(dest => dest.Ids, src => MapConverters.UserRoleId.Convert(src.Ids));

        config.ForType<RoleNotFoundError, RoleNotFound.Response>()
            .Map(dest => dest.Id, src => MapConverters.RoleId.Convert(src.Id));

        config.ForType<RoleVerificationFailedError, RoleVerificationFailed.Response>()
            .Map(dest => dest.Id, src => MapConverters.RoleId.Convert(src.Id));

        config.ForType<UserPermissionExistsError, UserPermissionExists.Response>()
            .Map(dest => dest.UserId, src => MapConverters.UserId.Convert(src.UserId))
            .Map(dest => dest.PermissionId, src => MapConverters.PermissionId.Convert(src.PermissionId));

        config.ForType<UserPermissionNotFoundError, UserPermissionNotFound.Response>()
            .Map(dest => dest.UserId, src => MapConverters.UserId.Convert(src.UserId))
            .Map(dest => dest.PermissionId, src => MapConverters.PermissionId.Convert(src.PermissionId));

        config.ForType<UserRoleExistsError, UserRoleExists.Response>()
            .Map(dest => dest.UserId, src => MapConverters.UserId.Convert(src.UserId))
            .Map(dest => dest.RoleId, src => MapConverters.RoleId.Convert(src.RoleId));

        config.ForType<UserRoleNotFoundError, UserRoleNotFound.Response>()
            .Map(dest => dest.UserId, src => MapConverters.UserId.Convert(src.UserId))
            .Map(dest => dest.RoleId, src => MapConverters.RoleId.Convert(src.RoleId));
    }
}