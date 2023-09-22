using FluentValidation;
using SimpleWebDal.DTOs.WebUserDTOs.BasicInformationDTOs;
using SImpleWebLogic.Validations.AddressCreateDTOValidation;

namespace SImpleWebLogic.Validations.WebUserDTOValidation.BasicInformationDTOValidation;

public class BasicInformationDTOValidator : AbstractValidator<BasicInformationCreateDTO>
{
    public BasicInformationDTOValidator()
    {
        RuleFor(info => info.Name)
               .NotEmpty().WithMessage("Name cannot be empty.")
               .MaximumLength(30).WithMessage("The maximum name length is 30 characters.");

        RuleFor(info => info.Surname)
            .NotEmpty().WithMessage("Surname cannot be empty.")
            .MaximumLength(50).WithMessage("The maximum surname length is 50 characters.");

        RuleFor(info => info.Phone)
            .NotEmpty().WithMessage("Phone cannot be empty.")
            .MaximumLength(20).WithMessage("The maximum phone length is 20 characters.");

        RuleFor(info => info.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .MaximumLength(100).WithMessage("The maximum email length is 100 characters.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(info => info.Address)
            .NotNull();
           // .SetValidator(new AddressCreateDTOValidator());
    }
}
