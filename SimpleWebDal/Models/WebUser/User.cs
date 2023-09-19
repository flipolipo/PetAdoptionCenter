using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.ProfileUser;
using SimpleWebDal.Models.TemporaryHouse;

namespace SimpleWebDal.Models.WebUser;

public class User
{
    public Guid Id { get; set; }
    public Credentials Credentials { get; set; }
    public BasicInformation BasicInformation { get; set; }
    public CalendarActivity UserCalendar { get; set; }
    public ICollection<Role> Roles { get; set; }
    public TempHouse? TempHouse { get; set; }
    public ProfileModel? ProfileUser { get; set; }
}
