using Microsoft.EntityFrameworkCore;
using SimpleWebDal.Data;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.Animal.Enums;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.TemporaryHouse;
using SimpleWebDal.Models.WebUser;
using SImpleWebLogic.Repository.ShelterRepo;

namespace SimpleWebDal.Repository.ShelterRepo
{
    public class ShelterRepository : IShelterRepository
    {
        private readonly PetAdoptionCenterContext _dbContext;
        public ShelterRepository(PetAdoptionCenterContext dbContext)
        {
            _dbContext = dbContext;
        }
        private async Task<Shelter> FindShelter(Guid shelterId) 
        {
            var foundShelter = await _dbContext.Shelters.FirstOrDefaultAsync(e => e.Id == shelterId);
            return foundShelter;

        }
        private List<User> FilterUsersByRole(ICollection<User> users, string roleName) 
        {
            var filteredUsers = new List<User>();
            foreach (var user in users)
            {
                foreach (var role in user.Roles)
                {
                    if (role.RoleName.Equals(roleName))
                        filteredUsers.Add(user);
                }
            }
            return filteredUsers;
        }
        

        public async Task<Activity> AddActivityToCalendar(Guid shelterId, Activity activity)
        {
          var foundShelter = await _dbContext.Calendars.FirstOrDefaultAsync(c => c.Id == shelterId);
            foundShelter.Activities.Add(activity);
            return activity;
        }

        public async Task<User> AddContributor(Guid shelterId, Guid userId)
        {
            var foundShelter = await FindShelter(shelterId);
            var foundUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            foundShelter.ShelterUsers.Add(foundUser);
            return foundUser;
        }

        public async Task<Pet> AddPet(Guid shelterId, PetType type, string description, PetStatus status, bool avaibleForAdoption)
        {
            var foundShelter = await FindShelter(shelterId);
            var pet = new Pet() 
            {
                Type = type,
                Description = description,
                Status = status,
                AvaibleForAdoption = avaibleForAdoption
            };
            foundShelter.ShelterPets.Add(pet);
            return pet;
        }
        public async Task<BasicHealthInfo> AddBasicHelathInfoToAPet(Guid shelterId, Guid petId, string name, int age, Size size,  ) 
        {
            var foundShelter = await FindShelter(shelterId);
            var foundPet = foundShelter.ShelterPets.FirstOrDefault(e => e.Id == shelterId);
            var info = new BasicHealthInfo() 
            {

            }    
        }
        public Task<TempHouse> AddTempHouse(int shelterId)
        {
            throw new NotImplementedException();
        }

        public Task<User> AddWorker(int shelterId)
        {
            throw new NotImplementedException();
        }

        public Task<Shelter> CreateShelter()
        {
            throw new NotImplementedException();
        }

        public void DeleteCallendar(int callendarId, int shelterId)
        {
            throw new NotImplementedException();
        }

        public void DeleteContributor(int shelterId, int userId)
        {
            throw new NotImplementedException();
        }

        public void DeleteShelter(int shelterId)
        {
            throw new NotImplementedException();
        }

        public void DeleteShelterPet(int shelterId, int petId)
        {
            throw new NotImplementedException();
        }

        public void DeleteTempHouse(int tempHouseId, int shelterId)
        {
            throw new NotImplementedException();
        }

