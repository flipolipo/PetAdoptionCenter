using SimpleWebDal.DTOs.AdoptionDTOs;
using SimpleWebDal.DTOs.AnimalDTOs;
using SimpleWebDal.DTOs.CalendarDTOs;
using SimpleWebDal.DTOs.WebUserDTOs.BasicInformationDTOs;
using SimpleWebDal.DTOs.WebUserDTOs.CredentialsDTOs;
using SimpleWebDal.DTOs.WebUserDTOs.RoleDTOs;
using SimpleWebDal.DTOs.WebUserDTOs.UserPetsDTOs;

namespace SimpleWebDal.DTOs.WebUserDTOs;
public class UserReadDTO
{
    public Guid Id { get; set; }
    public CredentialsReadDTO Credentials { get; set; }
    public BasicInformationReadDTO BasicInformation { get; set; }
    public CalendarActivityReadDTO UserCalendar { get; set; }
    public ICollection<RoleReadDTO> Roles { get; set; }
    public ICollection<AdoptionReadDTO>? Adoptions { get; set; }
    public ICollection<PetReadDTO>? PetList { get; set; }
}
