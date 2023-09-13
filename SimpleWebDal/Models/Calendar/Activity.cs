using SimpleWebDal.Models.Animal;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.Models.Calendar;

public class Activity
{
    [Key]
    [Required]
    public uint ActivityId { get; set; }
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string Name { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime AcctivityDate { get; set; }
   // public Pet pet { get; set; }
}
