namespace SimpleWebDal.Models.Animal;

public class Vaccination
{
    public Guid Id { get; set; }
    public string VaccinationName { get; set; }
    public DateTimeOffset Date { get; set; }

}
