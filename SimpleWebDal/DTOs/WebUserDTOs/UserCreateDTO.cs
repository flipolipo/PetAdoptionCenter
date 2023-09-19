using SimpleWebDal.DTOs.WebUserDTOs.BasicInformationDTOs;
using SimpleWebDal.DTOs.WebUserDTOs.CredentialsDTOs;
using SimpleWebDal.DTOs.WebUserDTOs.RoleDTOs;
using SimpleWebDal.Models.CalendarModel;

namespace SimpleWebDal.DTOs.WebUserDTOs;

public class UserCreateDTO
{
    public CredentialsCreateDTO Credentials { get; set; }
    public BasicInformationCreateDTO BasicInformation { get; set; }
    public CalendarActivity UserCalendar { get; set; }
    public ICollection<RoleCreateDTO> Roles { get; set; }
}
