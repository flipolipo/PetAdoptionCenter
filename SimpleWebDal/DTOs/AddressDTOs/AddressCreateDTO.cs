namespace SimpleWebDal.DTOs.AddressDTOs;

public class AddressCreateDTO
{
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public int? FlatNumber { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
}
