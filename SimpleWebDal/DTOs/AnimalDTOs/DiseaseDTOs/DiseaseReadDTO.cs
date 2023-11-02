namespace SimpleWebDal.DTOs.AnimalDTOs.DiseaseDTOs;

public class DiseaseReadDTO
{
    public string NameOfdisease { get; set; }
    public DateTimeOffset IllnessStart { get; set; }
    public DateTimeOffset IllnessEnd { get; set; }
}
