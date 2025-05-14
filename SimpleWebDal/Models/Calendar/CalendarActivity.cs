namespace SimpleWebDal.Models.CalendarModel;
public class CalendarActivity
{
    public Guid Id { get; set; }
    public ICollection<Activity>? Activities { get; set; }
}
