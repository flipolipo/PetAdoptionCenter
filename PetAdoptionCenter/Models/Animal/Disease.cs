using System.Formats.Asn1;

namespace PetAdoptionCenter.Models.Animal;

public class Disease
{
    public string NameOfdisease { get; set; }
    public DateTime IllnessStart { get; set; }
    public DateTime IllnessEnd { get; set; }

}
