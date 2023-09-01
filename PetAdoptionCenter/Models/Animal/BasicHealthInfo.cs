using PetAdoptionCenter.Models.Animal.Enums;

namespace PetAdoptionCenter.Models.Animal;

public class BasicHealthInfo
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Size Size { get; set; }
    public IEnumerable<Vaccination> Vaccinations { get; set; }
    public IEnumerable<Disease> MedicalHistory { get; set; }
}
