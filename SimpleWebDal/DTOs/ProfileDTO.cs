using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.Calendar;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.DTOs;

public class ProfileDTO
{
    [Required]
    public UserDTO UserLogged { get; set; }
    public IEnumerable<Pet> FavouriteListPets { get; set; }
    public IEnumerable<Pet> VirtualAdoptionPetsList { get; set; }
    public TimeTable CalendarActivity { get; set; }
}
