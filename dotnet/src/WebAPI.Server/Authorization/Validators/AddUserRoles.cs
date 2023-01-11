using System.Data;
using JIL.Authorization.Contracts;

namespace JIL.Authorization.Validators;

sealed class AddUserRolesValidator : Validator<AddUserRoles.Request>
{
    public AddUserRolesValidator()
    {
        RuleFor(_ => _.UserId).NotEmpty();
        RuleFor(_ => _.RoleIds).NotEmpty();
        RuleForEach(_ => _.RoleIds).NotEmpty();
    }
}