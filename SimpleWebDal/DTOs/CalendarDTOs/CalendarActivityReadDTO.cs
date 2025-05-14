using SimpleWebDal.DTOs.CalendarDTOs.ActivityDTOs;

namespace SimpleWebDal.DTOs.CalendarDTOs;

public class CalendarActivityReadDTO
{
    public ICollection<ActivityReadDTO>? Activities { get; set; }
}
