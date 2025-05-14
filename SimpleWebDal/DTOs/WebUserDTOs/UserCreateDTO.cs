using SimpleWebDal.DTOs.CalendarDTOs;
using SimpleWebDal.DTOs.WebUserDTOs.BasicInformationDTOs;

using SimpleWebDal.DTOs.WebUserDTOs.RoleDTOs;

namespace SimpleWebDal.DTOs.WebUserDTOs;

public class UserCreateDTO
{
  
    public BasicInformationCreateDTO BasicInformation { get; set; }
    public CalendarActivityCreateDTO UserCalendar { get; set; }
   // public ICollection<RoleCreateDTO> Roles { get; set; }
    
}
