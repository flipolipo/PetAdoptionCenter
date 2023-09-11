using SimpleWebDal.Models.Animal.Enums;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.Models.Animal;

public class BasicHealthInfo
{
    [Required]
    [MinLength(2)]
    [MaxLength(20)]
    public string Name { get; set; }
    [Required]
    [Range(0,50)]
    public int Age { get; set; }
    public Size Size { get; set; }
    public IEnumerable<Vaccination> Vaccinations { get; set; }
    public IEnumerable<Disease> MedicalHistory { get; set; }
}
