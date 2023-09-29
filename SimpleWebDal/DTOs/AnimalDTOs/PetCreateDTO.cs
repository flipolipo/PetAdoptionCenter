using SimpleWebDal.Models.Animal.Enums;
using SimpleWebDal.DTOs.WebUserDTOs;
using SimpleWebDal.DTOs.AnimalDTOs.BasicHealthInfoDTOs;
using SimpleWebDal.DTOs.CalendarDTOs;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.DTOs.AnimalDTOs.PatronUsersDTOs;

namespace SimpleWebDal.DTOs.AnimalDTOs;

public class PetCreateDTO
{
    public PetType Type { get; init; }
    public BasicHealthInfoCreateDTO? BasicHealthInfo { get; set; }
    public string Description { get; set; }
    public CalendarActivityCreateDTO Calendar { get; set; }
    public PetStatus Status { get; set; }
    public bool AvaibleForAdoption { get; set; }
    public ICollection<UserCreateDTO> PatronUsers { get; set; } 
}
