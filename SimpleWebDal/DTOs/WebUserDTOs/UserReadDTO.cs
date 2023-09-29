using SimpleWebDal.DTOs.AdoptionDTOs;
using SimpleWebDal.DTOs.CalendarDTOs;
using SimpleWebDal.DTOs.WebUserDTOs.BasicInformationDTOs;

using SimpleWebDal.DTOs.WebUserDTOs.RoleDTOs;
using SimpleWebDal.DTOs.WebUserDTOs.UserPetsDTOs;

namespace SimpleWebDal.DTOs.WebUserDTOs;
public class UserReadDTO
{
    public string Id { get; set; }
  
    public BasicInformationReadDTO BasicInformation { get; set; }
    public CalendarActivityReadDTO UserCalendar { get; set; }
    public ICollection<RoleReadDTO> Roles { get; set; }
    public ICollection<AdoptionReadDTO>? Adoptions { get; set; }
    public ICollection<UserPetsReadDTO>? PetList { get; set; }
}
