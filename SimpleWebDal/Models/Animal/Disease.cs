using System.ComponentModel.DataAnnotations;
using System.Formats.Asn1;

namespace PetAdoptionCenter.Models.Animal;

public class Disease
{
    [Required]
    [MinLength(2)]
    [MaxLength(20)]
    public string NameOfdisease { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime IllnessStart { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime IllnessEnd { get; set; }

}
