using FluentValidation;
using SimpleWebDal.DTOs.AnimalDTOs;
using SimpleWebDal.Models.Animal.Enums;
using SImpleWebLogic.Validations.CalendarCreateValidation;
using SImpleWebLogic.Validations.WebUserValidation;

namespace SImpleWebLogic.Validations.PetCreateDTOValidation;

public class PetCreateDTOValidator : AbstractValidator<PetCreateDTO>
{
    public PetCreateDTOValidator()
    {
        RuleFor(pet => pet.Type)
            .NotEmpty().WithMessage("Type must be provided.");
        //walidacja dla BasicHealthInfo? tak czy nie?
        RuleFor(pet => pet.Description)
            .NotEmpty().WithMessage("Description must be provided.")
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
        RuleFor(pet => pet.Callendar).SetValidator(new CalendarValidator());
        RuleFor(pet => pet.Status).NotEmpty().WithMessage("Status must be provided.");
        RuleFor(pet => pet.AvaibleForAdoption).Must(avaible => avaible == true)
            .When(pet => pet.Status == PetStatus.OnAdoptionProccess)
            .WithMessage("If the status is 'Available for Adoption', AvaibleForAdoption must be true.");
        RuleForEach(pet => pet.PatronUsers)
            .NotEmpty().WithMessage("At least one patron user must be provided.")
            .SetValidator(new UserCreateDTOValidator());
    }
}
