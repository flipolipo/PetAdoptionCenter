using PetAdoptionCenter.Models.Animal;
using PetAdoptionCenter.Models.PetShelter;
using PetAdoptionCenter.Models.WebUser;
using System.ComponentModel.DataAnnotations;

namespace PetAdoptionCenter.Models.TemporaryHouse;

public class TempHouse
{
    [Key]
    [Required]
    public uint Id { get; init; }
    [Required]
    public User TemporaryOwner { get; set; }
    [Required]
    
    public Pet PetInTemporaryHouse { get; set; }
    [Required]
    public Shelter ShelterName { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime StartOfTemporaryHouseDate { get; init; }
}
