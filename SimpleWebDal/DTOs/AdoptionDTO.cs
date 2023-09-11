using PetAdoptionCenter.Models.Animal;
using PetAdoptionCenter.Models.PetShelter;
using PetAdoptionCenter.Models.WebUser;
using System.ComponentModel.DataAnnotations;

namespace PetAdoptionCenter.DTOs;

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
