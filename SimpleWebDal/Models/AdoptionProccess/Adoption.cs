using PetAdoptionCenter.Models.Animal;
using PetAdoptionCenter.Models.PetShelter;
using PetAdoptionCenter.Models.WebUser;
using System.ComponentModel.DataAnnotations;

namespace PetAdoptionCenter.Models.AdoptionProccess;

public class Adoption
{
    [Key]
    public uint Id { get; set; }
    [Required]
    
    public Pet PetToAdoption { get; set; }
    [Required]
    public Shelter Shelter { get; set; }
    [Required]
    public User Adopter { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime DateOfAdoption { get; set; }
}
