using SimpleWebDal.Models.AdoptionProccess;
using SimpleWebDal.Models.Animal;

using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.TemporaryHouse;
using SimpleWebDal.Models.WebUser;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.Models.PetShelter;

public class Shelter
{


    public int ShelterId { get; set; }
    public string Name { get; set; }
    public Address ShelterAddress { get; set; }
    public string ShelterDescription { get; set; }
    public User ShelterOwner { get; set; }
    public ICollection<User> ShelterUsers { get; set; }
    public ICollection<Pet> ShelterPets { get; set; }
    public CalendarModelClass ShelterCalendar { get; set; }

    //Navigation properties:

    public int AdoptionId { get; set; }
    public Adoption Adoption { get; set; }
    public int TempHouseId { get; set; }
    public TempHouse TempHouse { get; set; }


}
