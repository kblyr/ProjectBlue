namespace JIL.Authorization;

static class RequestSchemaIds
{
    public const string AddUserPermission = "req://auth.jil/add-user-permission";
    public const string AddUserPermissions = "req://auth.jil/add-user-permissions";
    public const string AddUserRole = "req://auth.jil/add-user-role";
    public const string AddUserRoles = "req://auth.jil/add-user-roles";
    public const string RemoveUserPermission = "req://auth.jil/remove-user-permission";
    public const string RemoveUserPermissions = "req://auth.jil/remove-user-permissions";
    public const string RemoveUserRole = "req://auth.jil/remove-user-role";
    public const string RemoveUserRoles = "req://auth.jil/remove-user-roles";
}

static class ResponseSchemaIds
{
    public const string AddUserPermission = "res://auth.jil/add-user-permission";
    public const string AddUserPermissions = "res://auth.jil/add-user-permissions";
    public const string AddUserRole = "res://auth.jil/add-user-role";
    public const string AddUserRoles = "res://auth.jil/add-user-roles";
    public const string PermissionNotFound = "res://auth.jil/permission-not-found";
    public const string PermissionVerificationFailed = "res://auth.jil/permission-verification-failed";
    public const string RemoveUserPermission = "res://auth.jil/remove-user-permission";
    public const string RemoveUserPermissions = "res://auth.jil/remove-user-permissions";
    public const string RemoveUserRole = "res://auth.jil/remove-user-role";
    public const string RemoveUserRoles = "res://auth.jil/remove-user-roles";
    public const string RoleNotFound ="res://auth.jil/role-not-found";
    public const string RoleVerificationFailed = "res://auth.jil/role-verification-failed";
    public const string UserPermissionExists = "res://auth.jil/user-permission-exists";
    public const string UserPermissionNotFound = "res://auth.jil/user-permission-not-found";
    public const string UserRoleExists = "res://auth.jil/user-role-exists";
    public const string UserRoleNotFound = "res://auth.jil/user-role-not-found";
}