using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.Models.WebUser;

public class Address
{
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string Street { get; set; }
    [Required]
    
  
    public string HouseNumber { get; set; }
    [Required]
    
    public int FlatNumber { get; set; }
    [Required]
    [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Invalid postal code format. It should be in the format XX-XXX.")]
    public string PostalCode { get; set; }
    [Required]
    [MinLength(2)]
    [MaxLength(35)]
    public string City { get; set; }
}
