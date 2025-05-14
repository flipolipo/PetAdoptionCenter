namespace SimpleWebDal.DTOs.CalendarDTOs.ActivityDTOs;

public class ActivityCreateDTO
{
    public string Name { get; set; }
    public DateTimeOffset StartActivityDate { get; set; }
    public DateTimeOffset EndActivityDate { get; set; }
}
