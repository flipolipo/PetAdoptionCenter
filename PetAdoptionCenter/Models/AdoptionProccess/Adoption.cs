using PetAdoptionCenter.Models.Animal;
using PetAdoptionCenter.Models.PetShelter;
using PetAdoptionCenter.Models.WebUser;

namespace PetAdoptionCenter.Models.AdoptionProccess;

public class Adoption
{
    public uint Id { get; set; }
    public Pet PetToAdoption { get; set; }
    public Shelter Shelter { get; set; }
    public User Adopter { get; set; }
    public DateTime DateOfAdoption { get; set; }
}
