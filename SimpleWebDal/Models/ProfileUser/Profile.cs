using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.TimeTable;
using SimpleWebDal.Models.WebUser;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.Models.ProfileUser;

public class Profile
{
    [Required]
    public User UserLogged { get; set; }
    public IEnumerable<Pet> FavouriteListPets { get; set; }
    public IEnumerable<Pet> VirtualAdoptionPetsList { get; set; }
    public TimeTable<Profile> CalendarActivity { get; set; }
}
