using SimpleWebDal.DTOs.CalendarDTOs.ActivityDTOs;

namespace SimpleWebDal.DTOs.CalendarDTOs;

public class CalendarActivityReadDTO
{
    public DateTime DateWithTime { get; set; }
    public ICollection<ActivityReadDTO>? Activities { get; set; }
}
