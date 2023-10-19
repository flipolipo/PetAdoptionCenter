using FluentValidation;
using SimpleWebDal.DTOs.ShelterDTOs;

namespace SImpleWebLogic.Validations.ShelterCreateDTOValidation;

public class ShelterCreateDTOValidator : AbstractValidator<ShelterCreateDTO>
{
    public ShelterCreateDTOValidator()
    {
        RuleFor(shelter => shelter.Name)
      .NotEmpty().WithMessage("Name cannot be empty.")
      .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");

        RuleFor(shelter => shelter.ShelterAddress)
            .NotNull().WithMessage("ShelterAddress cannot be null.");

        RuleFor(shelter => shelter.ShelterDescription)
            .MaximumLength(500).WithMessage("ShelterDescription cannot exceed 500 characters.");
    }
}
