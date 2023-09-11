using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.WebUser;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.Models.TemporaryHouse;

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
