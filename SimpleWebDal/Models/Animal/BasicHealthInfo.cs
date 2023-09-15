using SimpleWebDal.Models.Animal.Enums;
using SimpleWebDal.Models.WebUser;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.Models.Animal;

public class BasicHealthInfo
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public Size Size { get; set; }
    public ICollection<Vaccination> Vaccinations { get; set; }
    public ICollection<Disease> MedicalHistory { get; set; }
    public int PetId { get; set; }
    public Pet Pet { get; set; }
}
