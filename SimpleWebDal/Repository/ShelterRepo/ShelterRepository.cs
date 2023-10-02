using Microsoft.EntityFrameworkCore;
using SimpleWebDal.Data;
using SimpleWebDal.Models.AdoptionProccess;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.Animal.Enums;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.TemporaryHouse;
using SimpleWebDal.Models.WebUser;
using SimpleWebDal.Models.WebUser.Enums;
using SImpleWebLogic.Repository.ShelterRepo;
using System.Text;

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
            var foundShelter = await _dbContext.Shelters.Include(x => x.ShelterCalendar)
                .ThenInclude(a => a.Activities).Include(y => y.ShelterAddress)
                .Include(b => b.ShelterUsers)
                .Include(c => c.Adoptions)
                .Include(d => d.TempHouses).ThenInclude(h => h.TemporaryOwner)
                .Include(d => d.TempHouses).ThenInclude(h => h.TemporaryHouseAddress)
                .Include(d => d.TempHouses).ThenInclude(h => h.PetsInTemporaryHouse)
                .Include(f => f.ShelterPets)
                .FirstOrDefaultAsync(e => e.Id == shelterId);
            return foundShelter;
        }
        public async Task<User> FindUserById(string userId)
        {
            var foundUser = await _dbContext.Users
            .Include(b => b.BasicInformation).ThenInclude(c => c.Address)
            .Include(d => d.Roles)
            .Include(e => e.UserCalendar).ThenInclude(f => f.Activities)
            .Include(g => g.Adoptions)
            .Include(h => h.Pets).FirstOrDefaultAsync(z => z.Id == userId);
            return foundUser;
        }
        private List<User> FilterUsersByRole(ICollection<User> users, RoleName roleName)
        {
            var filteredUsers = new List<User>();
            foreach (var user in users)
            {
                foreach (var role in user.Roles)
                {
                    if (role.Title.Equals(roleName))
                        filteredUsers.Add(user);
                }
            }
            return filteredUsers;
        }

        //TO FIX!!!!!!!
        public async Task<Activity> AddActivityToCalendar(Guid shelterId, Activity activity)
        {
            var foundShelter = await FindShelter(shelterId);
            
             foundShelter.ShelterCalendar.Activities.Add(activity);
            // _dbContext.Shelters.Update(foundShelter);
            //_dbContext.Add(activity);
            _dbContext.SaveChanges();
            return activity;
        }
        public async Task<bool> AddShelterUser(Guid shelterId, string userId, RoleName roleName)
        {
            var foundShelter = await FindShelter(shelterId);
            var foundUser = await _dbContext.Users.Include(r => r.Roles).FirstOrDefaultAsync(u => u.Id == userId);
            if (foundUser != null)
            {
                var role = new Role()
                {
                    Id = Guid.NewGuid(),
                    Title = roleName
                };
                foundUser.Roles.Add(role);
                foundShelter.ShelterUsers.Add(foundUser);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<Pet> AddPet(Guid shelterId, Pet pet)
        {
            var foundShelter = await FindShelter(shelterId);
            //var newpet = new Pet()
            //{
            //    Id = Guid.NewGuid(),
            //    Type = type,
            //    Description = description,
            //    Status = status,
            //    AvaibleForAdoption = avaibleForAdoption,
            //    Calendar = new CalendarActivity(),
            //    ShelterId = foundShelter.Id,
            //    PatronUsers = new List<PatronUsers>(),
            //    BasicHealthInfo = new BasicHealthInfo()
            //    {
            //        Name = name,
            //        Age = age,
            //        Size = size,
            //        Vaccinations = new List<Vaccination>(),
            //        MedicalHistory = new List<Disease>()
            //    }
            //};


            foundShelter.ShelterPets.Add(pet);
            //_dbContext.Pets.Add(pet);
            _dbContext.SaveChanges();
            return pet;
        }
        public async Task<BasicHealthInfo> AddBasicHelathInfoToAPet(Guid shelterId, Guid petId, string name, int age, Size size)
        {
            var foundShelter = await FindShelter(shelterId);
            var foundPet = foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId);
            var info = new BasicHealthInfo()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Age = age,
                Size = size
            };
            foundPet.BasicHealthInfo = info;
            return info;

        }
        public async Task<Vaccination> AddVaccinationToAPet(Guid shelterId, Guid petId, string vaccName, DateTime date)
        {
            var foundShelter = await FindShelter(shelterId);
            var foundPet = foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId);
            var vacc = new Vaccination()
            {
                Id = Guid.NewGuid(),
                Date = date,
                VaccinationName = vaccName
            };
            foundPet.BasicHealthInfo.Vaccinations.Add(vacc);
            return vacc;

        }
        public async Task<Disease> AddDiseaseHistoryToAPet(Guid shelterId, Guid petId, string name, DateTime start, DateTime end)
        {
            var foundShelter = await FindShelter(shelterId);
            var foundPet = foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId);
            var disease = new Disease()
            {
                Id = Guid.NewGuid(),
                NameOfdisease = name,
                IllnessEnd = end,
                IllnessStart = start,
            };
            foundPet.BasicHealthInfo.MedicalHistory.Add(disease);
            return disease;
        }
        public async Task<TempHouse> AddTempHouse(Guid shelterId, string userId, Guid petId, TempHouse tempHouse)
        {
            var foundShelter = await FindShelter(shelterId);
            var foundPetById = await GetShelterPetById(shelterId, petId);
            var foundUser = await _dbContext.Users
            .Include(b => b.BasicInformation).ThenInclude(c => c.Address)
            .Include(d => d.Roles)
            .Include(e => e.UserCalendar).ThenInclude(f => f.Activities)
            .Include(g => g.Adoptions)
            .Include(h => h.Pets).FirstOrDefaultAsync(e => e.Id == userId);
            var foundPet = await GetShelterPetById(shelterId, petId);
            tempHouse.TemporaryOwner = foundUser;
            tempHouse.TemporaryHouseAddress = foundUser.BasicInformation.Address;
            tempHouse.PetsInTemporaryHouse = new List<Pet>
            {
                foundPet
            };
            foundShelter.TempHouses.Add(tempHouse);
           // foundPet.Status = PetStatus.TemporaryHouse;
            _dbContext.SaveChanges();
            return tempHouse;

        }
        public async Task<bool> AddUserToShelter(Guid shelterId, string userId, RoleName role)
        {
            var foundShelter = await FindShelter(shelterId);
            var foundUser = await _dbContext.Users.FirstOrDefaultAsync(e => e.Id == userId);
            var newRole = new Role()
            {
                Title = role,
                Id = Guid.NewGuid()
            };
            if (foundUser != null)
            {
                foundUser.Roles.Add(newRole);
                foundShelter.ShelterUsers.Add(foundUser);
                return true;
            }
            return false;
        }

        public async Task<Shelter> CreateShelter(Shelter shelter)
        {
            shelter.TempHouses = new List<TempHouse>();
            _dbContext.Shelters.Add(shelter);


            _dbContext.SaveChanges();
            return shelter;
        }

        public async Task<bool> DeleteActivity(Guid shelterId, Guid activityId)
        {
            var foundShelter = await FindShelter(shelterId);
            var foundActivity = foundShelter.ShelterCalendar.Activities.FirstOrDefault(e => e.Id == activityId);

            if (foundActivity != null)
            {
                foundShelter.ShelterCalendar.Activities.Remove(foundActivity);
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteShelterUser(Guid shelterId, string userId)
        {
            var foundShelter = await FindShelter(shelterId);
            var user = foundShelter.ShelterUsers.FirstOrDefault(e => e.Id == userId);

            if (user != null)
            {
                foundShelter.ShelterUsers.Remove(user);
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteShelter(Guid shelterId)
        {
            var foundShelter = await FindShelter(shelterId);

            if (foundShelter != null)
            {
                _dbContext.Shelters.Remove(foundShelter);
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteShelterPet(Guid shelterId, Guid petId)
        {
            var foundShelter = await FindShelter(shelterId);
            var pet = foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId);

            if (pet != null)
            {
                foundShelter.ShelterPets.Remove(pet);
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteTempHouse(Guid tempHouseId, Guid shelterId)
        {
            var foundShelter = await FindShelter(shelterId);
            var temphouse = foundShelter.TempHouses.FirstOrDefault(e => e.Id == tempHouseId);

            if (temphouse != null)
            {
                foundShelter.TempHouses.Remove(temphouse);
                _dbContext.SaveChanges();
                return true;
            }

            return false;
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

        public async Task<IEnumerable<Pet>> GetAllShelterDogsOrCats(Guid shelterId, PetType type)
        {
            var foundShelter = await FindShelter(shelterId);
            var pets = new List<Pet>();
            foreach (var pet in foundShelter.ShelterPets)
            {
                if (pet.Type.Equals(type))
                    pets.Add(pet);
            }
            return pets;
        }

        public async Task<IEnumerable<Pet>> GetAllShelterPets(Guid shelterId)
        {
            var foundShelter = await FindShelter(shelterId);
            return foundShelter.ShelterPets.ToList();
        }

        public async Task<IEnumerable<Shelter>> GetAllShelters()
        {
            return await _dbContext.Shelters.Include(x => x.ShelterCalendar)
                 .ThenInclude(a => a.Activities).Include(y => y.ShelterAddress)
                .Include(b => b.ShelterUsers)
                .Include(c => c.Adoptions)
                .Include(d => d.TempHouses)
                .Include(f => f.ShelterPets)
                .ToListAsync();
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
        public async Task<IEnumerable<User>> GetShelterUsers(Guid shelterId)
        {
            var foundShelter = await FindShelter(shelterId);
            return foundShelter.ShelterUsers;
        }
        public async Task<IEnumerable<User>> GetShelterUsersByRole(Guid shelterId, RoleName role)
        {
            var foundShelter = await FindShelter(shelterId);
            var shelterUsers = foundShelter.ShelterUsers;

            var filteredUsers = FilterUsersByRole(shelterUsers, role);
            return filteredUsers;


        }

        public async Task<User> GetShelterUserById(Guid shelterId, string userId)
        {
            var foundShelter = await FindShelter(shelterId);
            var shelterUsers = foundShelter.ShelterUsers;

            return shelterUsers.FirstOrDefault(e => e.Id == userId);
        }

        public async Task<Pet> GetShelterPetById(Guid shelterId, Guid petId)
        {
            var foundShelter = await FindShelter(shelterId);
            return foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId);
        }

        public async Task<TempHouse> GetTempHouseById(Guid shelterId, Guid tempHouseId)
        {
            var foundShelter = await FindShelter(shelterId);
            return foundShelter.TempHouses.FirstOrDefault(e => e.Id == tempHouseId);
        }
        public async Task<bool> UpdateActivity(Guid shelterId, Guid activityId, string name, DateTime date)
        {
            var foundShelter = await FindShelter(shelterId);
            var foundActivity = foundShelter.ShelterCalendar.Activities.FirstOrDefault(e => e.Id == activityId);

            if (foundActivity != null)
            {
                foundActivity.ActivityDate = date;
                foundActivity.Name = name;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateShelter(Guid shelterId, string name, string description, string street, string houseNumber, string postalCode, string city)
        {
            var foundShelter = await FindShelter(shelterId);

            if (foundShelter != null)
            {
                foundShelter.ShelterDescription = description;
                foundShelter.Name = name;
                foundShelter.ShelterAddress.Street = street;
                foundShelter.ShelterAddress.PostalCode = postalCode;
                foundShelter.ShelterAddress.HouseNumber = houseNumber;
                foundShelter.ShelterAddress.City = city;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateShelterPet(Guid shelterId, Guid petId, PetType type, string description, PetStatus status, bool avaibleForAdoption)
        {
            var foundShelter = await FindShelter(shelterId);
            var foundPet = foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId);

            if (foundPet != null)
            {
                foundPet.AvaibleForAdoption = avaibleForAdoption;
                foundPet.Description = description;
                foundPet.Status = status;
                foundPet.Type = type;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdatePetBasicHealthInfo(Guid shelterId, Guid petId, string name, int age, Size size)
        {
            var foundShelter = await FindShelter(shelterId);
            var foundPet = foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId);

            if (foundPet != null)
            {
                var petHealthInfo = foundPet.BasicHealthInfo;
                petHealthInfo.Size = size;
                petHealthInfo.Age = age;
                petHealthInfo.Name = name;
                await _dbContext.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Activity>> GetShelterActivities(Guid shelterId)
        {
            var foundShelter = await FindShelter(shelterId);
            var activities = foundShelter.ShelterCalendar.Activities.ToList();
            return activities;
        }

        public async Task<Activity> GetShelterActivityById(Guid shelterId, Guid activityId)
        {
            var foundShelter = await FindShelter(shelterId);
            var activity = foundShelter.ShelterCalendar.Activities.FirstOrDefault(e => e.Id == activityId);
            return activity;
        }

        public async Task<IEnumerable<Disease>> GetAllPetDiseases(Guid shelterId, Guid petId)
        {
            var foundShelter = await FindShelter(shelterId);
            var foundPet = foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId);
            var diseases = foundPet.BasicHealthInfo.MedicalHistory;
            return diseases;
        }

        public async Task<Disease> GetPetDiseaseById(Guid shelterId, Guid petId, Guid diseaseId)
        {
            var foundShelter = await FindShelter(shelterId);
            var foundPet = foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId);
            var disease = foundPet.BasicHealthInfo.MedicalHistory.FirstOrDefault(e => e.Id == diseaseId);
            return disease;
        }

        public async Task<IEnumerable<Vaccination>> GetAllPetVaccinations(Guid shelterId, Guid petId)
        {
            var foundShelter = await FindShelter(shelterId);
            var foundPet = foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId);
            var vaccinations = foundPet.BasicHealthInfo.Vaccinations;
            return vaccinations;
        }

        public async Task<Disease> GetPetVaccinationById(Guid shelterId, Guid petId, Guid vaccinationId)
        {
            var foundShelter = await FindShelter(shelterId);
            var foundPet = foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId);
            var vaccination = foundPet.BasicHealthInfo.MedicalHistory.FirstOrDefault(e => e.Id == vaccinationId);
            return vaccination;
        }
        public async Task<Disease> AddPetDisease(Guid shelterId, Guid petId, string name, DateTime start, DateTime end)
        {
            var disease = new Disease()
            {
                Id = Guid.NewGuid(),
                NameOfdisease = name,
                IllnessStart = start,
                IllnessEnd = end,
            };
            var foundShelter = await FindShelter(shelterId);
            var foundPet = foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId);
            foundPet.BasicHealthInfo.MedicalHistory.Add(disease);
            return disease;
        }
        public async Task<Vaccination> AddPetVaccination(Guid shelterId, Guid petId, string name, DateTime date)
        {
            var vaccination = new Vaccination()
            {
                Id = Guid.NewGuid(),
                Date = date,
                VaccinationName = name,
            };
            var foundShelter = await FindShelter(shelterId);
            var foundPet = foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId);
            foundPet.BasicHealthInfo.Vaccinations.Add(vaccination);
            return vaccination;
        }


        public async Task<IEnumerable<Activity>> GetAllActivities(Guid shelterId)
        {
            var foundShelter = await FindShelter(shelterId);
            var activities = foundShelter.ShelterCalendar.Activities;
            return activities;
        }

        public async Task<Activity> GetActivityById(Guid shelterId, Guid activityId)
        {
            var foundShelter = await FindShelter(shelterId);
            var activity = foundShelter.ShelterCalendar.Activities.FirstOrDefault(e => e.Id == activityId);
            return activity;
        }
    }
}
