using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleWebDal.Models.WebUser;

public class BasicInformation
{

    public int BasicInformationId { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }
    
    public Address Address { get; set; }

  

}
