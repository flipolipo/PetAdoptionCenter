namespace SimpleWebDal.DTOs.CalendarDTOs.ActivityDTOs;

public class ActivityReadDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTimeOffset ActivityDate { get; set; }
}
