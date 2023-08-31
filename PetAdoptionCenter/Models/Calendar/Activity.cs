using PetAdoptionCenter.Models.Animal;
using PetAdoptionCenter.Models.WebUser;

namespace PetAdoptionCenter.Models.TimeTable
{
    public class Activity
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public DateTime AcctivityDate { get; set; }
        public Pet pet { get; set; }
        public User user { get; set; }
    }
}
