using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.Calendar;
using SimpleWebDal.Models.WebUser;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.Models.PetShelter;

public class Shelter
{
    [Key]
    [Required]
    public uint ShelterId { get; set; }
    public string Name { get; set; }
    public Address ShelterAddress { get; set; }
    public string ShelterDescription { get; set; }
    public User ShelterOwner { get; set; }
    public IEnumerable<User> ShelterWorkers { get; set; }
    public IEnumerable<User> ShelterContributors { get; set; }
    public IEnumerable<Pet> ListOfPets { get; set; }
    public IEnumerable<Pet> ListOfPetsAdopted { get; set; }
    public TimeTable ShelterCalendar { get; set; }
}
