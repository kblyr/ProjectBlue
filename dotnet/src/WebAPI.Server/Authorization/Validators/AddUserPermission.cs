using JIL.Authorization.Contracts;

namespace JIL.Authorization.Validators;

sealed class AddUserPermissionValidator : Validator<AddUserPermission.Request>
{
    public AddUserPermissionValidator()
    {
        RuleFor(_ => _.UserId).NotEmpty();
        RuleFor(_ => _.PermissionId).NotEmpty();
    }
}