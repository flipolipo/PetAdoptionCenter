using SimpleWebDal.Models.Animal.Enums;
using SimpleWebDal.DTOs.AnimalDTOs.BasicHealthInfoDTOs;
using SimpleWebDal.DTOs.CalendarDTOs;
using SimpleWebDal.DTOs.WebUserDTOs;
using SimpleWebDal.DTOs.ProfileUserDTOs;

namespace SimpleWebDal.DTOs.AnimalDTOs;

public class PetReadDTO
{
    public Guid Id { get; init; }
    public PetType Type { get; init; }
    public BasicHealthInfoReadDTO? BasicHealthInfo { get; set; }
    public string Description { get; set; }
    public CalendarActivityReadDTO Calendar { get; set; }
    public PetStatus Status { get; set; }
    public bool AvaibleForAdoption { get; set; }
    public ICollection<UserReadDTO>? PatronUsers { get; set; }
    public ICollection<ProfileModelReadDTO>? Profiles { get; set; }
}
