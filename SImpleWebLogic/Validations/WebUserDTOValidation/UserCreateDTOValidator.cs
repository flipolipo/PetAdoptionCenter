using FluentValidation;
using SimpleWebDal.DTOs.WebUserDTOs;


namespace SImpleWebLogic.Validations.WebUserValidation;

public class UserCreateDTOValidator : AbstractValidator<UserCreateDTO>
{
    public UserCreateDTOValidator()
    {
     
        RuleFor(user => user.BasicInformation).NotNull();
        RuleFor(user => user.UserCalendar).NotNull();
        RuleForEach(user => user.Roles).NotNull();
    }
}
