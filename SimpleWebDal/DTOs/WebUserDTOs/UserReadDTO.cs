using SimpleWebDal.DTOs.WebUserDTOs.BasicInformationDTOs;
using SimpleWebDal.DTOs.WebUserDTOs.CredentialsDTOs;
using SimpleWebDal.DTOs.WebUserDTOs.RoleDTOs;
using SimpleWebDal.Models.CalendarModel;

namespace SimpleWebDal.DTOs.WebUserDTOs;
public class UserReadDTO
{
    public Guid Id { get; set; }
    public CredentialsReadDTO Credentials { get; set; }
    public BasicInformationReadDTO BasicInformation { get; set; }
    public CalendarActivity UserCalendar { get; set; }
    public ICollection<RoleReadDTO> Roles { get; set; }
}
