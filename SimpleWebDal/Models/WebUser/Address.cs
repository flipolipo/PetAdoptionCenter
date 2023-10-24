namespace SimpleWebDal.Models.WebUser;

public class Address
{

    public Address()
    {

        Id = Guid.NewGuid();
        Street = "";
        HouseNumber = "";
        FlatNumber = 0;
        PostalCode = "";
        City = "";

    }

    public Guid Id { get; set; }
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public int FlatNumber { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
}