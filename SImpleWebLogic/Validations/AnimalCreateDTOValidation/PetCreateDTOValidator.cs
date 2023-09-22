using FluentValidation;
using SimpleWebDal.DTOs.AnimalDTOs;
using SimpleWebDal.Models.Animal.Enums;
using SImpleWebLogic.Validations.AnimalCreateDTOValidation;
using SImpleWebLogic.Validations.CalendarCreateValidation;
using SImpleWebLogic.Validations.WebUserValidation;

namespace SImpleWebLogic.Validations.PetCreateDTOValidation;

public class PetCreateDTOValidator : AbstractValidator<PetCreateDTO>
{
    public PetCreateDTOValidator()
    {
        RuleFor(pet => pet.Type)
          .IsInEnum().WithMessage("Invalid PetType value.");

        RuleFor(pet => pet.Description)
            .MaximumLength(300).WithMessage("Description cannot exceed 300 characters.");

        RuleFor(pet => pet.Calendar)
            .NotNull().WithMessage("Calendar cannot be null.");

        RuleFor(pet => pet.Status)
            .IsInEnum().WithMessage("Invalid PetStatus value.");

        When(pet => pet.BasicHealthInfo != null, () =>
        {
            RuleFor(pet => pet.BasicHealthInfo)
            .NotNull();
                //.SetValidator(new BasicHealthInfoCreateDTOValidator());
        });

        When(pet => pet.PatronUsers.Any(), () =>
        {
            RuleForEach(pet => pet.PatronUsers)
            .NotNull();
                //.SetValidator(new UserCreateDTOValidator());
        });
    }
}
