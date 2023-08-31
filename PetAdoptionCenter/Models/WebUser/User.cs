
using PetAdoptionCenter.Models.TimeTable;
using System.Data;

namespace PetAdoptionCenter.Models.WebUser
{
    public class User
    {
        public uint Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public BasicInformation BasicInformation { get; set; }
        public TimeTable<User> usersTimeTable { get; set; }
        public IEnumerable<Role> roles;
    }
}
