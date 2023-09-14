namespace SimpleWebDal.Models.CalendarModel;
public class CalendarActivity
{
    public int CalendarId { get; init; }
    public DateTime DateWithTime { get; set; }
    public ICollection<Activity> Activities { get; set; }
}
