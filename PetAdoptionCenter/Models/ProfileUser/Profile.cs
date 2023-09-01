using PetAdoptionCenter.Models.Animal;
using PetAdoptionCenter.Models.TimeTable;
using PetAdoptionCenter.Models.WebUser;

namespace PetAdoptionCenter.Models.ProfileUser;

public class Profile
{
    public User UserLogged { get; set; }
    public IEnumerable<Pet> FavouriteListPets { get; set; }
    public IEnumerable<Pet> VirtualAdoptionPetsList { get; set; }
    public TimeTable<Profile> CalendarActivity { get; set; }
}
