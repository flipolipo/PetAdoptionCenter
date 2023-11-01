using Microsoft.AspNetCore.Http;
using SimpleWebDal.DTOs.AddressDTOs;
using SimpleWebDal.DTOs.AdoptionDTOs;
using SimpleWebDal.DTOs.AnimalDTOs;
using SimpleWebDal.DTOs.CalendarDTOs;
using SimpleWebDal.DTOs.TemporaryHouseDTOs;
using SimpleWebDal.DTOs.WebUserDTOs;

namespace SimpleWebDal.DTOs.ShelterDTOs;

public class ShelterCreateDTO
{
    public string Name { get; set; }
    public AddressCreateDTO ShelterAddress { get; set; }
    public string ShelterDescription { get; set; }
    public string PhoneNumber { get; set; }
    public IFormFile ImageFile { get; set; }
    
}
