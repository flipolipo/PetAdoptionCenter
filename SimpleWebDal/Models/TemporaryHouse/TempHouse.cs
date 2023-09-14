using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.WebUser;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.Models.TemporaryHouse;

public class TempHouse
{ 
    
    public int TempHouseId { get; init; }
    
    public User TemporaryOwner { get; set; }
    
    public Address TemporaryHouseAddress { get; set; }
    public ICollection<Pet> PetsInTemporaryHouse { get; set; }
   
    public Shelter ShelterName { get; set; }
   
    public DateTime StartOfTemporaryHouseDate { get; init; }
}
