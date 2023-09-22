using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.WebUser;

namespace SimpleWebDal.Models.CalendarModel;
public class CalendarActivity
{
    public Guid Id { get; init; }
    public DateTime DateWithTime { get; set; } = DateTime.Now;
    public ICollection<Activity>? Activities { get; set; }



}
