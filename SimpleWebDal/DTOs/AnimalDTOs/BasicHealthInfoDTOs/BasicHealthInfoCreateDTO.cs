using SimpleWebDal.DTOs.AnimalDTOs.DiseaseDTOs;
using SimpleWebDal.DTOs.AnimalDTOs.VaccinationDTOs;
using SimpleWebDal.Models.Animal.Enums;

namespace SimpleWebDal.DTOs.AnimalDTOs.BasicHealthInfoDTOs;

public class BasicHealthInfoCreateDTO
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Size Size { get; set; }
    public bool IsNeutered { get; set; }
   
}
