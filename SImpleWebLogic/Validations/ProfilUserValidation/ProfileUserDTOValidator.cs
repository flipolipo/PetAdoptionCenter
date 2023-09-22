using FluentValidation;
using SimpleWebDal.DTOs.ProfileUserDTOs;
using SImpleWebLogic.Validations.PetCreateDTOValidation;
using SImpleWebLogic.Validations.WebUserValidation;

namespace SImpleWebLogic.Validations.ProfilUserValidation;

public class ProfileUserDTOValidator :AbstractValidator<ProfileModelCreateDTO>
{
    public ProfileUserDTOValidator()
    {
        RuleFor(profile => profile.UserLogged).SetValidator(new UserCreateDTOValidator());
        When(profile => profile.ProfilePets.Any(), () =>
        {
            RuleForEach(profile => profile.ProfilePets)
                .NotNull().WithMessage("ProfilePets cannot contain null elements.");
                //.SetValidator(new PetCreateDTOValidator());
        });
    }
}
