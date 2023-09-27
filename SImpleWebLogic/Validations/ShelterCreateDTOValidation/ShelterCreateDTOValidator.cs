using FluentValidation;
using SimpleWebDal.DTOs.ShelterDTOs;
using SImpleWebLogic.Validations.AddressCreateDTOValidation;
using SImpleWebLogic.Validations.AdoptionCreateDTOValidation;
using SImpleWebLogic.Validations.CalendarCreateValidation;
using SImpleWebLogic.Validations.PetCreateDTOValidation;
using SImpleWebLogic.Validations.TemporaryHouseCreateDTOValidation;
using SImpleWebLogic.Validations.WebUserValidation;

namespace SImpleWebLogic.Validations.ShelterCreateDTOValidation;

public class ShelterCreateDTOValidator : AbstractValidator<ShelterCreateDTO>
{
    public ShelterCreateDTOValidator()
    {
        RuleFor(shelter => shelter.Name)
      .NotEmpty().WithMessage("Name cannot be empty.")
      .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");

        RuleFor(shelter => shelter.ShelterCalendar)
            .NotNull().WithMessage("ShelterCalendar cannot be null.");
        //.SetValidator(new CalendarValidator());

        RuleFor(shelter => shelter.ShelterAddress)
            .NotNull().WithMessage("ShelterAddress cannot be null.");
           // .SetValidator(new AddressCreateDTOValidator());

        RuleFor(shelter => shelter.ShelterDescription)
            .MaximumLength(500).WithMessage("ShelterDescription cannot exceed 500 characters.");

        //When(shelter => shelter.ShelterUsers != null && shelter.ShelterUsers.Any(), () =>
        //{
        //    RuleForEach(shelter => shelter.ShelterUsers);
        //      //  .SetValidator(new UserCreateDTOValidator());
        //});

        //When(shelter => shelter.ShelterPets != null && shelter.ShelterPets.Any(), () =>
        //{
        //    RuleForEach(shelter => shelter.ShelterPets)
        //    .NotNull();
        //        //.SetValidator(new PetCreateDTOValidator());
        //});

        //When(shelter => shelter.Adoptions != null && shelter.Adoptions.Any(), () =>
        //{
        //    RuleForEach(shelter => shelter.Adoptions)
        //    .NotNull();
        //       // .SetValidator(new AdoptionCreateDTOValidator());
        //});

        //When(shelter => shelter.TempHouses != null && shelter.TempHouses.Any(), () =>
        //{
        //    RuleForEach(shelter => shelter.TempHouses).NotNull();

        //       // .SetValidator(new TempHouseCreateDTOValidator());
        //});
    }
}
