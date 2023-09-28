using SimpleWebDal.DTOs.AddressDTOs;
using SimpleWebDal.DTOs.AdoptionDTOs;
using SimpleWebDal.DTOs.AnimalDTOs;
using SimpleWebDal.DTOs.CalendarDTOs;
using SimpleWebDal.DTOs.TemporaryHouseDTOs;
using SimpleWebDal.DTOs.WebUserDTOs;

namespace SimpleWebDal.DTOs.ShelterDTOs;

public class ShelterCreateDTO
{
    public string Name { get; set; }
    public CalendarActivityCreateDTO ShelterCalendar { get; set; }
    public AddressCreateDTO ShelterAddress { get; set; }
    public string ShelterDescription { get; set; }
    public ICollection<UserCreateDTO>? ShelterUsers { get; set; }
    public ICollection<PetCreateDTO>? ShelterPets { get; set; }
    public ICollection<AdoptionCreateDTO>? Adoptions { get; set; }
    public ICollection<TempHouseCreateDTO>? TempHouses { get; set; }
}
