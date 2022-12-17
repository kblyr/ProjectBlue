using JIL.Authorization.Contracts;

namespace JIL.Authorization.Validators;

sealed class RemoveUserRolesValidator : Validator<RemoveUserRoles.Request>
{
    public RemoveUserRolesValidator()
    {
        RuleFor(_ => _.UserId).NotEmpty();
        RuleFor(_ => _.RoleIds).NotEmpty();
        RuleForEach(_ => _.RoleIds).NotEmpty();
    }
}