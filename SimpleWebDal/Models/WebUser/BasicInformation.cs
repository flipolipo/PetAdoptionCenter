namespace SimpleWebDal.Models.WebUser;

public class BasicInformation
{
    public BasicInformation()
    {
        Id = Guid.NewGuid();
        Name = "";
        Surname = "";
        Phone = "";
           }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public Guid? AddressId { get; set; }
    public Address? Address { get; set; }


}
