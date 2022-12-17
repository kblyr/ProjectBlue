using JIL.Authorization.Contracts;

namespace JIL.Authorization.Validators;

sealed class AddUserRoleValidator : Validator<AddUserRole.Request>
{
    public AddUserRoleValidator()
    {
        RuleFor(_ => _.UserId).NotEmpty();
        RuleFor(_ => _.RoleId).NotEmpty();
    }
}