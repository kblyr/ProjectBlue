using JIL.Authorization.Contracts;

namespace JIL.Authorization.Validators;

sealed class RemoveUserPermissionValidator : Validator<RemoveUserPermission.Request>
{
    public RemoveUserPermissionValidator()
    {
        RuleFor(_ => _.UserId).NotEmpty();
        RuleFor(_ => _.PermissionId).NotEmpty();
    }
}