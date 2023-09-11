using PetAdoptionCenter.Models.WebUser;
using System.ComponentModel.DataAnnotations;

namespace PetAdoptionCenter.DTOs;

public class BasicInformationDTO
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    [Required]
    public string Email { get; set; }
    public AddressDTO addressDTO { get; set; }
}
