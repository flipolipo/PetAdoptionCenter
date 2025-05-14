using SimpleWebDal.DTOs.AddressDTOs;
using SimpleWebDal.DTOs.AnimalDTOs;
using SimpleWebDal.DTOs.CalendarDTOs;
using SimpleWebDal.DTOs.WebUserDTOs;

namespace SimpleWebDal.DTOs.TemporaryHouseDTOs;

public class TempHouseCreateDTO
{
    //public UserCreateDTO TemporaryOwner { get; set; }
    //public AddressCreateDTO TemporaryHouseAddress { get; set; }
    // public ICollection<PetCreateDTO>? PetsInTemporaryHouse { get; set; }
    public bool IsPreTempHousePoll { get; set; }
    public string TempHousePoll { get; set; }
    //public Guid? CalendarId { get; set; }
    public CalendarActivityCreateDTO? Activity { get; set; }
    //public bool IsMeetings { get; set; }
    //public DateTimeOffset StartOfTemporaryHouseDate { get; set; }
}
