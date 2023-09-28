using SimpleWebDal.DTOs.AnimalDTOs;

namespace SimpleWebDal.DTOs.AdoptionDTOs;

public class AdoptionReadDTO
{
    public Guid Id { get; set; }
    public bool PreAdoptionPoll { get; set; }
    public bool ContractAdoption { get; set; }
    public bool Meetings { get; set; }
    public DateTime? DateOfAdoption { get; set; }
}
