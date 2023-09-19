using SimpleWebDal.DTOs.AddressDTOs;
using SimpleWebDal.DTOs.AnimalDTOs;
using SimpleWebDal.DTOs.WebUserDTOs;
using SimpleWebDal.Models.CalendarModel;

namespace SimpleWebDal.DTOs.ShelterDTOs;

public class ShelterCreateDTO
{
    public string Name { get; set; }
    public AddressCreateDTO ShelterAddress { get; set; }
    public string ShelterDescription { get; set; }
    public ICollection<UserCreateDTO> ShelterUsers { get; set; }
    public ICollection<PetCreateDTO> ShelterPets { get; set; }
    public CalendarActivity ShelterCalendar { get; set; }
}
