using SimpleWebDal.Models.AdoptionProccess;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.Animal.Enums;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.TemporaryHouse;
using SimpleWebDal.Models.WebUser;
using SimpleWebDal.Models.WebUser.Enums;

namespace SImpleWebLogic.Repository.ShelterRepo
{
    public interface IShelterRepository
    {
        #region //GET
        public Task<IEnumerable<Shelter>> GetAllShelters();
        public Task<IEnumerable<User>> GetShelterUsersByRole(Guid shelterId, RoleName role);
        public Task<User> GetShelterUserById(Guid shelterId, Guid userId);
        public Task<IEnumerable<User>> GetShelterUsers(Guid shelterId);
        public Task<Shelter> GetShelterById(Guid shelterId); 
        public Task<IEnumerable<Pet>> GetAllShelterDogsOrCats(Guid shelterId, PetType type);
        public Task<IEnumerable<Pet>> GetAllShelterPets(Guid shelterId);
        public Task<Pet> GetShelterPetById(Guid shelterId, Guid petId);
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
        public Task<IEnumerable<Activity>> GetAllActivities(Guid shelterId);
        public Task<Activity> GetActivityById(Guid shelterId, Guid activityId);
        public Task<IEnumerable<Activity>> GetAllPetActivities(Guid shelterId, Guid petId);
        public Task<Activity> GetPetActivityById(Guid shelterId, Guid activityId, Guid petId);
        public Task<IEnumerable<Adoption>> GetAllShelterAdoptions(Guid shelterId);
        public Task<Adoption> GetShelterAdoptionById(Guid shelterId, Guid adoptionId);
        #endregion

        #region //POST
        // public Task<Shelter> CreateShelter(string name, string description, string street, string houseNumber, string postalCode, string city);
        public Task<Shelter> CreateShelter(Shelter shelter);
        public Task<TempHouse> AddTempHouse(Guid shelterId, Guid userId, Guid petId, TempHouse tempHouse);
        public Task<Pet> AddPet(Guid shelterId, Pet pet);
        public Task<Activity> AddActivityToCalendar(Guid shelterId, Activity activity);
        public Task<Activity> AddPetActivityToCalendar(Guid shelterId, Guid petId, Activity activity);
        public Task<Vaccination> AddPetVaccination(Guid shelterId, Guid petId, Vaccination vaccination);
        public Task<Disease> AddPetDisease(Guid shelterId, Guid petId, Disease disease);
        public Task<Adoption> AddAdoption(Guid shelterId, Guid petId, Guid userId, Adoption adoption);
        #endregion

        #region //PUT
        public Task<bool> UpdateShelter(Guid shelterId, string name, string description, string street, string houseNumber, string postalCode, string city);
        public Task<bool> UpdateShelterPet(Guid shelterId, Guid petId, PetGender gender, PetType type, string description, PetStatus status, bool avaibleForAdoption);
        public Task<bool> UpdateActivity(Guid shelterId, Guid activityId, string name, DateTime date);
        public Task<bool> AddShelterUser(Guid shelterId, Guid userId, RoleName roleName);
        public Task<bool> UpdatePetBasicHealthInfo(Guid shelterId, Guid petId, string name, int age, Size size, bool isNeutred);
        public Task<bool> UpdatePetActivity(Guid shelterId, Guid petId, Activity activity);
        #endregion

        #region //DELETE
        public Task<bool> DeleteShelterPet(Guid shelterId, Guid petId);
        public Task<bool> DeleteActivity(Guid callendarId, Guid activityId);
        public Task<bool> DeleteTempHouse(Guid tempHouseId, Guid shelterId);
        public Task<bool> DeleteShelter(Guid shelterId);
        public Task<bool> DeleteShelterUser(Guid shelterId, Guid userId);
        public Task<bool> DeletePetActivity(Guid shelterId, Guid petId, Guid activityId);

        #endregion

        #region //UTILITY
        public Task<User> FindUserById(Guid userId);
        #endregion
    }
}
