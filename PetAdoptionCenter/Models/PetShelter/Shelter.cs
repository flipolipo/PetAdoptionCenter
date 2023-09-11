using PetAdoptionCenter.Models.Animal;
using PetAdoptionCenter.Models.TimeTable;
using PetAdoptionCenter.Models.WebUser;

namespace PetAdoptionCenter.Models.PetShelter;

public class Shelter
{
    public uint Id { get; set; }
    public string Name { get; set; }
    public Address ShelterAddress { get; set; }
    public string ShelterDescription { get; set; }
    public User ShelterOwner { get; set; }
    public IEnumerable<User> UsersAdmin { get; set; }
    public IEnumerable<User> UsersHelping { get; set; }
    public IEnumerable<Pet> ListOfPets { get; set; }
    public IEnumerable<Pet> ListOfPetsAdopted { get; set; }
    public TimeTable<Shelter> ShelterCalendar { get; set; }
}
