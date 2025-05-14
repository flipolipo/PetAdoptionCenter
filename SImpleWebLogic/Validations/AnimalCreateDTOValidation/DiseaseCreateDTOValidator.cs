using FluentValidation;
using SimpleWebDal.DTOs.AnimalDTOs.DiseaseDTOs;

namespace SImpleWebLogic.Validations.AnimalCreateDTOValidation;

public class DiseaseCreateDTOValidator : AbstractValidator<DiseaseCreateDTO>
{
    public DiseaseCreateDTOValidator()
    {
        RuleFor(disease => disease.NameOfdisease)
                .NotEmpty().WithMessage("NameOfdisease cannot be empty.")
                .MaximumLength(30).WithMessage("NameOfdisease cannot exceed 30 characters.");

        RuleFor(disease => disease.IllnessStart)
            .NotEmpty().WithMessage("IllnessStart cannot be empty.")
            .Must(date => date <= DateTime.Now).WithMessage("IllnessStart must be in the past or present.");

        RuleFor(disease => disease.IllnessEnd)
            .NotEmpty().WithMessage("IllnessEnd cannot be empty.")
            .Must(date => date >= DateTime.Now).WithMessage("IllnessEnd must be in the present or future.")
            .GreaterThanOrEqualTo(disease => disease.IllnessStart).WithMessage("IllnessEnd must be greater than or equal to IllnessStart.");
    }
}
