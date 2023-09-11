using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.ProfileUser;
using SimpleWebDal.Models.TimeTable;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.DTOs;

public class ProfileDTO
{
    [Required]
    public UserDTO UserLogged { get; set; }
    public IEnumerable<Pet> FavouriteListPets { get; set; }
    public IEnumerable<Pet> VirtualAdoptionPetsList { get; set; }
    public TimeTable<Profile> CalendarActivity { get; set; }
}
