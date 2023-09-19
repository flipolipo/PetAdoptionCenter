using FluentValidation;
using SimpleWebDal.DTOs.ShelterDTOs;
using SImpleWebLogic.Validations.AddressCreateDTOValidation;
using SImpleWebLogic.Validations.CalendarCreateValidation;
using SImpleWebLogic.Validations.PetCreateDTOValidation;
using SImpleWebLogic.Validations.WebUserValidation;

namespace SImpleWebLogic.Validations.ShelterCreateDTOValidation;

public class ShelterCreateDTOValidator : AbstractValidator<ShelterCreateDTO>
{
    public ShelterCreateDTOValidator()
    {
        RuleFor(shelter => shelter.Name)
               .NotEmpty().WithMessage("Name must be provided.")
               .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");

        RuleFor(shelter => shelter.ShelterAddress)
            .SetValidator(new AddressCreateDTOValidator());

        RuleFor(shelter => shelter.ShelterDescription)
            .NotEmpty().WithMessage("Description must be provided.")
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

        RuleForEach(shelter => shelter.ShelterUsers)
            .NotEmpty().WithMessage("At least one shelter user must be provided.")
            .SetValidator(new UserCreateDTOValidator());

        RuleForEach(shelter => shelter.ShelterPets)
            .NotEmpty().WithMessage("At least one pet must be provided.")
            .SetValidator(new PetCreateDTOValidator());

        RuleFor(shelter => shelter.ShelterCalendar)
            .SetValidator(new CalendarValidator());
    }
}
