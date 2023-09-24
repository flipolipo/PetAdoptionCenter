using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.Animal.Enums;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.TemporaryHouse;
using SimpleWebDal.Models.WebUser;

namespace SImpleWebLogic.Repository.ShelterRepo
{
    public interface IShelterRepository
    {
        //GET
        public Task<IEnumerable<Shelter>> GetAllShelters();
        public Task<Shelter> GetShelterById(Guid shelterId); 
        public Task<IEnumerable<Pet>> GetAllShelterDogsOrCats(Guid shelterId, PetType type);
        public Task<IEnumerable<Pet>> GetAllShelterPets(Guid shelterId);
        public Task<Pet> GetShelterPetById(Guid shelterId, Guid petId);
        public Task<IEnumerable<User>> GetShelterWorkers(Guid shelterId);
        public Task<IEnumerable<User>> GetShelterContributors(Guid shelterId);
        public Task<User> GetShelterWorkerById(Guid shelterId, Guid workerId);
        public Task<User> GetShelterContributorById(Guid shelterId, Guid workerId);
        public Task<IEnumerable<Pet>> GetAllAdoptedPets(Guid shelterId);
        public Task<TempHouse> GetTempHouseById(Guid shelterId, Guid tempHouseId);
        public Task<IEnumerable<TempHouse>> GetAllTempHouses(Guid shelterId);
        public Task<IEnumerable<Pet>> GetAllShelterTempHousesPets(Guid shelterId);
        public Task<Pet> GetTempHousePetById(Guid shelterId, Guid petId, Guid tempHouseId);
        public Task<IEnumerable<Activity>> GetShelterActivities(Guid shelterId);
        public Task<Activity> GetShelterActivityById(Guid shelterId, Guid activityId);
        public Task<IEnumerable<Disease>> GetAllPetDiseases(Guid shelterId, Guid petId);
        public Task<Disease> GetPetDiseaseById(Guid shelterId, Guid petId, Guid diseaseId);
        public Task<IEnumerable<Vaccination>> GetAllPetVaccinations(Guid shelterId, Guid petId);
        public Task<Disease> GetPetVaccinationById(Guid shelterId, Guid petId, Guid vaccinationId);

        //POST
        public Task<Shelter> CreateShelter(string name, string description, string street, string houseNumber, string postalCode, string city);
        public Task<Pet> AddPet(Guid shelterId, PetType type, string description, PetStatus status, bool avaibleForAdoption);
        public Task<Activity> AddActivityToCalendar(Guid shelterId, string activityName, DateTime activityDate);
        public Task<TempHouse> AddTempHouse(Guid shelterId, Guid userId, DateTime startDate);

        //PUT
        public Task<bool> AddWorker(Guid shelterId, Guid userId);
        public Task<bool> AddContributor(Guid shelterId, Guid userId);
        public Task<bool> UpdateShelter(Guid shelterId, string name, string description, string street, string houseNumber, string postalCode, string city);
        public Task<bool> UpdateShelterPet(Guid shelterId, Guid petId, PetType type, string description, PetStatus status, bool avaibleForAdoption);
        public Task<bool> UpdateActivity(Guid shelterId, Guid activityId, string name, DateTime date);
        public Task<bool> UpdatePetBasicHealthInfo(Guid shelterId, Guid petId, string name, int age, Size size);
        //DELETE
        public Task<bool> DeleteShelterPet(Guid shelterId, Guid petId);
        public Task<bool> DeleteActivity(Guid callendarId, Guid activityId);
        public Task<bool> DeleteWorker(Guid shelterId, Guid userId);
        public Task<bool> DeleteContributor(Guid shelterId, Guid userId);
        public Task<bool> DeleteTempHouse(Guid tempHouseId, Guid shelterId);
        public Task<bool> DeleteShelter(Guid shelterId);
    }
}
