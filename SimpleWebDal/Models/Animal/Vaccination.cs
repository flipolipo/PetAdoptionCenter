using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.Models.Animal;

public class Vaccination
{

public int VaccinationId { get; set; }
    public string VaccinationName { get; set; }
   
    public DateTime date { get; set; }

    //Navigation properties:

    public int BasicHealthInfoId { get; set; }
    public BasicHealthInfo BasicHealthInfo { get; set; }
}
