using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.Models.Animal;

public class Vaccination
{
    public int Id { get; set; }
    public string VaccinationName { get; set; }
    public DateTime date { get; set; }
    public int BasicHealthInfoId { get; set; }
    public BasicHealthInfo? BasicHealthInfo { get; set; }
}
