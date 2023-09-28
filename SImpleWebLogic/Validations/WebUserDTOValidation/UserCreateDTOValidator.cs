using FluentValidation;
using SimpleWebDal.DTOs.WebUserDTOs;
using SImpleWebLogic.Validations.AdoptionCreateDTOValidation;
using SImpleWebLogic.Validations.CalendarCreateValidation;
using SImpleWebLogic.Validations.WebUserDTOValidation.BasicInformationDTOValidation;
using SImpleWebLogic.Validations.WebUserValidation.CredentialsCreateValidations;
using SImpleWebLogic.Validations.WebUserValidation.RoleCreateValidations;

namespace SImpleWebLogic.Validations.WebUserValidation;

public class UserCreateDTOValidator : AbstractValidator<UserCreateDTO>
{
    public UserCreateDTOValidator()
    {
        RuleFor(user => user.Credentials).NotNull();
        RuleFor(user => user.BasicInformation).NotNull();
        RuleFor(user => user.UserCalendar).NotNull();
        RuleForEach(user => user.Roles).NotNull();
    }
}
