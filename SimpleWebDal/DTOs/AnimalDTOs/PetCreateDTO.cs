using SimpleWebDal.Models.Animal.Enums;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.DTOs.WebUserDTOs;

namespace SimpleWebDal.DTOs.AnimalDTOs;

public class PetCreateDTO
{
    public Guid Id { get; init; }
    public PetType Type { get; init; }
    public BasicHealthInfo? BasicHealthInfo { get; set; }
    public string Description { get; set; }
    public CalendarActivity Callendar { get; set; }
    public PetStatus Status { get; set; }
    public bool AvaibleForAdoption { get; set; }
    public ICollection<UserCreateDTO> PatronUsers { get; set; }
}
