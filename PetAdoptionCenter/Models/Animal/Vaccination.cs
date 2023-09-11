using System.ComponentModel.DataAnnotations;

namespace PetAdoptionCenter.Models.Animal;

public class Vaccination
{
    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public string VaccinationName { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime date { get; set; }
}
