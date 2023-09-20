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
        RuleFor(user => user.Credentials).SetValidator(new CredentialsCreateDTOValidator());
        RuleFor(user => user.BasicInformation).SetValidator(new BasicInformationDTOValidator());
        RuleFor(user => user.UserCalendar).SetValidator(new CalendarValidator());
        RuleForEach(user => user.Roles).SetValidator(new RoleCreateDTOValidator());
        When(user => user.Adoptions != null && user.Adoptions.Any(), () =>
        {
            RuleForEach(user => user.Adoptions)
                .SetValidator(new AdoptionCreateDTOValidator());
        });
    }
}
