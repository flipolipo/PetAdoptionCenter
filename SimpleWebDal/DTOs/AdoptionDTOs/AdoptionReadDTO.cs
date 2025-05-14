using SimpleWebDal.DTOs.CalendarDTOs;

namespace SimpleWebDal.DTOs.AdoptionDTOs;

public class AdoptionReadDTO
{
    public Guid Id { get; set; }
    public Guid PetId { get; set; }
    public Guid UserId { get; set; }
    public bool IsPreAdoptionPoll { get; set; }
    public string PreadoptionPoll { get; set; }
    public Guid? CalendarId { get; set; }
    public CalendarActivityReadDTO Activity { get; set; }
    public bool IsMeetings { get; set; }
    public bool IsContractAdoption { get; set; }
    public string? ContractAdoption { get; set; }
    public DateTimeOffset? DateOfAdoption { get; set; }
}
