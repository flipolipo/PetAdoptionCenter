namespace SimpleWebDal.Models.CalendarModel;
public class CalendarActivity
{
    public Guid Id { get; init; }
    public DateTime DateWithTime { get; set; } = DateTime.Now;
    public ICollection<Activity>? Activities { get; set; }
}
