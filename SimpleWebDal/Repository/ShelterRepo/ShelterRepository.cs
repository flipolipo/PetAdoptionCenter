using Microsoft.EntityFrameworkCore;
using SimpleWebDal.Data;
using SimpleWebDal.Exceptions.UserRepository;
using SimpleWebDal.Models.AdoptionProccess;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.Animal.Enums;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.TemporaryHouse;
using SimpleWebDal.Models.WebUser;
using SimpleWebDal.Models.WebUser.Enums;
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
            var foundShelter = await _dbContext.Shelters.Include(x => x.ShelterCalendar)
                .ThenInclude(a => a.Activities)
                .Include(y => y.ShelterAddress)
                .Include(b => b.ShelterUsers).ThenInclude(r => r.BasicInformation).ThenInclude(a => a.Address)
                .Include(b => b.ShelterUsers).ThenInclude(r => r.Roles)
                .Include(c => c.Adoptions).ThenInclude(a => a.Activity).ThenInclude(a => a.Activities)
                .Include(d => d.TempHouses).ThenInclude(h => h.TemporaryOwner)
                .Include(d => d.TempHouses).ThenInclude(h => h.TemporaryHouseAddress)
                .Include(d => d.TempHouses).ThenInclude(h => h.PetsInTemporaryHouse).ThenInclude(u => u.Users)
                 .Include(d => d.TempHouses).ThenInclude(h => h.PetsInTemporaryHouse).ThenInclude(a => a.BasicHealthInfo)
                .Include(f => f.ShelterPets).ThenInclude(h => h.BasicHealthInfo).ThenInclude(v => v.Vaccinations)
                .Include(f => f.ShelterPets).ThenInclude(h => h.BasicHealthInfo).ThenInclude(v => v.MedicalHistory)
                .Include(f => f.ShelterPets).ThenInclude(h => h.Calendar).ThenInclude(a => a.Activities)

                .FirstOrDefaultAsync(e => e.Id == shelterId);
            return foundShelter;
        }
        public async Task<User> FindUserById(Guid userId)
        {
            var foundUser = await _dbContext.Users
            .Include(b => b.BasicInformation).ThenInclude(c => c.Address)
            .Include(d => d.Roles)
            .Include(e => e.UserCalendar).ThenInclude(f => f.Activities)
            .Include(g => g.Adoptions)
            .Include(h => h.Pets).FirstOrDefaultAsync(u => u.Id == userId);
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


        public async Task<Activity> AddActivityToCalendar(Guid shelterId, Activity activity)
        {
            var foundShelter = await FindShelter(shelterId);

            foundShelter.ShelterCalendar.Activities.Add(activity);

            _dbContext.SaveChanges();
            return activity;
        }
        public async Task<bool> AddShelterUser(Guid shelterId, Guid userId, Role role)
        {
            var foundShelter = await FindShelter(shelterId);
            var foundUser = await FindUserById(userId);
            if (foundShelter != null && foundUser != null)
            {

                foundUser.Roles.Add(role);
                foundShelter.ShelterUsers.Add(foundUser);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Pet> AddPet(Guid shelterId, Pet pet)
        {
            var foundShelter = await FindShelter(shelterId);
            pet.Calendar = new CalendarActivity();
            pet.Users = new List<User>();
            foundShelter.ShelterPets.Add(pet);
            await _dbContext.SaveChangesAsync();
            return pet;
        }
        public async Task<BasicHealthInfo> AddBasicHelathInfoToAPet(Guid shelterId, Guid petId, string name, int age, Size size, bool isNeutred)
        {
            var foundShelter = await FindShelter(shelterId);
            var foundPet = foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId);
            var info = new BasicHealthInfo()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Age = age,
                Size = size,
                IsNeutered = isNeutred
            };
            foundPet.BasicHealthInfo = info;
            await _dbContext.SaveChangesAsync();

            return info;

        }


        public async Task<bool> AddUserToShelter(Guid shelterId, Guid userId, RoleName role)
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
            shelter.ShelterCalendar = new CalendarActivity();
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

        public async Task<bool> DeleteShelterUser(Guid shelterId, Guid userId)
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

            return foundShelter.TempHouses
                .SelectMany(tempHouse => tempHouse.PetsInTemporaryHouse)
                .ToList();
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

        public async Task<User> GetShelterUserById(Guid shelterId, Guid userId)
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

        public async Task<Pet> GetPetById(Guid id)
        {
            return await _dbContext.Pets.Include(p => p.BasicHealthInfo).ThenInclude(p => p.Vaccinations)
                .Include(p => p.BasicHealthInfo).ThenInclude(p => p.MedicalHistory)
                .Include(p => p.Calendar).ThenInclude(p => p.Activities)
                .Include(p => p.Users)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<TempHouse> GetTempHouseById(Guid shelterId, Guid tempHouseId)
        {
            var foundShelter = await FindShelter(shelterId);
            return foundShelter.TempHouses.FirstOrDefault(e => e.Id == tempHouseId);
        }
        public async Task<bool> UpdateShelterActivity(Guid shelterId, Activity activity)
        {
            var foundShelter = await FindShelter(shelterId);
            var foundActivity = foundShelter.ShelterCalendar.Activities.FirstOrDefault(e => e.Id == activity.Id);

            if (foundShelter != null && foundActivity != null)
            {
                foundActivity.Name = activity.Name;
                foundActivity.StartActivityDate = activity.StartActivityDate;
                foundActivity.EndActivityDate = activity.EndActivityDate;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateShelter(Guid shelterId, string name, string description, string street, string houseNumber, string postalCode, string city, string phone)
        {
            var foundShelter = await FindShelter(shelterId);

            if (foundShelter != null)
            {
                foundShelter.ShelterDescription = description;
                foundShelter.Name = name;
                foundShelter.PhoneNumber = phone;
                foundShelter.ShelterAddress.Street = street;
                foundShelter.ShelterAddress.PostalCode = postalCode;
                foundShelter.ShelterAddress.HouseNumber = houseNumber;
                foundShelter.ShelterAddress.City = city;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateShelterPet(Guid shelterId, Guid petId, PetGender gender, PetType type, string description, PetStatus status, bool avaibleForAdoption)
        {
            var foundShelter = await FindShelter(shelterId);
            var foundPet = foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId);

            if (foundPet != null)
            {
                foundPet.AvaibleForAdoption = avaibleForAdoption;
                foundPet.Gender = gender;
                foundPet.Description = description;
                foundPet.Status = status;
                foundPet.Type = type;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdatePetBasicHealthInfo(Guid shelterId, Guid petId, string name, int age, Size size, bool isNeutred)
        {
            var foundShelter = await FindShelter(shelterId);
            var foundPet = foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId);

            if (foundPet != null)
            {
                var petHealthInfo = foundPet.BasicHealthInfo;
                petHealthInfo.Size = size;
                petHealthInfo.Age = age;
                petHealthInfo.Name = name;
                petHealthInfo.IsNeutered = isNeutred;
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
        public async Task<Disease> AddPetDisease(Guid shelterId, Guid petId, Disease disease)
        {

            var foundShelter = await FindShelter(shelterId);
            var foundPet = foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId);
            foundPet.BasicHealthInfo.MedicalHistory.Add(disease);
            _dbContext.SaveChanges();
            return disease;
        }
        public async Task<Vaccination> AddPetVaccination(Guid shelterId, Guid petId, Vaccination vaccination)
        {
            var foundShelter = await FindShelter(shelterId);
            var foundPet = foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId);
            foundPet.BasicHealthInfo.Vaccinations.Add(vaccination);
            _dbContext.SaveChanges();
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

        public async Task<IEnumerable<Adoption>> GetAllShelterAdoptions(Guid shelterId)
        {
            var foundShelter = await FindShelter(shelterId);
            return foundShelter.Adoptions.ToList();
        }

        public async Task<Adoption> GetShelterAdoptionById(Guid shelterId, Guid adoptionId)
        {
            var foundShelter = await FindShelter(shelterId);
            var adoption = foundShelter.Adoptions.FirstOrDefault(e => e.Id == adoptionId);
            return adoption;

        }
        public async Task<Adoption> InitializePetAdoption(Guid shelterId, Guid petId, Guid userId, Adoption adoption)
        {
            if (userId == Guid.Empty)
            {
                throw new UserValidationException("User ID cannot be empty.");
            }

            if (adoption != null)
            {
                var foundShelter = await FindShelter(shelterId);
                var pet = await GetPetById(petId);
                var foundPet = await GetShelterPetById(shelterId, pet.Id);
                var foundUser = await FindUserById(userId);
                if (foundShelter != null && foundPet != null && foundUser != null && adoption.PreadoptionPoll != null && foundPet.AvaibleForAdoption == true)
                {
                    adoption.PetId = foundPet.Id;
                    adoption.UserId = foundUser.Id;
                    adoption.IsPreAdoptionPoll = true;
                    adoption.Activity = new CalendarActivity();
                    adoption.ContractAdoption = "";
                    adoption.DateOfAdoption = new DateTimeOffset().ToUniversalTime();
                    foundShelter.Adoptions.Add(adoption);
                    foundUser.Adoptions.Add(adoption);
                    foundUser.Pets.Add(foundPet);
                    foundPet.AvaibleForAdoption = false;
                    foundPet.Status = PetStatus.OnAdoptionProccess;
                    foundPet.Users.Add(foundUser);
                    await _dbContext.SaveChangesAsync();
                    return adoption;
                }
            }

            return null;
        }

        public async Task<Adoption> ChooseMeetingDatesForAdoption(Guid shelterId, Guid petId, Guid userId, Guid adoptionId, Guid activityId)
        {
            if (userId == Guid.Empty)
            {
                throw new UserValidationException("User ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId);
            var pet = await GetPetById(petId);
            var foundPet = await GetShelterPetById(shelterId, pet.Id);
            var foundUser = await FindUserById(userId);
            var foundAdoption = await GetShelterAdoptionById(shelterId, adoptionId);
            var foundActivity = await GetPetActivityById(foundShelter.Id, activityId, foundPet.Id);
            if (foundShelter != null && foundPet != null && foundUser != null && foundAdoption != null)
            {
                if (foundAdoption.PetId == foundPet.Id && foundAdoption.UserId == userId && foundAdoption.IsPreAdoptionPoll == true && foundAdoption.PreadoptionPoll != null && foundActivity != null)
                {
                    foundAdoption.Activity.Activities.Add(foundActivity);
                    foundPet.Calendar.Activities.Remove(foundActivity);
                    await _dbContext.SaveChangesAsync();
                    return foundAdoption;
                }
            }

            return null;
        }

        public async Task<Adoption> PetAdoptionMeetingsDone(Guid adoptionId)
        {
            var foundAdoption = await GetAdoptionFromDataBaseById(adoptionId);

            if (foundAdoption.IsPreAdoptionPoll == true && foundAdoption.PreadoptionPoll != null)
            {
                if (foundAdoption.Activity.Activities.Count >= 1)
                {
                    foreach (var activityEnd in foundAdoption.Activity.Activities)
                    {
                        if (activityEnd.EndActivityDate < DateTimeOffset.Now.ToUniversalTime())
                        {
                            foundAdoption.IsMeetings = true;
                            await _dbContext.SaveChangesAsync();
                            return foundAdoption;
                        }
                    }

                }
            }

            return null;
        }

        public async Task<Adoption> ContractForPetAdoption(Guid shelterId, Guid petId, Guid userId, Guid adoptionId, string contractAdoption)
        {
            if (userId == Guid.Empty)
            {
                throw new UserValidationException("User ID cannot be empty.");
            }

            var foundShelter = await FindShelter(shelterId);
            var pet = await GetPetById(petId);
            var foundPet = await GetShelterPetById(shelterId, pet.Id);
            var foundUser = await FindUserById(userId);
            var foundAdoption = await GetAdoptionFromDataBaseById(adoptionId);
            if (foundShelter != null && foundPet != null && foundUser != null && foundAdoption != null)
            {
                if (foundAdoption.PetId == foundPet.Id && foundAdoption.UserId == userId && foundAdoption.IsPreAdoptionPoll == true && foundAdoption.PreadoptionPoll != null && foundAdoption.IsMeetings == true)
                {
                    if (foundAdoption.ContractAdoption != null)
                    {
                        foundAdoption.IsContractAdoption = true;
                        foundAdoption.ContractAdoption = contractAdoption;
                        foundPet.Status = PetStatus.Adopted;
                        foundAdoption.DateOfAdoption = DateTimeOffset.Now.ToUniversalTime();
                        await _dbContext.SaveChangesAsync();
                        return foundAdoption;
                    }
                }

            }
            return null;
        }
        public async Task<bool> DeleteAdoption(Guid shelterId, Guid adoptionId, Guid petId, Guid userId)
        {
            var foundShelter = await FindShelter(shelterId);
            var pet = await GetPetById(petId);
            var foundPet = await GetShelterPetById(shelterId, pet.Id);
            var foundUser = await FindUserById(userId);
            var foundAdoption = await GetAdoptionFromDataBaseById(adoptionId);
            if (foundShelter != null && pet != null && foundPet != null && foundUser != null && foundAdoption != null)
            {
                foundShelter.Adoptions.Remove(foundAdoption);
                foundUser.Adoptions.Remove(foundAdoption);
                foundPet.Status = PetStatus.AtShelter;
                foundPet.AvaibleForAdoption = true;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;

        }
        private async Task<bool> CheckIfUserHaveTempHouse(Guid userId)
        {
            var foundTempHouseByUserId = await _dbContext.TempHouses.FirstOrDefaultAsync(u => u.UserId == userId);
            if (foundTempHouseByUserId == null)
            {
                return false;
            }
            return true;
        }
        public async Task<TempHouse> InitializeTempHouseForPet(Guid shelterId, Guid userId, Guid petId, TempHouse tempHouse)
        {
            var foundShelter = await FindShelter(shelterId);
            var pet = await GetPetById(petId);
            var foundPet = await GetShelterPetById(shelterId, pet.Id);
            var foundUser = await FindUserById(userId);
            var checkIfUserHaveTempHouse = await CheckIfUserHaveTempHouse(userId);
            if (foundShelter != null && foundPet != null && foundUser != null && !checkIfUserHaveTempHouse)
            {
                if (foundPet.Status != PetStatus.OnAdoptionProccess && foundPet.Status != PetStatus.Adopted && foundPet.Status != PetStatus.TemporaryHouse)
                {
                    tempHouse.TemporaryOwner = foundUser;
                    tempHouse.TemporaryHouseAddress = foundUser.BasicInformation.Address;
                    tempHouse.PetsInTemporaryHouse = new List<Pet> { foundPet };
                    tempHouse.IsPreTempHousePoll = true;
                    tempHouse.Activity = new CalendarActivity();
                    tempHouse.StartOfTemporaryHouseDate = new DateTimeOffset().ToUniversalTime();
                    foundPet.Status = PetStatus.TemporaryHouse;
                    foundUser.Pets.Add(foundPet);
                    foundShelter.TempHouses.Add(tempHouse);
                    await _dbContext.SaveChangesAsync();
                    return tempHouse;
                }
            }
            return null;
        }
        private async Task<Pet> GetPetInTempHouse(Guid tempHouse, Guid petId)
        {
            var foundTempHouse = await GetTempHouse(tempHouse);
            return foundTempHouse.PetsInTemporaryHouse.FirstOrDefault(p => p.Id == petId);
        }

        private async Task<Activity> GetPetActivity (Guid petId, Guid activityId)
        {
            var foundPet = await GetPetById(petId);
            return foundPet.Calendar.Activities.FirstOrDefault(a => a.Id == activityId);
        }
        public async Task<TempHouse> ChooseMeetingDatesForTempHouseProcess(Guid petId, Guid tempHouseId, Guid activityId)
        {
            var foundTempHouse = await GetTempHouse(tempHouseId);
            var pet = await GetPetById(petId);
            var foundPetInTempHouse = await GetPetInTempHouse(foundTempHouse.Id, pet.Id);
            var foundActivity = await GetPetActivity(foundPetInTempHouse.Id, activityId);
            if (foundTempHouse != null && foundPetInTempHouse != null && foundActivity != null)
            {
                if (foundTempHouse.IsPreTempHousePoll == true && foundTempHouse.TempHousePoll != null)
                {
                    foundTempHouse.Activity.Activities.Add(foundActivity);
                    foundPetInTempHouse.Calendar.Activities.Remove(foundActivity);
                    await _dbContext.SaveChangesAsync();
                    return foundTempHouse;
                }
            }

            return null;
        }
        public async Task<TempHouse> GetTempHouse(Guid tempHouseId)
        {
            return await _dbContext.TempHouses
                .Include(t => t.TemporaryOwner).ThenInclude(u => u.BasicInformation).ThenInclude(a => a.Address)
                .Include(t => t.TemporaryOwner).ThenInclude(u => u.UserCalendar).ThenInclude(a => a.Activities)
                .Include(t => t.TemporaryOwner).ThenInclude(u => u.Roles)
                .Include(t => t.TemporaryOwner).ThenInclude(u => u.Adoptions)
                .Include(t => t.PetsInTemporaryHouse).ThenInclude(p => p.BasicHealthInfo).ThenInclude(v => v.Vaccinations)
                .Include(t => t.PetsInTemporaryHouse).ThenInclude(p => p.BasicHealthInfo).ThenInclude(d => d.MedicalHistory)
                .Include(t => t.PetsInTemporaryHouse).ThenInclude(c => c.Calendar).ThenInclude(a => a.Activities)
                .Include(t => t.PetsInTemporaryHouse).ThenInclude(u => u.Users).ThenInclude(b => b.BasicInformation).ThenInclude(a => a.Address)
                .Include(t => t.Activity).ThenInclude(a => a.Activities)
                .Include(t => t.TemporaryHouseAddress)
                .FirstOrDefaultAsync(t => t.Id == tempHouseId);
        }
        public async Task<TempHouse> PetMeetingsForTempHouseDone(Guid tempHouseId)
        {
            var foundTempHouse = await GetTempHouse(tempHouseId);

            if (foundTempHouse.IsPreTempHousePoll == true && foundTempHouse.TempHousePoll != null)
            {
                if (foundTempHouse.Activity.Activities.Count >= 1)
                {
                    foreach (var activityEnd in foundTempHouse.Activity.Activities)
                    {
                        if (activityEnd.EndActivityDate < DateTimeOffset.Now.ToUniversalTime())
                        {
                            foundTempHouse.IsMeetings = true;
                            foundTempHouse.StartOfTemporaryHouseDate = DateTimeOffset.Now.ToUniversalTime();
                            await _dbContext.SaveChangesAsync();
                            return foundTempHouse;
                        }
                    }

                }
            }

            return null;
        }
        public async Task<bool> UpdateTempHouse(TempHouse tempHouse)
        {
            var foundTempHouse = await GetTempHouse(tempHouse.Id);
            if (foundTempHouse != null)
            {
                foundTempHouse.UserId = tempHouse.UserId;
                foundTempHouse.TemporaryOwner = tempHouse.TemporaryOwner;
                foundTempHouse.AddressId = tempHouse.AddressId;
                foundTempHouse.TemporaryHouseAddress = tempHouse.TemporaryHouseAddress;
                foundTempHouse.PetsInTemporaryHouse = tempHouse.PetsInTemporaryHouse;
                foundTempHouse.IsPreTempHousePoll = tempHouse.IsPreTempHousePoll;
                foundTempHouse.TempHousePoll = tempHouse.TempHousePoll;
                foundTempHouse.CalendarId = tempHouse.CalendarId;
                foundTempHouse.Activity = tempHouse.Activity;
                foundTempHouse.IsMeetings = tempHouse.IsMeetings;
                foundTempHouse.StartOfTemporaryHouseDate = tempHouse.StartOfTemporaryHouseDate.ToUniversalTime();
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteTempHouse(Guid tempHouseId, Guid shelterId, Guid petId, Guid userId)
        {
            var foundShelter = await FindShelter(shelterId);
            var foundTempHouse = await GetTempHouse(tempHouseId);
            var foundUser = await FindUserById(userId);
            var foundPet = await GetPetById(petId);
            if (foundShelter != null && foundTempHouse != null && foundUser != null && foundPet != null)
            {
                foundTempHouse.PetsInTemporaryHouse.Remove(foundPet);
                foundPet.Status = PetStatus.AtShelter;
                var howManyPetsInTempHouse = foundTempHouse.PetsInTemporaryHouse.Count();
                if (howManyPetsInTempHouse <= 1)
                {
                    foundShelter.TempHouses.Remove(foundTempHouse);
                }
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
        public async Task<IEnumerable<Activity>> GetAllPetActivities(Guid shelterId, Guid petId)
        {
            var pet = await GetPetById(petId);
            var foundPet = await GetShelterPetById(shelterId, pet.Id);

            if (foundPet != null && foundPet.Calendar != null && foundPet.Calendar.Activities != null)
            {
                return foundPet.Calendar.Activities.ToList();
            }

            return Enumerable.Empty<Activity>();
        }

        public async Task<Activity> GetPetActivityById(Guid shelterId, Guid activityId, Guid petId)
        {
            var pet = await GetPetById(petId);
            var foundPet = await GetShelterPetById(shelterId, pet.Id);
            if (foundPet != null && foundPet.Calendar != null)
            {
                var activity = foundPet.Calendar.Activities.FirstOrDefault(e => e.Id == activityId);
                return activity;
            }

            return null;
        }

        public async Task<Activity> AddPetActivityToCalendar(Guid shelterId, Guid petId, Activity activity)
        {
            var pet = await GetPetById(petId);
            var foundPet = await GetShelterPetById(shelterId, pet.Id);
            if (foundPet != null && foundPet.Calendar != null && foundPet.Calendar.Activities != null)
            {
                var foundActivity = foundPet.Calendar.Activities.FirstOrDefault(a => a.Name == activity.Name && a.StartActivityDate == activity.StartActivityDate && a.EndActivityDate == activity.EndActivityDate);
                if (!foundPet.Calendar.Activities.Contains(foundActivity))
                {
                    foundPet.Calendar.Activities.Add(activity);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Activity is already exist");
                }
            }
            return activity;
        }

        public async Task<bool> UpdatePetActivity(Guid shelterId, Guid petId, Activity activity)
        {
            var foundPet = await GetShelterPetById(shelterId, petId);
            var foundActivity = foundPet.Calendar.Activities.FirstOrDefault(e => e.Id == activity.Id);

            if (foundPet != null && foundActivity != null)
            {
                foundActivity.Name = activity.Name;
                foundActivity.StartActivityDate = activity.StartActivityDate.ToUniversalTime();
                foundActivity.EndActivityDate = activity.EndActivityDate.ToUniversalTime();
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeletePetActivity(Guid shelterId, Guid petId, Guid activityId)
        {
            var foundPet = await GetShelterPetById(shelterId, petId);
            var foundActivity = foundPet.Calendar.Activities.FirstOrDefault(e => e.Id == activityId);

            if (foundPet != null && foundActivity != null)
            {
                foundPet.Calendar.Activities.Remove(foundActivity);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
        //public async Task<bool> DeleteAdoption(Guid shelterId, Guid adoptionId, Guid userId)
        //{
        //    var foundShelter = await FindShelter(shelterId);
        //    var foundAdoption = foundShelter.Adoptions.FirstOrDefault(x => x.Id == adoptionId);
        //    var foundUser = await FindUserById(userId);
        //    if (foundAdoption != null && foundShelter != null)
        //    {
        //        foundShelter.Adoptions.Remove(foundAdoption);
        //        foundUser.Adoptions.Remove(foundAdoption);
        //        await _dbContext.SaveChangesAsync();
        //        return true;
        //    }
        //    return false;

        //}

        public async Task<bool> UpdateAdoption(Guid shelterId, Guid userId, Adoption adoption)
        {
            var foundShelter = await FindShelter(shelterId);
            var foundUser = await FindUserById(userId);
            if (foundShelter != null && foundUser != null)
            {
                var foundShelterAdoption = foundShelter.Adoptions.FirstOrDefault(x => x.Id == adoption.Id);
                var foundUserAdoption = foundUser.Adoptions.FirstOrDefault(a => a.Id == adoption.Id);

                if (foundShelterAdoption != null && foundUserAdoption != null)
                {
                    foundShelterAdoption.IsPreAdoptionPoll = adoption.IsPreAdoptionPoll;
                    foundShelterAdoption.PreadoptionPoll = adoption.PreadoptionPoll;
                    foundShelterAdoption.IsMeetings = adoption.IsMeetings;
                    foundShelterAdoption.Activity = adoption.Activity;
                    foundShelterAdoption.IsContractAdoption = adoption.IsContractAdoption;
                    foundShelterAdoption.ContractAdoption = adoption.ContractAdoption;
                    foundShelterAdoption.DateOfAdoption = adoption.DateOfAdoption;

                    foundUserAdoption.IsPreAdoptionPoll = adoption.IsPreAdoptionPoll;
                    foundUserAdoption.PreadoptionPoll = adoption.PreadoptionPoll;
                    foundUserAdoption.IsMeetings = adoption.IsMeetings;
                    foundUserAdoption.Activity = adoption.Activity;
                    foundUserAdoption.IsContractAdoption = adoption.IsContractAdoption;
                    foundUserAdoption.ContractAdoption = adoption.ContractAdoption;
                    foundUserAdoption.DateOfAdoption = adoption.DateOfAdoption;

                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                return false;

            }
            return false;
        }

        public async Task<IEnumerable<Pet>> GetAllAvaiblePets(Guid shelterId)
        {
            var foundShelter = await FindShelter(shelterId);
            if (foundShelter != null)
            {
                var avaiblePets = foundShelter.ShelterPets.Where(x => x.AvaibleForAdoption == true).ToList();

                return avaiblePets;
            }
            throw new Exception("Shelter not found");

        }

        public async Task<Adoption> GetAdoptionFromDataBaseById(Guid adoptionId)
        {
            return _dbContext.Adoptions.Include(a => a.Activity).ThenInclude(a => a.Activities).FirstOrDefault(a => a.Id == adoptionId);
        }
    }
}
