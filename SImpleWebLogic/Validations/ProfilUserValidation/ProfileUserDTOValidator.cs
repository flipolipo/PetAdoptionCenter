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
    }
}
