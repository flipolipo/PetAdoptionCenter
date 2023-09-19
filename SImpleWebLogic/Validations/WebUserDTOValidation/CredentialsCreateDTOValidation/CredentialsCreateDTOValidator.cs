using FluentValidation;
using SimpleWebDal.DTOs.WebUserDTOs.CredentialsDTOs;

namespace SImpleWebLogic.Validations.WebUserValidation.CredentialsCreateValidations;

public class CredentialsCreateDTOValidator : AbstractValidator<CredentialsCreateDTO>
{
    public CredentialsCreateDTOValidator()
    {
        RuleFor(credentials => credentials.Username)
          .NotEmpty().WithMessage("The username cannot be empty.")
          .MaximumLength(30).WithMessage("The maximum length of a username is 30 characters.");

        RuleFor(credentials => credentials.Password)
            .NotEmpty().WithMessage("The password cannot be empty.")
            .MinimumLength(8).WithMessage("The password must be at least 8 characters long.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$")
            .WithMessage("The password must contain at least one lowercase letter, one uppercase letter and one number.");
    }
}
