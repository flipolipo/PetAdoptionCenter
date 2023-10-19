using SimpleWebDal.DTOs.AdoptionDTOs;
using SimpleWebDal.DTOs.AnimalDTOs;
using SimpleWebDal.DTOs.CalendarDTOs;
using SimpleWebDal.DTOs.TemporaryHouseDTOs;
using SimpleWebDal.DTOs.WebUserDTOs;
using SimpleWebDal.Models.WebUser;

namespace SimpleWebDal.DTOs.ShelterDTOs;

public class ShelterReadDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public CalendarActivityReadDTO ShelterCalendar { get; set; }
    public Address ShelterAddress { get; set; }
    public string ShelterDescription { get; set; }
    public ICollection<UserReadDTO>? ShelterUsers { get; set; }
    public ICollection<PetReadDTO>? ShelterPets { get; set; }
    public ICollection<AdoptionReadDTO>? Adoptions { get; set; }
    public ICollection<TempHouseReadDTO>? TempHouses { get; set; }
    public string ImageBase64 { get; set; }
}

