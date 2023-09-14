using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.Models.Calendar;
public class TimeTable
{
    [Key]
    [Required]
    public int Id { get; init; }
    [Required]
    public string TimeTableUser { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime DateWithTime { get; set; }
    [Required]
    public Activity Activity { get; set; }
}
