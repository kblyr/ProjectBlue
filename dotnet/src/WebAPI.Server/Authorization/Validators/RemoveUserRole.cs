using JIL.Authorization.Contracts;

namespace  JIL.Authorization.Validators;

sealed class RemoveUserRoleValidator : Validator<RemoveUserRole.Request>
{
    public RemoveUserRoleValidator()
    {
        RuleFor(_ => _.UserId).NotEmpty();
        RuleFor(_ => _.RoleId).NotEmpty();
    }
}