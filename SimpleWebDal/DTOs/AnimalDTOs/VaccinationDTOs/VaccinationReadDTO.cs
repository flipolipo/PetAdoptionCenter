namespace SimpleWebDal.DTOs.AnimalDTOs.VaccinationDTOs;

public class VaccinationReadDTO
{
    public Guid Id { get; set; }
    public string VaccinationName { get; set; }
    public DateTimeOffset Date { get; set; }
}
