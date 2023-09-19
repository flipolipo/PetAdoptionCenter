using SimpleWebDal.DTOs.AnimalDTOs;
using SimpleWebDal.DTOs.WebUserDTOs;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.WebUser;

namespace SimpleWebDal.DTOs.ShelterDTOs;

public class ShelterReadDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Address ShelterAddress { get; set; }
    public string ShelterDescription { get; set; }
    public UserReadDTO ShelterOwner { get; set; }
    public IEnumerable<UserReadDTO> ShelterUsers { get; set; }
    public IEnumerable<PetReadDTO> ShelterPets { get; set; }
    public CalendarActivity ShelterCalendar { get; set; }
}

