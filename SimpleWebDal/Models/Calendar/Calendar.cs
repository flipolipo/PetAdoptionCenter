using System.ComponentModel.DataAnnotations;

namespace PetAdoptionCenter.Models.TimeTable;
public class TimeTable<T>
{
    [Key]
    [Required]
    public uint Id { get; init; }
    [Required]
    public T Owner { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime DateWithTime { get; set; }
    [Required]
    public Activity Activity { get; set; }
}
