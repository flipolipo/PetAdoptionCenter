using FluentValidation;
using SimpleWebDal.DTOs.AnimalDTOs.VaccinationDTOs;

namespace SImpleWebLogic.Validations.AnimalCreateDTOValidation;

public class VaccinationCreateDTOValidator : AbstractValidator<VaccinationCreateDTO>
{
    public VaccinationCreateDTOValidator()
    {
        //RuleFor(vaccination => vaccination.VaccinationName)
        //        .NotEmpty().WithMessage("VaccinationName cannot be empty.")
        //        .MaximumLength(30).WithMessage("VaccinationName cannot exceed 30 characters.");

        //RuleFor(vaccination => vaccination.date)
        //    .NotEmpty().WithMessage("Date cannot be empty.")
        //    .Must(date => date > DateTime.Now).WithMessage("Date must be in the future.");
    }
}

