using SimpleWebDal.Models.AdoptionProccess;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.TemporaryHouse;
using SimpleWebDal.Models.WebUser;

namespace SimpleWebDal.Models.PetShelter;

public class Shelter
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CalendarId { get; set; }
    public CalendarActivity ShelterCalendar { get; set; }
    public Guid AddressId { get; set; }
    public Address ShelterAddress { get; set; }
    public string ShelterDescription { get; set; }
    public ICollection<User>? ShelterUsers { get; set; }
    public ICollection<Pet>? ShelterPets { get; set; }
    public ICollection<Adoption>? Adoptions { get; set; }
    public ICollection<TempHouse>?  TempHouses { get; set; }
    


}
