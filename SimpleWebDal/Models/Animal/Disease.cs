using SimpleWebDal.Models.PetShelter;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebDal.Models.Animal;

public class Disease
{
   public int DiseaseId { get; set; }
    public string NameOfdisease { get; set; }
    
    public DateTime IllnessStart { get; set; }
    
    public DateTime IllnessEnd { get; set; }

    //Navigation properties:

    public int BasicHealthInfoId { get; set; }
    public BasicHealthInfo BasicHealthInfo { get; set; }

}
