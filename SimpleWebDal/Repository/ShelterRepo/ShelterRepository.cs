using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SimpleWebDal.Data;
using SimpleWebDal.Exceptions;
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
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
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
            if (userId.Equals(Guid.Empty))
            {
                throw new UserValidationException("User ID cannot be empty.");
            }
            var foundUser = await _dbContext.Users
            .Include(b => b.BasicInformation).ThenInclude(c => c.Address)
            .Include(d => d.Roles)
            .Include(e => e.UserCalendar).ThenInclude(f => f.Activities)
            .Include(g => g.Adoptions)
            .Include(h => h.Pets).FirstOrDefaultAsync(u => u.Id == userId);
            return foundUser;
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

        public async Task<Shelter> GetShelterById(Guid shelterId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId);
            return foundShelter;
        }
        private async Task<Address> GetExistingAddressFromDataBase(Shelter shelter)
        {
            if (shelter == null)
            {
                throw new ShelterValidationException("Shelter not found.");
            }
            var existingAddress = await _dbContext.Addresses.FirstOrDefaultAsync(a => a.Street == shelter.ShelterAddress.Street &&
                       a.HouseNumber == shelter.ShelterAddress.HouseNumber && a.FlatNumber == shelter.ShelterAddress.FlatNumber &&
                       a.PostalCode == shelter.ShelterAddress.PostalCode && a.City == shelter.ShelterAddress.City);
            return existingAddress;
        }

        public async Task<Shelter> CreateShelter(Shelter shelter)
        {
            shelter.TempHouses = new List<TempHouse>();
            shelter.ShelterCalendar = new CalendarActivity();
            var existingAddress = await GetExistingAddressFromDataBase(shelter);
            if (existingAddress == null)
            {
                _dbContext.Addresses.Add(shelter.ShelterAddress);
            }
            else
            {
                shelter.ShelterAddress = existingAddress;
            }
            _dbContext.Shelters.Add(shelter);
            _dbContext.SaveChanges();
            return shelter;
        }

        public async Task<bool> UpdateShelter(Guid shelterId, string name, string description, string street, string houseNumber, string postalCode, string city, string phone, string bankNumber, IFormFile image)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter object cannot be null");

            if (image != null)
            {
                using var memoryStream = new MemoryStream();
                await image.CopyToAsync(memoryStream);
                foundShelter.Image = memoryStream.ToArray();
            }
            foundShelter.ShelterDescription = description;
            foundShelter.Name = name;
            foundShelter.PhoneNumber = phone;
            foundShelter.ShelterAddress.Street = street;
            foundShelter.ShelterAddress.PostalCode = postalCode;
            foundShelter.ShelterAddress.HouseNumber = houseNumber;
            foundShelter.ShelterAddress.City = city;
            foundShelter.BankNumber = bankNumber;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteShelter(Guid shelterId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            _dbContext.Shelters.Remove(foundShelter);
            _dbContext.SaveChanges();
            return true;
        }
        public async Task<IEnumerable<Activity>> GetShelterActivities(Guid shelterId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            if (foundShelter.ShelterCalendar != null && foundShelter.ShelterCalendar.Activities != null)
            {
                return foundShelter.ShelterCalendar.Activities.ToList();
            }

            return Enumerable.Empty<Activity>();
        }

        public async Task<Activity> GetShelterActivityById(Guid shelterId, Guid activityId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (activityId.Equals(Guid.Empty))
            {
                throw new ActivityValidationException("Activity ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            if (foundShelter.ShelterCalendar != null)
            {
                var activity = foundShelter.ShelterCalendar.Activities.FirstOrDefault(e => e.Id == activityId);
                return activity;
            }
            return null;
        }
        public async Task<Activity> AddActivityToCalendar(Guid shelterId, Activity activity)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var foundCalendar = foundShelter.ShelterCalendar ?? throw new CalendarValidationException("Calendar not found");
            var foundActivity = foundShelter.ShelterCalendar.Activities.FirstOrDefault(a => a.Name == activity.Name && a.StartActivityDate == activity.StartActivityDate && a.EndActivityDate == activity.EndActivityDate);

            if (!foundCalendar.Activities.Contains(foundActivity))
            {
                foundCalendar.Activities.Add(activity);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new ActivityValidationException("Activity is already exist");
            }
            return activity;
        }
        public async Task<bool> UpdateShelterActivity(Guid shelterId, Activity activity)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var foundActivity = foundShelter.ShelterCalendar.Activities.FirstOrDefault(e => e.Id == activity.Id) ?? throw new ActivityValidationException("Activity not found");
            foundActivity.Name = activity.Name;
            foundActivity.StartActivityDate = activity.StartActivityDate;
            foundActivity.EndActivityDate = activity.EndActivityDate;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteActivity(Guid shelterId, Guid activityId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (activityId.Equals(Guid.Empty))
            {
                throw new ActivityValidationException("Activity ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var foundActivity = foundShelter.ShelterCalendar.Activities.FirstOrDefault(e => e.Id == activityId) ?? throw new ActivityValidationException("Activity not found");
            foundShelter.ShelterCalendar.Activities.Remove(foundActivity);
            _dbContext.Activities.Remove(foundActivity);
            _dbContext.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<User>> GetShelterUsers(Guid shelterId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            return foundShelter.ShelterUsers;
        }
        public async Task<User> GetShelterUserById(Guid shelterId, Guid userId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (userId.Equals(Guid.Empty))
            {
                throw new UserValidationException("User ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var shelterUsers = foundShelter.ShelterUsers;
            if (shelterUsers != null)
            {
                return shelterUsers.FirstOrDefault(e => e.Id == userId);
            }
            return null;
        }
        public async Task<bool> AddShelterUser(Guid shelterId, Guid userId, Role role)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (userId.Equals(Guid.Empty))
            {
                throw new UserValidationException("User ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var foundUser = await FindUserById(userId) ?? throw new UserValidationException("User not found");
            var foundRole = foundUser.Roles ?? throw new RoleValidationException("Role not found");
            foundRole.Add(role);
            foundShelter.ShelterUsers.Add(foundUser);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteShelterUser(Guid shelterId, Guid userId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (userId.Equals(Guid.Empty))
            {
                throw new UserValidationException("User ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var user = foundShelter.ShelterUsers.FirstOrDefault(e => e.Id == userId) ?? throw new UserValidationException("User not found");
            foundShelter.ShelterUsers.Remove(user);
            _dbContext.SaveChanges();
            return true;
        }
        public async Task<Pet> GetPetById(Guid petId)
        {
            if (petId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Pet ID cannot be empty.");
            }
            return await _dbContext.Pets.Include(p => p.BasicHealthInfo).ThenInclude(p => p.Vaccinations)
                .Include(p => p.BasicHealthInfo).ThenInclude(p => p.MedicalHistory)
                .Include(p => p.Calendar).ThenInclude(p => p.Activities)
                .Include(p => p.Users)
                .FirstOrDefaultAsync(p => p.Id == petId);
        }
        public async Task<IEnumerable<Pet>> GetAllShelterPets(Guid shelterId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            return foundShelter.ShelterPets.ToList();
        }
        public async Task<Pet> GetShelterPetById(Guid shelterId, Guid petId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            if (foundShelter.ShelterPets != null)
            {
                return foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId);
            }
            return null;
        }
        public async Task<Pet> AddPet(Guid shelterId, Pet pet)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            pet.Calendar = new CalendarActivity();
            pet.Users = new List<User>();
            var foundPet = foundShelter.ShelterPets ?? throw new PetValidationException("Pet list not found");
            foundPet.Add(pet);
            await _dbContext.SaveChangesAsync();
            return pet;
        }

        public async Task<bool> UpdateShelterPet(Guid shelterId, Guid petId, PetGender gender, PetType type, string description, PetStatus status, bool avaibleForAdoption, IFormFile image)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var foundPet = foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId) ?? throw new PetValidationException("Pet not found");
            if (image != null)
            {
                using var memoryStream = new MemoryStream();
                await image.CopyToAsync(memoryStream);
                foundPet.Image = memoryStream.ToArray();
            }
            foundPet.AvaibleForAdoption = avaibleForAdoption;
            foundPet.Gender = gender;
            foundPet.Description = description;
            foundPet.Status = status;
            foundPet.Type = type;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteShelterPet(Guid shelterId, Guid petId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var pet = foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId) ?? throw new PetValidationException("Pet not found");
            foundShelter.ShelterPets.Remove(pet);
            _dbContext.Pets.Remove(pet);
            _dbContext.SaveChanges();
            return true;
        }
        public async Task<BasicHealthInfo> GetPetBasicHealthInfo(Guid shelterId, Guid petId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var pet = await GetPetById(petId) ?? throw new PetValidationException("Pet not found");
            var foundPet = await GetShelterPetById(shelterId, pet.Id) ?? throw new PetValidationException("Pet not found in the shelter");
            if (foundPet.BasicHealthInfo != null)
            {
                return foundPet.BasicHealthInfo;
            }
            return null;
        }
        public async Task<BasicHealthInfo> GetPetBasicHealthInfoById(Guid shelterId, Guid petId, Guid basicHealtInfoId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            if (basicHealtInfoId.Equals(Guid.Empty))
            {
                throw new BasicHealthInfoValidationException("Basic Healt Info ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var pet = await GetPetById(petId) ?? throw new PetValidationException("Pet not found");
            var foundPet = await GetShelterPetById(shelterId, pet.Id) ?? throw new PetValidationException("Pet not found in the shelter");
            var basicHealthInfo = foundPet.BasicHealthInfo ?? throw new BasicHealthInfoValidationException("Basic health info for the pet not found");
            if (basicHealthInfo.Id == basicHealtInfoId)
            {
                return basicHealthInfo;
            }
            return null;
        }
      
        public async Task<bool> UpdatePetBasicHealthInfo(Guid shelterId, Guid petId, string name, int age, Size size, bool isNeutred)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var foundPet = foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId) ?? throw new PetValidationException("Pet not found");
            var petHealthInfo = foundPet.BasicHealthInfo;
            petHealthInfo.Size = size;
            petHealthInfo.Age = age;
            petHealthInfo.Name = name;
            petHealthInfo.IsNeutered = isNeutred;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Vaccination>> GetAllPetVaccinations(Guid shelterId, Guid petId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var pet = await GetPetById(petId) ?? throw new PetValidationException("Pet not found");
            var foundPet = await GetShelterPetById(shelterId, pet.Id) ?? throw new PetValidationException("Pet not found in the shelter");
            if (foundPet.BasicHealthInfo != null)
            {
                return foundPet.BasicHealthInfo.Vaccinations.ToList();
            }
            return Enumerable.Empty<Vaccination>();
        }

        public async Task<Vaccination> GetPetVaccinationById(Guid shelterId, Guid petId, Guid vaccinationId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            if (vaccinationId.Equals(Guid.Empty))
            {
                throw new VaccinationValidationException("Vaccination ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var foundPet = foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId) ?? throw new PetValidationException("Pet not found");
            if (foundPet.BasicHealthInfo.Vaccinations != null)
            {
                return foundPet.BasicHealthInfo.Vaccinations.FirstOrDefault(e => e.Id == vaccinationId);
            }
            return null;
        }

        public async Task<Vaccination> AddPetVaccination(Guid shelterId, Guid petId, Vaccination vaccination)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var foundPet = foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId) ?? throw new PetValidationException("Pet not found");
            var foundVaccinations = foundPet.BasicHealthInfo.Vaccinations ?? throw new VaccinationValidationException("Vaccination list not found");
            foundVaccinations.Add(vaccination);
            _dbContext.SaveChanges();
            return vaccination;
        }
        public async Task<bool> UpdatePetVaccination(Guid shelterId, Guid petId, Vaccination vaccination)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var pet = await GetPetById(petId) ?? throw new PetValidationException("Pet not found");
            var foundPetInShelter = await GetShelterPetById(shelterId, pet.Id) ?? throw new PetValidationException("Pet not found in the shelter");
            var foundVaccination = foundPetInShelter.BasicHealthInfo.Vaccinations.FirstOrDefault(e => e.Id == vaccination.Id) ?? throw new VaccinationValidationException("Vaccination not found");
            foundVaccination.VaccinationName = vaccination.VaccinationName;
            foundVaccination.Date = vaccination.Date;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeletePetVaccination(Guid shelterId, Guid petId, Guid vaccinationId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            if (vaccinationId.Equals(Guid.Empty))
            {
                throw new VaccinationValidationException("Vaccination ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var pet = await GetPetById(petId) ?? throw new PetValidationException("Pet not found");
            var foundVaccination = await GetPetVaccinationById(shelterId, pet.Id, vaccinationId) ?? throw new VaccinationValidationException("Vaccination not found");
            pet.BasicHealthInfo.Vaccinations.Remove(foundVaccination);
            _dbContext.Vaccinations.Remove(foundVaccination);
            _dbContext.SaveChanges();
            return true;
        }


        public async Task<IEnumerable<Disease>> GetAllPetDiseases(Guid shelterId, Guid petId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var pet = await GetPetById(petId) ?? throw new PetValidationException("Pet not found");
            var foundPet = await GetShelterPetById(shelterId, pet.Id) ?? throw new PetValidationException("Pet not found in the shelter");
            if (foundPet.BasicHealthInfo != null)
            {
                return foundPet.BasicHealthInfo.MedicalHistory.ToList();
            }
            return Enumerable.Empty<Disease>();
        }

        public async Task<Disease> GetPetDiseaseById(Guid shelterId, Guid petId, Guid diseaseId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            if (diseaseId.Equals(Guid.Empty))
            {
                throw new DiseaseValidationException("Disease ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var foundPet = foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId) ?? throw new PetValidationException("Pet not found");
            if (foundPet.BasicHealthInfo.MedicalHistory != null)
            {
                return foundPet.BasicHealthInfo.MedicalHistory.FirstOrDefault(e => e.Id == diseaseId);
            }
            return null;
        }

        public async Task<Disease> AddPetDisease(Guid shelterId, Guid petId, Disease disease)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var foundPet = foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId) ?? throw new PetValidationException("Pet not found");
            var diseaseList = foundPet.BasicHealthInfo.MedicalHistory ?? throw new DiseaseValidationException("Diseases list not found");
            diseaseList.Add(disease);
            _dbContext.SaveChanges();
            return disease;
        }
        public async Task<bool> UpdatePetDisease(Guid shelterId, Guid petId, Disease disease)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var pet = await GetPetById(petId) ?? throw new PetValidationException("Pet not found");
            var foundPetInShelter = await GetShelterPetById(shelterId, pet.Id) ?? throw new PetValidationException("Pet not found in the shelter");
            var foundDisease = foundPetInShelter.BasicHealthInfo.MedicalHistory.FirstOrDefault(e => e.Id == disease.Id) ?? throw new DiseaseValidationException("Disease not found");
            foundDisease.NameOfdisease = disease.NameOfdisease;
            foundDisease.IllnessStart = disease.IllnessStart;
            foundDisease.IllnessEnd = disease.IllnessEnd;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeletePetDisease(Guid shelterId, Guid petId, Guid diseaseId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            if (diseaseId.Equals(Guid.Empty))
            {
                throw new DiseaseValidationException("Disease ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var pet = await GetPetById(petId) ?? throw new PetValidationException("Pet not found");
            var foundDisease = await GetPetDiseaseById(shelterId, pet.Id, diseaseId) ?? throw new DiseaseValidationException("Disease not found");
            pet.BasicHealthInfo.MedicalHistory.Remove(foundDisease);
            _dbContext.Diseases.Remove(foundDisease);
            _dbContext.SaveChanges();
            return true;
        }
        public async Task<IEnumerable<Activity>> GetAllPetActivities(Guid shelterId, Guid petId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            var pet = await GetPetById(petId) ?? throw new PetValidationException("Pet not found");
            var foundPet = await GetShelterPetById(shelterId, pet.Id) ?? throw new PetValidationException("Pet not found in the shelter");
            var foundCalendar = foundPet.Calendar ?? throw new CalendarValidationException("Calendar not found");
            if (foundCalendar.Activities != null)
            {
                return foundPet.Calendar.Activities.ToList();
            }

            return Enumerable.Empty<Activity>();
        }

        public async Task<Activity> GetPetActivityById(Guid shelterId, Guid activityId, Guid petId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            var pet = await GetPetById(petId) ?? throw new PetValidationException("Pet not found");
            var foundPet = await GetShelterPetById(shelterId, pet.Id) ?? throw new PetValidationException("Pet not found in the shelter");
            if (foundPet.Calendar != null)
            {
                return foundPet.Calendar.Activities.FirstOrDefault(e => e.Id == activityId);
            }
            return null;
        }

        public async Task<Activity> AddPetActivityToCalendar(Guid shelterId, Guid petId, Activity activity)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            var pet = await GetPetById(petId) ?? throw new PetValidationException("Pet not found");
            var foundPet = await GetShelterPetById(shelterId, pet.Id) ?? throw new PetValidationException("Pet not found in the shelter");
            var foundCalendar = foundPet.Calendar ?? throw new CalendarValidationException("Calendar not found");
            var foundActivityList = foundCalendar.Activities ?? throw new ActivityValidationException("Activity list not found");
            var foundActivity = foundPet.Calendar.Activities.FirstOrDefault(a => a.Name == activity.Name && a.StartActivityDate == activity.StartActivityDate && a.EndActivityDate == activity.EndActivityDate);
            if (!foundPet.Calendar.Activities.Contains(foundActivity))
            {
                foundPet.Calendar.Activities.Add(activity);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new ActivityValidationException("Activity is already exist");
            }
            return activity;
        }

        public async Task<bool> UpdatePetActivity(Guid shelterId, Guid petId, Activity activity)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            var pet = await GetPetById(petId) ?? throw new PetValidationException("Pet not found");
            var foundPet = await GetShelterPetById(shelterId, pet.Id) ?? throw new PetValidationException("Pet not found in the shelter");
            var foundCalendar = foundPet.Calendar ?? throw new CalendarValidationException("Calendar not found");
            var foundActivity = foundCalendar.Activities.FirstOrDefault(e => e.Id == activity.Id) ?? throw new ActivityValidationException("Activity not found");
            foundActivity.Name = activity.Name;
            foundActivity.StartActivityDate = activity.StartActivityDate.ToUniversalTime();
            foundActivity.EndActivityDate = activity.EndActivityDate.ToUniversalTime();
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePetActivity(Guid shelterId, Guid petId, Guid activityId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            if (activityId.Equals(Guid.Empty))
            {
                throw new ActivityValidationException("Activity ID cannot be empty.");
            }
            var pet = await GetPetById(petId) ?? throw new PetValidationException("Pet not found");
            var foundPet = await GetShelterPetById(shelterId, pet.Id) ?? throw new PetValidationException("Pet not found in the shelter");
            var foundActivity = foundPet.Calendar.Activities.FirstOrDefault(e => e.Id == activityId) ?? throw new ActivityValidationException("Activity not found");
            foundPet.Calendar.Activities.Remove(foundActivity);
            _dbContext.Activities.Remove(foundActivity);
            await _dbContext.SaveChangesAsync();
            return true;
        }


        public async Task<IEnumerable<TempHouse>> GetAllTempHouses(Guid shelterId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            return foundShelter.TempHouses;
        }

        public async Task<Pet> GetTempHousePetById(Guid shelterId, Guid tempHouseId, Guid petId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            if (tempHouseId.Equals(Guid.Empty))
            {
                throw new TempHouseValidationException("TempHouse ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var foundTempHouse = foundShelter.TempHouses.FirstOrDefault(e => e.Id == tempHouseId) ?? throw new TempHouseValidationException("Temporary house not found");
            var pet = foundTempHouse.PetsInTemporaryHouse.FirstOrDefault(e => e.Id == petId) ?? throw new PetValidationException("Pet not found in the temporary house");
            return pet;

        }
        public async Task<TempHouse> GetTempHouseById(Guid shelterId, Guid tempHouseId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (tempHouseId.Equals(Guid.Empty))
            {
                throw new TempHouseValidationException("Temporary house ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var foundTempHouse = foundShelter.TempHouses;
            if (foundTempHouse != null)
            {
                return foundShelter.TempHouses.FirstOrDefault(e => e.Id == tempHouseId);
            }
            return null;
        }
        private async Task<bool> CheckIfUserHaveTempHouse(Guid userId)
        {
            if (userId.Equals(Guid.Empty))
            {
                throw new UserValidationException("User ID cannot be empty.");
            }
            var foundTempHouseByUserId = await _dbContext.TempHouses.FirstOrDefaultAsync(u => u.UserId == userId);
            if (foundTempHouseByUserId == null)
            {
                return false;
            }
            return true;
        }
        public async Task<TempHouse> InitializeTempHouseForPet(Guid shelterId, Guid userId, Guid petId, TempHouse tempHouse)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (userId.Equals(Guid.Empty))
            {
                throw new UserValidationException("User ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var pet = await GetPetById(petId) ?? throw new PetValidationException("Pet not found");
            var foundPet = await GetShelterPetById(shelterId, pet.Id) ?? throw new PetValidationException("Pet in the shelter not found"); ;
            var foundUser = await FindUserById(userId) ?? throw new UserValidationException("User not found"); ;
            var checkIfUserHaveTempHouse = await CheckIfUserHaveTempHouse(userId);
            if (!checkIfUserHaveTempHouse)
            {
                if (foundPet.Status != PetStatus.OnAdoptionProccess && foundPet.Status != PetStatus.Adopted && foundPet.Status != PetStatus.TemporaryHouse)
                {
                    tempHouse.TemporaryOwner = foundUser;
                    tempHouse.TemporaryHouseAddress = foundUser.BasicInformation.Address;
                    tempHouse.PetsInTemporaryHouse = new List<Pet> { foundPet };
                    tempHouse.IsPreTempHousePoll = true;
                    tempHouse.Activity = new CalendarActivity();
                    tempHouse.StartOfTemporaryHouseDate = new DateTimeOffset().ToUniversalTime();
                    foundPet.Status = PetStatus.OnTemporaryHouseProcess;
                    foundUser.Pets.Add(foundPet);
                    foundShelter.TempHouses.Add(tempHouse);
                    await _dbContext.SaveChangesAsync();
                    return tempHouse;
                }
            }
            return null;
        }
        private async Task<Pet> GetPetInTempHouse(Guid tempHouseId, Guid petId)
        {
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            var foundTempHouse = await GetTempHouse(tempHouseId) ?? throw new TempHouseValidationException("Temporary house not found");
            return foundTempHouse.PetsInTemporaryHouse.FirstOrDefault(p => p.Id == petId);
        }

        private async Task<Activity> GetPetActivity(Guid petId, Guid activityId)
        {
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            if (activityId.Equals(Guid.Empty))
            {
                throw new ActivityValidationException("Activity ID cannot be empty.");
            }
            var foundPet = await GetPetById(petId) ?? throw new PetValidationException("Pet not found");
            return foundPet.Calendar.Activities.FirstOrDefault(a => a.Id == activityId);
        }
        public async Task<TempHouse> ChooseMeetingDatesForTempHouseProcess(Guid petId, Guid tempHouseId, Guid activityId)
        {
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            if (tempHouseId.Equals(Guid.Empty))
            {
                throw new TempHouseValidationException("TempHouse ID cannot be empty.");
            }
            if (activityId.Equals(Guid.Empty))
            {
                throw new ActivityValidationException("Activity ID cannot be empty.");
            }
            var foundTempHouse = await GetTempHouse(tempHouseId) ?? throw new TempHouseValidationException("Temporary house not found");
            var pet = await GetPetById(petId) ?? throw new PetValidationException("Pet not found");
            var foundPetInTempHouse = await GetPetInTempHouse(foundTempHouse.Id, pet.Id) ?? throw new PetValidationException("Pet not found in the temporary house");
            var foundActivity = await GetPetActivity(foundPetInTempHouse.Id, activityId) ?? throw new ActivityValidationException("activity not found");
            if (foundTempHouse.IsPreTempHousePoll == true && foundTempHouse.TempHousePoll != null)
            {
                foundTempHouse.Activity.Activities.Add(foundActivity);
                foundPetInTempHouse.Calendar.Activities.Remove(foundActivity);
                await _dbContext.SaveChangesAsync();
                return foundTempHouse;
            }
            return null;
        }
        public async Task<TempHouse> GetTempHouse(Guid tempHouseId)
        {
            if (tempHouseId.Equals(Guid.Empty))
            {
                throw new TempHouseValidationException("TempHouse ID cannot be empty.");
            }
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
        public async Task<TempHouse> ChooseMeetingDatesForKnowAnotherPet(Guid shelterId, Guid petId, Guid userId, Guid tempHouseId, Guid activityId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            if (userId.Equals(Guid.Empty))
            {
                throw new UserValidationException("User ID cannot be empty.");
            }
            if (tempHouseId.Equals(Guid.Empty))
            {
                throw new TempHouseValidationException("TempHouse ID cannot be empty.");
            }
            if (activityId.Equals(Guid.Empty))
            {
                throw new ActivityValidationException("Activity ID cannot be empty.");
            }
            var foundTempHouse = await GetTempHouse(tempHouseId) ?? throw new TempHouseValidationException("Temporary house not found");
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var foundUser = await FindUserById(userId) ?? throw new UserValidationException("User not found.");
            var foundPet = await GetPetById(petId) ?? throw new PetValidationException("Pet not found");
            var foundPetInShelter = await GetShelterPetById(foundShelter.Id, foundPet.Id) ?? throw new PetValidationException("Pet not found in the shelter");
            var checkIfPetIsFromFoundShelter = foundTempHouse.PetsInTemporaryHouse.FirstOrDefault(s => s.ShelterId == shelterId);
            if (foundTempHouse.TemporaryOwner == foundUser && foundTempHouse.PetsInTemporaryHouse.Count >= 1 && checkIfPetIsFromFoundShelter != null)
            {
                foundTempHouse.Activity.Activities.Clear();
                foundTempHouse.IsMeetings = false;
                var foundActivity = await GetPetActivityById(foundShelter.Id, activityId, foundPetInShelter.Id);
                if (foundActivity != null)
                {
                    foundPet.Status = PetStatus.OnTemporaryHouseProcess;
                    foundTempHouse.PetsInTemporaryHouse.Add(foundPet);
                    foundUser.Pets.Add(foundPet);
                    foundTempHouse.Activity.Activities.Add(foundActivity);
                    foundPetInShelter.Calendar.Activities.Remove(foundActivity);
                    await _dbContext.SaveChangesAsync();
                    return foundTempHouse;
                }
            }
            return null;
        }

        public async Task<TempHouse> ConfirmYourChooseForTempHouse(Guid tempHouseId, Guid petId)
        {
            if (tempHouseId.Equals(Guid.Empty))
            {
                throw new TempHouseValidationException("TempHouse ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            var foundTempHouse = await GetTempHouse(tempHouseId) ?? throw new TempHouseValidationException("Temporary house not found");
            var foundPet = await GetPetById(petId) ?? throw new PetValidationException("Pet not found");
            if (foundTempHouse.IsPreTempHousePoll == true && foundTempHouse.TempHousePoll != null)
            {
                if (foundTempHouse.Activity.Activities.Count >= 1)
                {
                    foreach (var activityEnd in foundTempHouse.Activity.Activities)
                    {
                        if (activityEnd.EndActivityDate < DateTimeOffset.Now.ToUniversalTime())
                        {
                            foundTempHouse.IsMeetings = true;
                            foundPet.Status = PetStatus.TemporaryHouse;
                            foundTempHouse.StartOfTemporaryHouseDate = DateTimeOffset.Now.ToUniversalTime();
                            await _dbContext.SaveChangesAsync();
                            return foundTempHouse;
                        }
                    }

                }
            }
            return null;
        }

        public async Task<TempHouse> ConfirmToAddAnotherPetToTempHouse(Guid tempHouseId, Guid petId)
        {
            if (tempHouseId.Equals(Guid.Empty))
            {
                throw new TempHouseValidationException("TempHouse ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            var foundTempHouse = await GetTempHouse(tempHouseId) ?? throw new TempHouseValidationException("Temporary house not found");
            var foundPet = await GetPetById(petId) ?? throw new PetValidationException("Pet not found");
            if (foundTempHouse.Activity.Activities.Count >= 1)
            {
                foreach (var activityEnd in foundTempHouse.Activity.Activities)
                {
                    if (activityEnd.EndActivityDate < DateTimeOffset.Now.ToUniversalTime())
                    {
                        foundTempHouse.IsMeetings = true;
                        foundPet.Status = PetStatus.TemporaryHouse;
                        foundTempHouse.StartOfTemporaryHouseDate = DateTimeOffset.Now.ToUniversalTime();
                        await _dbContext.SaveChangesAsync();
                        return foundTempHouse;
                    }
                }

            }
            return null;
        }
        public async Task<bool> DeletePetFromTempHouse(Guid tempHouseId, Guid shelterId, Guid petId, Guid userId)
        {
            if (tempHouseId.Equals(Guid.Empty))
            {
                throw new TempHouseValidationException("TempHouse ID cannot be empty.");
            }
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            if (userId.Equals(Guid.Empty))
            {
                throw new UserValidationException("User ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var foundTempHouse = await GetTempHouse(tempHouseId) ?? throw new TempHouseValidationException("Temporary house not found");
            var foundUser = await FindUserById(userId) ?? throw new UserValidationException("User not found.");
            var foundPet = await GetPetById(petId) ?? throw new PetValidationException("Pet not found");
            foundTempHouse.PetsInTemporaryHouse.Remove(foundPet);
            foundPet.Status = PetStatus.AtShelter;
            foundTempHouse.IsMeetings = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateTempHouse(TempHouse tempHouse)
        {
            var foundTempHouse = await GetTempHouse(tempHouse.Id) ?? throw new TempHouseValidationException("Temporary house not found");
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

        public async Task<bool> DeleteTempHouse(Guid tempHouseId, Guid shelterId, Guid petId, Guid userId)
        {
            if (tempHouseId.Equals(Guid.Empty))
            {
                throw new TempHouseValidationException("TempHouse ID cannot be empty.");
            }
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            if (userId.Equals(Guid.Empty))
            {
                throw new UserValidationException("User ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var foundTempHouse = await GetTempHouse(tempHouseId) ?? throw new TempHouseValidationException("Temporary house not found");
            var foundUser = await FindUserById(userId) ?? throw new UserValidationException("User not found.");
            var foundPet = await GetPetById(petId) ?? throw new PetValidationException("Pet not found");
            foundTempHouse.PetsInTemporaryHouse.Remove(foundPet);
            foundPet.Status = PetStatus.AtShelter;
            var howManyPetsInTempHouse = foundTempHouse.PetsInTemporaryHouse.Count();
            if (howManyPetsInTempHouse <= 1)
            {
                foundShelter.TempHouses.Remove(foundTempHouse);
                _dbContext.TempHouses.Remove(foundTempHouse);
            }
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<Adoption> GetAdoptionFromDataBaseById(Guid adoptionId)
        {
            if (adoptionId.Equals(Guid.Empty))
            {
                throw new AdoptionValidationException("Adoption ID cannot be empty.");
            }
            return _dbContext.Adoptions.Include(a => a.Activity).ThenInclude(a => a.Activities).FirstOrDefault(a => a.Id == adoptionId);
        }
        public async Task<IEnumerable<Adoption>> GetAllShelterAdoptions(Guid shelterId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            return foundShelter.Adoptions.ToList();
        }

        public async Task<Adoption> GetShelterAdoptionById(Guid shelterId, Guid adoptionId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (adoptionId.Equals(Guid.Empty))
            {
                throw new AdoptionValidationException("Adoption ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var adoptionList = foundShelter.Adoptions ?? throw new AdoptionValidationException("Adoption list not found");
            if (adoptionList != null)
            {
                return adoptionList.FirstOrDefault(e => e.Id == adoptionId);
            }
            return null;
        }

        public async Task<Adoption> InitializePetAdoption(Guid shelterId, Guid petId, Guid userId, Adoption adoption)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            if (userId.Equals(Guid.Empty))
            {
                throw new UserValidationException("User ID cannot be empty.");
            }

            if (adoption != null)
            {
                var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
                var pet = await GetPetById(petId) ?? throw new PetValidationException("Pet not found");
                var foundPet = await GetShelterPetById(shelterId, pet.Id) ?? throw new PetValidationException("Pet not found in the shelter");
                var foundUser = await FindUserById(userId) ?? throw new UserValidationException("User not found.");
                if (adoption.PreadoptionPoll != null && foundPet.AvaibleForAdoption == true)
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
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            if (userId.Equals(Guid.Empty))
            {
                throw new UserValidationException("User ID cannot be empty.");
            }
            if (adoptionId.Equals(Guid.Empty))
            {
                throw new AdoptionValidationException("Adoption ID cannot be empty.");
            }
            if (activityId.Equals(Guid.Empty))
            {
                throw new ActivityValidationException("Activity ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var pet = await GetPetById(petId) ?? throw new PetValidationException("Pet not found");
            var foundPet = await GetShelterPetById(shelterId, pet.Id) ?? throw new PetValidationException("Pet not found in the shelter");
            var foundUser = await FindUserById(userId) ?? throw new UserValidationException("User not found.");
            var foundAdoption = await GetShelterAdoptionById(shelterId, adoptionId) ?? throw new AdoptionValidationException("Adoption not found");
            var foundActivity = await GetPetActivityById(foundShelter.Id, activityId, foundPet.Id) ?? throw new ActivityValidationException("Activity not found");
            if (foundAdoption.PetId == foundPet.Id && foundAdoption.UserId == userId && foundAdoption.IsPreAdoptionPoll == true && foundAdoption.PreadoptionPoll != null && foundActivity != null)
            {
                foundAdoption.Activity.Activities.Add(foundActivity);
                foundPet.Calendar.Activities.Remove(foundActivity);
                await _dbContext.SaveChangesAsync();
                return foundAdoption;
            }
            return null;
        }

        public async Task<Adoption> PetAdoptionMeetingsDone(Guid adoptionId)
        {
            if (adoptionId.Equals(Guid.Empty))
            {
                throw new AdoptionValidationException("Adoption ID cannot be empty.");
            }
            var foundAdoption = await GetAdoptionFromDataBaseById(adoptionId) ?? throw new AdoptionValidationException("Adoption not found");

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
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            if (userId.Equals(Guid.Empty))
            {
                throw new UserValidationException("User ID cannot be empty.");
            }
            if (adoptionId.Equals(Guid.Empty))
            {
                throw new AdoptionValidationException("Adoption ID cannot be empty.");
            }

            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var pet = await GetPetById(petId) ?? throw new PetValidationException("Pet not found");
            var foundPet = await GetShelterPetById(shelterId, pet.Id) ?? throw new PetValidationException("Pet not found in the shelter");
            var foundUser = await FindUserById(userId) ?? throw new UserValidationException("User not found.");
            var foundAdoption = await GetAdoptionFromDataBaseById(adoptionId) ?? throw new AdoptionValidationException("Adoption not found");
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

            return null;
        }


        public async Task<bool> UpdateAdoption(Guid shelterId, Guid userId, Adoption adoption)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (userId.Equals(Guid.Empty))
            {
                throw new UserValidationException("User ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var foundUser = await FindUserById(userId) ?? throw new UserValidationException("User not found.");
            var foundShelterAdoption = foundShelter.Adoptions.FirstOrDefault(x => x.Id == adoption.Id) ?? throw new AdoptionValidationException("Adoption not found");
            var foundUserAdoption = foundUser.Adoptions.FirstOrDefault(a => a.Id == adoption.Id) ?? throw new AdoptionValidationException("Adoption not found");
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

        public async Task<bool> DeleteAdoption(Guid shelterId, Guid adoptionId, Guid petId, Guid userId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            if (petId.Equals(Guid.Empty))
            {
                throw new PetValidationException("Pet ID cannot be empty.");
            }
            if (adoptionId.Equals(Guid.Empty))
            {
                throw new AdoptionValidationException("Adoption ID cannot be empty.");
            }
            if (userId.Equals(Guid.Empty))
            {
                throw new UserValidationException("User ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var pet = await GetPetById(petId) ?? throw new PetValidationException("Pet not found");
            var foundPet = await GetShelterPetById(shelterId, pet.Id) ?? throw new PetValidationException("Pet not found in the shelter");
            var foundUser = await FindUserById(userId) ?? throw new UserValidationException("User not found.");
            var foundAdoption = await GetAdoptionFromDataBaseById(adoptionId) ?? throw new AdoptionValidationException("Adoption not found");
            foundShelter.Adoptions.Remove(foundAdoption);
            foundUser.Adoptions.Remove(foundAdoption);
            foundPet.Status = PetStatus.AtShelter;
            foundPet.AvaibleForAdoption = true;
            _dbContext.Adoptions.Remove(foundAdoption);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Pet>> GetAllShelterDogsOrCats(Guid shelterId, PetType type)
        {
            if (shelterId == Guid.Empty)
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var pets = foundShelter.ShelterPets.Where(pet => pet.Type == type).ToList();
            return pets;
        }


        public async Task<IEnumerable<Pet>> GetAllAvaiblePets(Guid shelterId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var avaiblePets = foundShelter.ShelterPets.Where(x => x.AvaibleForAdoption == true).ToList();
            return avaiblePets;
        }
        public async Task<IEnumerable<Pet>> GetAllAdoptedPets(Guid shelterId)
        {
            if (shelterId == Guid.Empty)
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");
            var adopted = foundShelter.ShelterPets.Where(pet => pet.Status == PetStatus.Adopted).ToList();
            return adopted;
        }

        public async Task<IEnumerable<Pet>> GetAllShelterTempHousesPets(Guid shelterId)
        {
            if (shelterId.Equals(Guid.Empty))
            {
                throw new ShelterValidationException("Shelter ID cannot be empty.");
            }
            var foundShelter = await FindShelter(shelterId) ?? throw new ShelterValidationException("Shelter not found");

            return foundShelter.TempHouses
                .SelectMany(tempHouse => tempHouse.PetsInTemporaryHouse)
                .ToList();
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


        //public async Task<IEnumerable<User>> GetShelterUsersByRole(Guid shelterId, RoleName role)
        //{
        //    var foundShelter = await FindShelter(shelterId);
        //    var shelterUsers = foundShelter.ShelterUsers;

        //    var filteredUsers = FilterUsersByRole(shelterUsers, role);
        //    return filteredUsers;

        //}





    }
}
