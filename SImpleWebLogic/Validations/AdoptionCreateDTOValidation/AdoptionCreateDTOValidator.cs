using FluentValidation;
using SimpleWebDal.DTOs.AdoptionDTOs;

namespace SImpleWebLogic.Validations.AdoptionCreateDTOValidation;

public class AdoptionCreateDTOValidator : AbstractValidator<AdoptionCreateDTO>
{
    public AdoptionCreateDTOValidator()
    {

        RuleFor(adoption => adoption.DateOfAdoption)
            .NotNull().WithMessage("Date of adoption must be provided.");
    }
}
