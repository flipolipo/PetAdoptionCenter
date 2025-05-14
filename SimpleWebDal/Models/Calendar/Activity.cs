namespace SimpleWebDal.Models.CalendarModel;

public class Activity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTimeOffset StartActivityDate { get; set; }
    public DateTimeOffset EndActivityDate { get; set; }
    public Guid CalendarActivityId { get; set; }
}
