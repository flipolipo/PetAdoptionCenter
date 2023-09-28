using SimpleWebDal.Models.AdoptionProccess;
using SimpleWebDal.Models.CalendarModel;

namespace SimpleWebDal.Models.WebUser;

public class User
{
    public Guid Id { get; set; }
    public Guid CredentialsId { get; set; }
    public Credentials Credentials { get; set; }
    public Guid BasicInformationId { get; set; }
    public BasicInformation BasicInformation { get; set; }
    public Guid UserCalendarId { get; set; }
    public CalendarActivity UserCalendar { get; set; }
    public ICollection<Role> Roles { get; set; }
    public ICollection<Adoption>? Adoptions { get; set; }
    public ICollection<UserPets>? PetList { get; set; }
}
