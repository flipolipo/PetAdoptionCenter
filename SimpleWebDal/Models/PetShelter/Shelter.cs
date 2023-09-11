using PetAdoptionCenter.Models.Animal;
using PetAdoptionCenter.Models.TimeTable;
using PetAdoptionCenter.Models.WebUser;
using System.ComponentModel.DataAnnotations;

namespace PetAdoptionCenter.Models.PetShelter;

public class Shelter
{
    [Key]
    [Required]
    public uint Id { get; set; }
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string Name { get; set; }
    [Required]
    public Address ShelterAddress { get; set; }
    public string ShelterDescription { get; set; }
    [Required]
    public User ShelterOwner { get; set; }
    public IEnumerable<User> ShelterWorkers { get; set; }
    public IEnumerable<User> ShelterContributors { get; set; }
    public IEnumerable<Pet> ListOfPets { get; set; }
    public IEnumerable<Pet> ListOfPetsAdopted { get; set; }
    public TimeTable<Shelter> ShelterCalendar { get; set; }
}
