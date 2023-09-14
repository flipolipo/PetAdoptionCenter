using SimpleWebDal.Models.WebUser;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.Models.TimeTable;
public class TimeTable
{
    [Key]
    [Required]
    public uint Id { get; init; }
    [Required]
    public User Owner { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime DateWithTime { get; set; }
    [Required]
    public Activity Activity { get; set; }
}
