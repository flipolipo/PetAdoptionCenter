using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.WebUser;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.DTOs;

public class AdoptionDTO
{
    [Key]
    public uint Id { get; set; }
    [Required]
    public Pet PetToAdoption { get; set; }
    [Required]
    public ShelterDTO Shelter { get; set; }
    [Required]
    public UserDTO Adopter { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime DateOfAdoption { get; set; }
}
