namespace SimpleWebDal.Models.CalendarModel;

public class Activity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTimeOffset ActivityDate { get; set; }
    public Guid CalendarActivityId { get; set; }
}
