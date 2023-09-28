using SimpleWebDal.DTOs.AddressDTOs;
using SimpleWebDal.DTOs.AnimalDTOs;
using SimpleWebDal.DTOs.WebUserDTOs;

namespace SimpleWebDal.DTOs.TemporaryHouseDTOs;

public class TempHouseCreateDTO
{
    //public UserCreateDTO TemporaryOwner { get; set; }
    //public AddressCreateDTO TemporaryHouseAddress { get; set; }
   // public ICollection<PetCreateDTO>? PetsInTemporaryHouse { get; set; }
    public DateTimeOffset StartOfTemporaryHouseDate { get; set; }
}
