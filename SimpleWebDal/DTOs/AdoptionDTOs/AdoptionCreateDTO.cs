using SimpleWebDal.DTOs.CalendarDTOs;

namespace SimpleWebDal.DTOs.AdoptionDTOs;

public class AdoptionCreateDTO
{
    public bool IsPreAdoptionPoll { get; set; }
    public string PreadoptionPoll { get; set; }
    public CalendarActivityCreateDTO Activity { get; set; }
    //public bool IsMeetings { get; set; }
    //public bool IsContractAdoption { get; set; }
    //public string? ContractAdoption { get; set; }
    //public DateTimeOffset? DateOfAdoption { get; set; }
}
