using FluentValidation;
using SimpleWebDal.DTOs.WebUserDTOs.RoleDTOs;

namespace SImpleWebLogic.Validations.WebUserValidation.RoleCreateValidations
{
    public class RoleCreateDTOValidator : AbstractValidator<RoleCreateDTO>
    {
        public RoleCreateDTOValidator()
        {
            RuleFor(role => role.Title)
             .IsInEnum();
        }
    }
}
