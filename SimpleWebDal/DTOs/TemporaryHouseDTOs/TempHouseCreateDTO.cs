using SimpleWebDal.DTOs.AddressDTOs;
using SimpleWebDal.DTOs.AnimalDTOs;
using SimpleWebDal.DTOs.ShelterDTOs;
using SimpleWebDal.DTOs.WebUserDTOs;

namespace SimpleWebDal.DTOs.TemporaryHouseDTOs;

public class TempHouseCreateDTO
{
    public Guid UserId { get; set; }
    public UserCreateDTO TemporaryOwner { get; set; }
    public Guid AddressId { get; set; }
    public AddressCreateDTO TemporaryHouseAddress { get; set; }
    public ICollection<PetCreateDTO> PetsInTemporaryHouse { get; set; }
    public Guid ShelterId { get; set; }
    public ShelterCreateDTO ShelterName { get; set; }
    public DateTime StartOfTemporaryHouseDate { get; set; }
}
