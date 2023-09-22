using FluentValidation;
using SimpleWebDal.DTOs.WebUserDTOs.RoleDTOs;

namespace SImpleWebLogic.Validations.WebUserValidation.RoleCreateValidations
{
    public class RoleCreateDTOValidator : AbstractValidator<RoleCreateDTO>
    {
        public RoleCreateDTOValidator()
        {
            RuleFor(role => role.RoleName)
                .NotEmpty().WithMessage("Role name cannot be empty.")
                .MaximumLength(30).WithMessage("Role name cannot exceed 30 characters.");

            //RuleForEach(role => role.Users)
            //    .SetValidator(new UserCreateDTOValidator());
        }
    }
}
