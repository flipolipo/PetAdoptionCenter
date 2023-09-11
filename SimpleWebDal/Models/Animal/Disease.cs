using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.Models.Animal;

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
