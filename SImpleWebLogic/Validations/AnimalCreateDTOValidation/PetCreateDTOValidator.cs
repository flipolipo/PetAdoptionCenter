using FluentValidation;
using SimpleWebDal.DTOs.AnimalDTOs;

namespace SImpleWebLogic.Validations.PetCreateDTOValidation;

public class PetCreateDTOValidator : AbstractValidator<PetCreateDTO>
{
    public PetCreateDTOValidator()
    {
        RuleFor(pet => pet.Type)
          .IsInEnum().WithMessage("Invalid PetType value.");
        RuleFor(info => info.Gender)
           .IsInEnum().WithMessage("Invalid Size value.");

        RuleFor(pet => pet.Description)
            .MaximumLength(300).WithMessage("Description cannot exceed 300 characters.");

        RuleFor(pet => pet.Calendar)
            .NotNull().WithMessage("Calendar cannot be null.");

        RuleFor(pet => pet.Status)
            .IsInEnum().WithMessage("Invalid PetStatus value.");

    }
}
