using Microsoft.EntityFrameworkCore;
using SimpleWebDal.Models.Animal.Enums;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.WebUser;
using System.Text;

namespace SimpleWebDal.Models.Animal;

public class Pet
{
    public Guid Id { get; init; }
    public PetType Type { get; set; }
    public Guid BasicHealthInfoId { get; set; }
    public BasicHealthInfo? BasicHealthInfo { get; set; }
    public string Description { get; set; }
    public Guid CalendarId { get; set; }
    public CalendarActivity Calendar { get; set; }
    public PetStatus Status { get; set; }
    public bool AvaibleForAdoption { get; set; }
    public ICollection<PatronUsers>? PatronUsers { get; set; }
    public Guid ShelterId { get; set; }
}
