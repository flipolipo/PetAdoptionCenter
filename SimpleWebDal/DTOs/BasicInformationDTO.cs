using PetAdoptionCenter.Models.WebUser;
using System.ComponentModel.DataAnnotations;

namespace PetAdoptionCenter.DTOs;

public class BasicInformationDTO
{
    [Required]
    [MinLength(2)]
    [MaxLength(20)]
    public string Name { get; set; }
    [Required]
    [MinLength(2)]
    [MaxLength(20)]
    public string Surname { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    public AddressDTO addressDTO { get; set; }
}
