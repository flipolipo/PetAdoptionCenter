using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.Models.Animal;

public class Vaccination
{

public int VaccinationId { get; set; }
    public string VaccinationName { get; set; }
   
    public DateTime date { get; set; }


}
