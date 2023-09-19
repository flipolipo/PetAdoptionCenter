using FluentValidation;
using SimpleWebDal.DTOs.TemporaryHouseDTOs;
using SImpleWebLogic.Validations.AddressCreateDTOValidation;
using SImpleWebLogic.Validations.PetCreateDTOValidation;
using SImpleWebLogic.Validations.ShelterCreateDTOValidation;
using SImpleWebLogic.Validations.WebUserValidation;

namespace SImpleWebLogic.Validations.TemporaryHouseCreateDTOValidation;

public class TempHouseCreateDTOValidator : AbstractValidator<TempHouseCreateDTO>
{
    public TempHouseCreateDTOValidator()
    {
        RuleFor(tempHouse => tempHouse.UserId)
               .NotEmpty().WithMessage("User ID must be provided.");

        RuleFor(tempHouse => tempHouse.TemporaryOwner)
            .SetValidator(new UserCreateDTOValidator());

        RuleFor(tempHouse => tempHouse.AddressId)
            .NotEmpty().WithMessage("Address ID must be provided.");

        RuleFor(tempHouse => tempHouse.TemporaryHouseAddress)
            .SetValidator(new AddressCreateDTOValidator());

        RuleForEach(tempHouse => tempHouse.PetsInTemporaryHouse)
            .NotEmpty().WithMessage("At least one pet must be provided.")
            .SetValidator(new PetCreateDTOValidator());

        RuleFor(tempHouse => tempHouse.ShelterId)
            .NotEmpty().WithMessage("Shelter ID must be provided.");

        RuleFor(tempHouse => tempHouse.ShelterName)
            .SetValidator(new ShelterCreateDTOValidator());

        RuleFor(tempHouse => tempHouse.StartOfTemporaryHouseDate)
            .NotEmpty().WithMessage("Start date of temporary house must be provided.");
    }
}
