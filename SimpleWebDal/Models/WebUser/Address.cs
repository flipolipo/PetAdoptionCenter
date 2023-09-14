
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.TemporaryHouse;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace SimpleWebDal.Models.WebUser;

public class Address
{

    public int AddressId { get; set; }

    public string Street { get; set; }

    public string HouseNumber { get; set; }


    public int FlatNumber { get; set; }

    public string PostalCode { get; set; }

    public string City { get; set; }




}