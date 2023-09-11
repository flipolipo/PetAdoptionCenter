using PetAdoptionCenter.Models.Animal;
using PetAdoptionCenter.Models.WebUser;
using System.ComponentModel.DataAnnotations;

namespace PetAdoptionCenter.Models.TimeTable;

public class Activity
{
    [Key]
    [Required]
    public uint Id { get; set; }
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string Name { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime AcctivityDate { get; set; }
    [Required]
    public Pet pet { get; set; }
}
