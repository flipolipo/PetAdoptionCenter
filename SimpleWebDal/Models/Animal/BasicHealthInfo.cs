using SimpleWebDal.Models.Animal.Enums;

namespace SimpleWebDal.Models.Animal;

public class BasicHealthInfo
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public Size Size { get; set; }
    public bool IsNeutered { get; set; }
    public ICollection<Vaccination>? Vaccinations { get; set; }
    public ICollection<Disease>? MedicalHistory { get; set; }

}
