namespace SimpleWebDal.Models.CalendarModel;

public class Activity
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime? ActivityDate { get; set; }
}
