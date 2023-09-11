using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.Models.WebUser;

public class BasicInformation
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
    [Phone]
    public string Phone { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public Address address { get; set; }
}
