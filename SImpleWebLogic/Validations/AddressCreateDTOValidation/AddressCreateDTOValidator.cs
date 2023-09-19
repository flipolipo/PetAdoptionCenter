using FluentValidation;
using SimpleWebDal.DTOs.AddressDTOs;

namespace SImpleWebLogic.Validations.AddressCreateDTOValidation;

public class AddressCreateDTOValidator : AbstractValidator<AddressCreateDTO>
{
    public AddressCreateDTOValidator()
    {
        RuleFor(address => address.Street)
            .NotEmpty().WithMessage("The street cannot be empty.")
            .MaximumLength(50).WithMessage("The maximum street length is 50 characters.");

        RuleFor(address => address.HouseNumber)
            .NotEmpty().WithMessage("The house number cannot be empty.")
            .MaximumLength(10).WithMessage("The maximum length of the house number is 10 characters.");

        RuleFor(address => address.FlatNumber)
           .Cascade(CascadeMode.Stop)
           .GreaterThanOrEqualTo(0).WithMessage("The apartment number must be non-negative.")
           .When(address => address.FlatNumber.HasValue);

        RuleFor(address => address.PostalCode)
            .NotEmpty().WithMessage("Postal code cannot be empty.")
            .Matches(@"^\d{2}-\d{3}$").WithMessage("Incorrect postal code format (XX-XXX).");

        RuleFor(address => address.City)
            .NotEmpty().WithMessage("The city cannot be empty.")
            .MaximumLength(50).WithMessage("The maximum length of the city is 50 characters.");
    }
}
