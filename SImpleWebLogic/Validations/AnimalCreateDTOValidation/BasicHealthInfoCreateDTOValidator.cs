using FluentValidation;
using SimpleWebDal.DTOs.AnimalDTOs.BasicHealthInfoDTOs;

namespace SImpleWebLogic.Validations.AnimalCreateDTOValidation;

public class BasicHealthInfoCreateDTOValidator : AbstractValidator<BasicHealthInfoCreateDTO>
{
    public BasicHealthInfoCreateDTOValidator()
    {
        RuleFor(info => info.Name)
               .NotEmpty().WithMessage("Name cannot be empty.")
               .MaximumLength(30).WithMessage("Name cannot exceed 30 characters.");

        RuleFor(info => info.Age)
            .NotEmpty().WithMessage("Age cannot be empty.")
            .GreaterThan(0).WithMessage("Age must be greater than 0.");

        RuleFor(info => info.Size)
            .IsInEnum().WithMessage("Invalid Size value.");
      
    }
}
