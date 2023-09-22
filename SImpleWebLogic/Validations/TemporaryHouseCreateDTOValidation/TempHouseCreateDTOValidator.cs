using FluentValidation;
using SimpleWebDal.DTOs.TemporaryHouseDTOs;
using SImpleWebLogic.Validations.AddressCreateDTOValidation;
using SImpleWebLogic.Validations.PetCreateDTOValidation;
using SImpleWebLogic.Validations.WebUserValidation;

namespace SImpleWebLogic.Validations.TemporaryHouseCreateDTOValidation;

public class TempHouseCreateDTOValidator : AbstractValidator<TempHouseCreateDTO>
{
    public TempHouseCreateDTOValidator()
    {
        RuleFor(tempHouse => tempHouse.TemporaryOwner)
                .NotNull().WithMessage("TemporaryOwner cannot be null.");
        //.SetValidator(new UserCreateDTOValidator());

        RuleFor(tempHouse => tempHouse.TemporaryHouseAddress)
            .NotNull().WithMessage("TemporaryHouseAddress cannot be null.");
            //.SetValidator(new AddressCreateDTOValidator());

        When(tempHouse => tempHouse.PetsInTemporaryHouse != null, () =>
        {
            RuleFor(tempHouse => tempHouse.PetsInTemporaryHouse)
            .NotNull();
                //.Must(pets => pets.Any()).WithMessage("PetsInTemporaryHouse must contain at least one element.");
                //.ForEach(pet => pet.SetValidator(new PetCreateDTOValidator()));
        });

        RuleFor(tempHouse => tempHouse.StartOfTemporaryHouseDate)
            .NotEmpty().WithMessage("StartOfTemporaryHouseDate cannot be empty.")
            .Must(date => date > DateTime.Now).WithMessage("StartOfTemporaryHouseDate must be in the future.");
    }
}

