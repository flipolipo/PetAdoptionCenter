using SimpleWebDal.Models.PetShelter;

namespace SimpleWebDal.Models.CalendarModel;
public class CalendarActivity
{
    public Guid Id { get; init; }
    public DateTime DateWithTime { get; set; }
    public ICollection<Activity> Activities { get; set; }
    public Shelter? Shelter { get; set; }
}
