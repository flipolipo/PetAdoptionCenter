using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.WebUser;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.Models.AdoptionProccess;

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
