using PetAdoptionCenter.Models.Animal;
using PetAdoptionCenter.Models.TimeTable;
using PetAdoptionCenter.Models.WebUser;
using System.ComponentModel.DataAnnotations;

namespace PetAdoptionCenter.Models.ProfileUser;

public class Profile
{
    [Required]
    public User UserLogged { get; set; }
    public IEnumerable<Pet> FavouriteListPets { get; set; }
    public IEnumerable<Pet> VirtualAdoptionPetsList { get; set; }
    public TimeTable<Profile> CalendarActivity { get; set; }
}
