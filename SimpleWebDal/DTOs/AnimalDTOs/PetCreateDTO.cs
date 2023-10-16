using SimpleWebDal.Models.Animal.Enums;
using SimpleWebDal.DTOs.WebUserDTOs;
using SimpleWebDal.DTOs.AnimalDTOs.BasicHealthInfoDTOs;
using SimpleWebDal.DTOs.CalendarDTOs;
using Microsoft.AspNetCore.Http;

namespace SimpleWebDal.DTOs.AnimalDTOs;

public class PetCreateDTO
{
    public PetType Type { get; set; }
    public PetGender Gender { get; set; }
    public BasicHealthInfoCreateDTO? BasicHealthInfo { get; set; }
    public string Description { get; set; }
    public PetStatus Status { get; set; }
    public bool AvaibleForAdoption { get; set; }
    public IFormFile ImageFile { get; set; }
    //public ICollection<UserCreateDTO> Users { get; set; } 
}
