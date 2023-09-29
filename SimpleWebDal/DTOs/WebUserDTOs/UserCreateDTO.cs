using SimpleWebDal.DTOs.AdoptionDTOs;
using SimpleWebDal.DTOs.AnimalDTOs;
using SimpleWebDal.DTOs.CalendarDTOs;
using SimpleWebDal.DTOs.WebUserDTOs.BasicInformationDTOs;
using SimpleWebDal.DTOs.WebUserDTOs.CredentialsDTOs;
using SimpleWebDal.DTOs.WebUserDTOs.RoleDTOs;
using SimpleWebDal.DTOs.WebUserDTOs.UserPetsDTOs;

namespace SimpleWebDal.DTOs.WebUserDTOs;

public class UserCreateDTO
{
    public CredentialsCreateDTO Credentials { get; set; }
    public BasicInformationCreateDTO BasicInformation { get; set; }
    public CalendarActivityCreateDTO UserCalendar { get; set; }
    public ICollection<RoleCreateDTO> Roles { get; set; }
    //public ICollection<AdoptionCreateDTO> Adoptions { get; set; }
    public ICollection<PetCreateDTO> PetList { get; set; }
}
