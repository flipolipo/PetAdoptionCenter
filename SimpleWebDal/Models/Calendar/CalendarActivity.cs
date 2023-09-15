using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.TemporaryHouse;

namespace SimpleWebDal.Models.CalendarModel;
public class CalendarActivity
{
    public int Id { get; init; }
    public DateTime DateWithTime { get; set; }
    public ICollection<Activity> Activities { get; set; }
    public Shelter? Shelter { get; set; }
}
