using FluentValidation;
using SimpleWebDal.DTOs.AdoptionDTOs;
using SImpleWebLogic.Validations.PetCreateDTOValidation;
using SImpleWebLogic.Validations.ShelterCreateDTOValidation;
using SImpleWebLogic.Validations.WebUserValidation;

namespace SImpleWebLogic.Validations.AdoptionCreateDTOValidation;

public class AdoptionCreateDTOValidator : AbstractValidator<AdoptionCreateDTO>
{
    public AdoptionCreateDTOValidator()
    {
        RuleFor(adoption => adoption.AdoptedPet)
              .NotNull().WithMessage("Adopted pet must be provided.")
              .SetValidator(new PetCreateDTOValidator());

        RuleFor(adoption => adoption.Shelter)
            .NotNull().WithMessage("Shelter must be provided.")
            .SetValidator(new ShelterCreateDTOValidator());

        RuleFor(adoption => adoption.Adopter)
            .NotNull().WithMessage("Adopter must be provided.")
            .SetValidator(new UserCreateDTOValidator());

        RuleFor(adoption => adoption.DateOfAdoption)
            .NotEmpty().WithMessage("Date of adoption must be provided.");
    }
}
