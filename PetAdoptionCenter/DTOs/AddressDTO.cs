using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.ComponentModel.DataAnnotations;

namespace PetAdoptionCenter.DTOs
{
    public class AddressDTO
    {
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Invalid postal code format. It should be in the format XX-XXX.")]
        public string PostalCode { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(35)]
        public string City { get; set; }
    }
}
