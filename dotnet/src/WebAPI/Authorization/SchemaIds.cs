namespace JIL.Authorization;

static class RequestSchemaIds
{
    public const string AddUserPermission = "req://authorization.jil/add-user-permission";
    public const string AddUserPermissions = "req://authorization.jil/add-user-permissions";
    public const string AddUserRole = "req://authorization.jil/add-user-role";
    public const string AddUserRoles = "req://authorization.jil/add-user-roles";
    public const string RemoveUserPermission = "req://authorization.jil/remove-user-permission";
    public const string RemoveUserPermissions = "req://authorization.jil/remove-user-permissions";
    public const string RemoveUserRole = "req://authorization.jil/remove-user-role";
    public const string RemoveUserRoles = "req://authorization.jil/remove-user-roles";
}

static class ResponseSchemaIds
{
    public const string AddUserPermission = "res://authorization.jil/add-user-permission";
    public const string AddUserPermissions = "res://authorization.jil/add-user-permissions";
    public const string AddUserRole = "res://authorization.jil/add-user-role";
    public const string AddUserRoles = "res://authorization.jil/add-user-roles";
    public const string PermissionNotFound = "res://authorization.jil/permission-not-found";
    public const string PermissionVerificationFailed = "res://authorization.jil/permission-verification-failed";
    public const string RemoveUserPermission = "res://authorization.jil/remove-user-permission";
    public const string RemoveUserPermissions = "res://authorization.jil/remove-user-permissions";
    public const string RemoveUserRole = "res://authorization.jil/remove-user-role";
    public const string RemoveUserRoles = "res://authorization.jil/remove-user-roles";
    public const string RoleNotFound ="res://authorization.jil/role-not-found";
    public const string RoleVerificationFailed = "res://authorization.jil/role-verification-failed";
    public const string UserPermissionExists = "res://authorization.jil/user-permission-exists";
    public const string UserPermissionNotFound = "res://authorization.jil/user-permission-not-found";
    public const string UserRoleExists = "res://authorization.jil/user-role-exists";
    public const string UserRoleNotFound = "res://authorization.jil/user-role-not-found";
}