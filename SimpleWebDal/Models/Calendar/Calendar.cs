using SimpleWebDal.Models.WebUser;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.Models.TimeTable;
public class TimeTable
{
    [Key]
    [Required]
    public int Id { get; init; }
    [Required]
    public string Owner { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime DateWithTime { get; set; }
    [Required]
    public Activity Activity { get; set; }
}
