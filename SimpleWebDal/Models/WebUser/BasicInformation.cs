namespace SimpleWebDal.Models.WebUser;

public class BasicInformation
{
    public int BasicInformationId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public int AddressId { get; set; }
    public Address Address { get; set; }
}
