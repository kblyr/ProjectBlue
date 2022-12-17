using JIL.Authorization.Contracts;

namespace JIL.Authorization.Validators;

sealed class RemoveUserPermissionsValidator : Validator<RemoveUserPermissions.Request>
{
    public RemoveUserPermissionsValidator()
    {
        RuleFor(_ => _.UserId).NotEmpty();
        RuleFor(_ => _.PermissionIds).NotEmpty();
        RuleForEach(_ => _.PermissionIds).NotEmpty();
    }
}