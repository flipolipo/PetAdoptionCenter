using FluentValidation;
using SimpleWebDal.DTOs.TemporaryHouseDTOs;

namespace SImpleWebLogic.Validations.TemporaryHouseCreateDTOValidation;

public class TempHouseCreateDTOValidator : AbstractValidator<TempHouseCreateDTO>
{
    public TempHouseCreateDTOValidator()
    {
        //RuleFor(tempHouse => tempHouse.TemporaryOwner)
        //        .NotNull().WithMessage("TemporaryOwner cannot be null.");
        ////.SetValidator(new UserCreateDTOValidator());

        //RuleFor(tempHouse => tempHouse.TemporaryHouseAddress)
        //    .NotNull().WithMessage("TemporaryHouseAddress cannot be null.");
        //    //.SetValidator(new AddressCreateDTOValidator());

        RuleFor(tempHouse => tempHouse.StartOfTemporaryHouseDate)
            .NotEmpty().WithMessage("StartOfTemporaryHouseDate cannot be empty.")
            .Must(date => date > DateTime.Now).WithMessage("StartOfTemporaryHouseDate must be in the future.");
    }
}

