using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.Models.Calendar;
public class TimeTable
{
    [Key]
    [Required]
    public int TimeTableId { get; init; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime DateWithTime { get; set; }
    public int ActivityId { get; set; }
    [Required]

    public Activity Activity { get; set; }
}
