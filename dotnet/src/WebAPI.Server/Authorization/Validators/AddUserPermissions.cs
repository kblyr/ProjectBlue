using JIL.Authorization.Contracts;

namespace JIL.Authorization.Validators;

sealed class AddUserPermissionsValidator : Validator<AddUserPermissions.Request>
{
    public AddUserPermissionsValidator()
    {
        RuleFor(_ => _.UserId).NotEmpty();
        RuleFor(_ => _.PermissionIds).NotEmpty();
        RuleForEach(_ => _.PermissionIds).NotEmpty();
    }
}