namespace SimpleWebDal.Models.Animal;

public class Disease
{
   public Guid Id { get; set; }
    public string NameOfdisease { get; set; }
    public DateTime IllnessStart { get; set; }
    public DateTime IllnessEnd { get; set; }
}
