using SimpleWebDal.Models.CalendarModel;

namespace SimpleWebDal.Models.AdoptionProccess;

public class Adoption
{
    public Guid Id { get; set; }
    public Guid PetId { get; set; }
    public Guid UserId { get; set; }
    public bool IsPreAdoptionPoll { get; set; }
    public string PreadoptionPoll { get; set; }
    public Guid? CalendarId { get; set; }
    public CalendarActivity? Activity { get; set; }
    public bool IsMeetings { get; set; }
    public bool IsContractAdoption { get; set; }
    public string? ContractAdoption { get; set; }
    public DateTimeOffset? DateOfAdoption { get; set; }
}
