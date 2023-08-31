namespace PetAdoptionCenter.Models.WebUser
{
    public class BasicInformation
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public Address address { get; set; }
    }
}
