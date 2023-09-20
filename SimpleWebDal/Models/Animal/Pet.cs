using SimpleWebDal.Models.Animal.Enums;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.WebUser;

namespace SimpleWebDal.Models.Animal;

public class Pet
{
    public Guid Id { get; init; }
    public PetType Type { get; init; }
    public BasicHealthInfo? BasicHealthInfo { get; set; }
    public string Description { get; set; }
    public CalendarActivity? Callendar { get; set; }
    public PetStatus Status { get; set; }
    public bool AvaibleForAdoption { get; set; }
    public ICollection<User>? PatronUsers { get; set; }
}
