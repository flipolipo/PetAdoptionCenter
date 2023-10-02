using SimpleWebDal.DTOs.AddressDTOs;

namespace SimpleWebDal.DTOs.WebUserDTOs.BasicInformationDTOs;

public class BasicInformationCreateDTO
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }

    public AddressCreateDTO Address { get; set; }
}
