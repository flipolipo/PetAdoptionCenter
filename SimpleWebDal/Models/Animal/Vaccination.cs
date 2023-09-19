namespace SimpleWebDal.Models.Animal;

public class Vaccination
{
    public Guid Id { get; set; }
    public string VaccinationName { get; set; }
    public DateTime date { get; set; }
    public Guid BasicHealthInfoId { get; set; }
    public BasicHealthInfo? BasicHealthInfo { get; set; }
}
