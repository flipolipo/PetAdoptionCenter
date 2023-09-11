using PetAdoptionCenter.Models.Animal;
using PetAdoptionCenter.Models.ProfileUser;
using PetAdoptionCenter.Models.TimeTable;
using System.ComponentModel.DataAnnotations;

namespace PetAdoptionCenter.DTOs;

public class ProfileDTO
{
    [Required]
    public UserDTO UserLogged { get; set; }
    public IEnumerable<Pet> FavouriteListPets { get; set; }
    public IEnumerable<Pet> VirtualAdoptionPetsList { get; set; }
    public TimeTable<Profile> CalendarActivity { get; set; }
}
