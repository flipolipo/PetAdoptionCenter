using SimpleWebDal.Models.Animal.Enums;
using SimpleWebDal.DTOs.AnimalDTOs.BasicHealthInfoDTOs;
using SimpleWebDal.DTOs.CalendarDTOs;
using SimpleWebDal.DTOs.WebUserDTOs;

namespace SimpleWebDal.DTOs.AnimalDTOs;

public class PetReadDTO
{
    public Guid Id { get; set; }
    public PetType Type { get; set; }
    public BasicHealthInfoReadDTO? BasicHealthInfo { get; set; }
    public string Description { get; set; }
    public CalendarActivityReadDTO Calendar { get; set; }
    public PetStatus Status { get; set; }
    public bool AvaibleForAdoption { get; set; }
    public Guid ShelterId { get; set; }
    public ICollection<UserReadDTO>? Users { get; set; }
}
