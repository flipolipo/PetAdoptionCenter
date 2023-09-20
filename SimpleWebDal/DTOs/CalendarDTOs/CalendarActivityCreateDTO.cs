using SimpleWebDal.DTOs.CalendarDTOs.ActivityDTOs;

namespace SimpleWebDal.DTOs.CalendarDTOs;

public class CalendarActivityCreateDTO
{
    public DateTime DateWithTime { get; set; }
    public ICollection<ActivityCreateDTO>? Activities { get; set; }
}