        public void DeleteWorker(int shelterId, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Pet> GetAdoptedPetsById(Guid shelterId, Guid petId)
        {
            var foundShelter = await FindShelter(shelterId);
            

            return foundPet;
        }

        public async Task<IEnumerable<Pet>> GetAllAdoptedPets(Guid shelterId)
        {
            var foundShelter = await FindShelter(shelterId);
            var adopted = new List<Pet>();
            foreach (var pet in foundShelter.ShelterPets)
            {
                if (pet.Status.Equals(PetStatus.Adopted)) 
                {
                    adopted.Add(pet);
                }
            }
            return adopted;
        }

        public Task<IEnumerable<Pet>> GetAllShelterDogsOrCats(int shelterId, PetType type)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Pet>> GetAllShelterPets(Guid shelterId)
        {
            var foundShelter = await FindShelter(shelterId);
            return foundShelter.ShelterPets.ToList();
        }

        public async Task<IEnumerable<Shelter>> GetAllShelters()
        {
            return await _dbContext.Shelters.ToListAsync();    
        }

        public async Task<IEnumerable<Pet>> GetAllShelterTempHousesPets(Guid shelterId)
        {
            var foundShelter = await FindShelter(shelterId);
            var pets = new List<Pet>();
            foreach (var tempHouse in foundShelter.TempHouses)
            {
                foreach (var pet in tempHouse.PetsInTemporaryHouse)
                {
                    pets.Add(pet);
                }
            }
            return pets.ToList();
        }

        public async Task<Pet> GetTempHousePetById(Guid shelterId, Guid tempHouseId, Guid petId)
        {
            var foundShelter = await FindShelter(shelterId);
            var foundTempHouse = foundShelter.TempHouses.FirstOrDefault(e => e.Id == tempHouseId);
            var pet = foundTempHouse.PetsInTemporaryHouse.FirstOrDefault(e => e.Id == petId);
            return pet;

        }

        public async Task<IEnumerable<TempHouse>> GetAllTempHouses(Guid shelterId)
        {
            var foundShelter = await FindShelter(shelterId);
            return foundShelter.TempHouses;
        }

        public async Task<Shelter> GetShelterById(Guid shelterId)
        {
            var foundShelter = await FindShelter(shelterId);
            return foundShelter;
        }

        public async Task<IEnumerable<User>> GetShelterContributors(Guid shelterId)
        {
            var foundShelter = await FindShelter(shelterId);
            var shelterUsers = foundShelter.ShelterUsers;
            var contributors = FilterUsersByRole(shelterUsers, "Contributor");
            return contributors;
            
        }

        public async Task<User> GetShelterContributorById(Guid shelterId, Guid workerId)
        {
            var foundShelter = await FindShelter(shelterId);
            var shelterUsers = foundShelter.ShelterUsers;
            var contributors = FilterUsersByRole(shelterUsers, "Contributor");
            return contributors.FirstOrDefault(e => e.Id == workerId);
        }

        public async Task<Pet> GetShelterPetById(Guid shelterId, Guid petId)
        {
            var foundShelter = await FindShelter(shelterId);
            return foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId);
        }

        public async Task<IEnumerable<User>> GetShelterWorkers(Guid shelterId)
        {
            var foundShelter = await FindShelter(shelterId);
            var shelterUsers = foundShelter.ShelterUsers;
            var contributors = FilterUsersByRole(shelterUsers, "Worker");
            return contributors;
        }

        public async Task<User> GetShelterWorkerById(Guid shelterId, Guid workerId)
        {
            var foundShelter = await FindShelter(shelterId);
            var shelterUsers = foundShelter.ShelterUsers;
            var contributors = FilterUsersByRole(shelterUsers, "Worker");
            return contributors.FirstOrDefault(e => e.Id == workerId);
        }

        public async Task<TempHouse> GetTempHouseById(Guid shelterId, Guid tempHouseId)
        {
            var foundShelter = await FindShelter(shelterId);
            return foundShelter.TempHouses.FirstOrDefault(e => e.Id == tempHouseId);
        }

        public async Task<Pet> GetTempHousePetById(Guid shelterId, Guid petId, Guid tempHouseId)
        {
            var foundShelter = await FindShelter(shelterId);
            var tempHouse = foundShelter.TempHouses.FirstOrDefault(e => e.Id == tempHouseId);
            return tempHouse.PetsInTemporaryHouse.FirstOrDefault(e => e.Id == petId);
        }

        public void UpdateCallendar(int shelterId, int callendarId)
        {
            throw new NotImplementedException();
        }

        public void UpdateShelter(int shelterId)
        {
            throw new NotImplementedException();
        }

        public void UpdateShelterPet(int shelterId, int petId)
        {
            throw new NotImplementedException();
        }
    }

}
