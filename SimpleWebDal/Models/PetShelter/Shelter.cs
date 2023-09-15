using SimpleWebDal.Models.AdoptionProccess;
using SimpleWebDal.Models.Animal;

using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.TemporaryHouse;
using SimpleWebDal.Models.WebUser;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.Models.PetShelter;

public class Shelter
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int AddressId { get; set; }
    public Address ShelterAddress { get; set; }
    public string ShelterDescription { get; set; }
    public ICollection<User> ShelterUsers { get; set; }
    public ICollection<Pet> ShelterPets { get; set; }
    public int CalendarActivityId { get; set; }
    public CalendarActivity ShelterCalendar { get; set; }
    public TempHouse? TempHouse { get; set; }

}
