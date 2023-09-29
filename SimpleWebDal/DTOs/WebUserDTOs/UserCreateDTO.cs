using SimpleWebDal.DTOs.AdoptionDTOs;
using SimpleWebDal.DTOs.CalendarDTOs;
using SimpleWebDal.DTOs.WebUserDTOs.BasicInformationDTOs;

using SimpleWebDal.DTOs.WebUserDTOs.RoleDTOs;
using SimpleWebDal.DTOs.WebUserDTOs.UserPetsDTOs;

namespace SimpleWebDal.DTOs.WebUserDTOs;

public class UserCreateDTO
{
  
    public BasicInformationCreateDTO BasicInformation { get; set; }
    public CalendarActivityCreateDTO UserCalendar { get; set; }
    public ICollection<RoleCreateDTO> Roles { get; set; }
    //public ICollection<AdoptionCreateDTO> Adoptions { get; set; }
    public ICollection<UserPetsCreateDTO> PetList { get; set; }
}
