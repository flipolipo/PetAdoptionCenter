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
            if (foundShelter == null)
            {
                throw new InvalidOperationException($"Shelter with ID {shelterId} not found.");
            }
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


        public async Task<Activity> AddActivityToCalendar(Guid shelterId, string activityName, DateTime activityDate)
        {
            try
            {
                var foundShelter = await FindShelter(shelterId);
                if (foundShelter != null && foundShelter.ShelterCalendar != null)
                {

                    var activity = new Activity()
                    {
                        Id = Guid.NewGuid(),
                        Name = activityName,
                        ActivityDate = activityDate
                    };
                    foundShelter.ShelterCalendar.Activities.Add(activity);
                    return activity;
                }
                else
                {
                    throw new InvalidOperationException($"Shelter with ID {shelterId} not found.");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> AddContributor(Guid shelterId, Guid userId)
        {
            var foundShelter = await FindShelter(shelterId);
            var foundUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (foundUser != null)
            {
                foundShelter.ShelterUsers.Add(foundUser);
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<Pet> AddPet(Guid shelterId, PetType type, string description, PetStatus status, bool avaibleForAdoption)
        {
            var foundShelter = await FindShelter(shelterId);
            var pet = new Pet()
            {
                Id = Guid.NewGuid(),
                Type = type,
                Description = description,
                Status = status,
                AvaibleForAdoption = avaibleForAdoption,
                Calendar = new CalendarActivity()

            };
            foundShelter.ShelterPets.Add(pet);
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
        public async Task<TempHouse> AddTempHouse(Guid shelterId, Guid userId, DateTime startDate)
        {
            var foundShelter = await FindShelter(shelterId);
            var foundUser = await _dbContext.Users.FirstOrDefaultAsync(e => e.Id == userId);
            var userAddress = foundUser.BasicInformation.Address;
            var userAddressId = userAddress.Id;
            var tempHouse = new TempHouse()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                TemporaryOwner = foundUser,
                AddressId = userAddressId,
                TemporaryHouseAddress = userAddress,
                StartOfTemporaryHouseDate = startDate,
            };
            return tempHouse;

        }

        public async Task<bool> AddWorker(Guid shelterId, Guid userId)
        {
            var foundShelter = await FindShelter(shelterId);
            var foundUser = await _dbContext.Users.FirstOrDefaultAsync(e => e.Id == userId);
            if (foundUser != null)
            {
                var role = new Role()
                {
                    Id = Guid.NewGuid(),
                    RoleName = "Worker"
                };

                foundUser.Roles.Add(role);
                foundShelter.ShelterUsers.Add(foundUser);
                return true;
            }
            return false;
        }

        public async Task<Shelter> CreateShelter(string name, string description, string street, string houseNumber, string postalCode, string city)
        {
            var shelter = new Shelter()
            {
                Id = Guid.NewGuid(),
                Name = name,
                ShelterCalendar = new CalendarActivity() { DateWithTime = DateTime.UtcNow },
                ShelterDescription = description,
                ShelterAddress = new Address()
                {
                    Id = Guid.NewGuid(),
                    City = city,
                    Street = street,
                    HouseNumber = houseNumber,
                    PostalCode = postalCode
                },
            };
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

        public async Task<bool> DeleteContributor(Guid shelterId, Guid userId)
        {
            var foundShelter = await FindShelter(shelterId);
            var contributor = foundShelter.ShelterUsers.FirstOrDefault(e => e.Id == userId);

            if (contributor != null)
            {
                foundShelter.ShelterUsers.Remove(contributor);
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

        public async Task<bool> DeleteWorker(Guid shelterId, Guid userId)
        {
            var foundShelter = await FindShelter(shelterId);
            var contributor = foundShelter.ShelterUsers.FirstOrDefault(e => e.Id == userId);

            if (contributor != null)
            {
                foundShelter.ShelterUsers.Remove(contributor);
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
    }
}
