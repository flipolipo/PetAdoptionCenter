using SimpleWebDal.DTOs.AddressDTOs;
using SimpleWebDal.DTOs.AnimalDTOs;
using SimpleWebDal.DTOs.WebUserDTOs;
using SimpleWebDal.Models.CalendarModel;

namespace SimpleWebDal.DTOs.TemporaryHouseDTOs;

public class TempHouseReadDTO
{
    public Guid Id { get; set; }
    public UserReadDTO TemporaryOwner { get; set; }
    public AddressReadDTO? TemporaryHouseAddress { get; set; }
    public ICollection<PetReadDTO>? PetsInTemporaryHouse { get; set; }
    public bool IsPreTempHousePoll { get; set; }
    public string TempHousePoll { get; set; }
    public Guid? CalendarId { get; set; }
    public CalendarActivity? Activity { get; set; }
    public bool IsMeetings { get; set; }
    //public DateTimeOffset StartOfTemporaryHouseDate { get; set; }
}

