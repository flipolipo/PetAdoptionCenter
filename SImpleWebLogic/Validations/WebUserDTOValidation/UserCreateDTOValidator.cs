using FluentValidation;
using SimpleWebDal.DTOs.WebUserDTOs;

namespace SImpleWebLogic.Validations.WebUserValidation.RoleCreateValidations
{
    public class UserCreateDTOValidator : AbstractValidator<UserCreateDTO>
    {
        public UserCreateDTOValidator()
        {
            RuleFor(user => user.Roles)
                .NotNull() 
                .ForEach(role =>
                {
                    role.SetValidator(new RoleCreateDTOValidator()); 
                });
        }
    }
}
