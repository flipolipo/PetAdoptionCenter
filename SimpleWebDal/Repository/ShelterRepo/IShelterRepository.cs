using Microsoft.AspNetCore.Http;
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
        // public Task<IEnumerable<User>> GetShelterUsersByRole(Guid shelterId, RoleName role);
        public Task<User> GetShelterUserById(Guid shelterId, Guid userId);
        public Task<IEnumerable<User>> GetShelterUsers(Guid shelterId);
        public Task<Shelter> GetShelterById(Guid shelterId);
        public Task<IEnumerable<Pet>> GetAllShelterDogsOrCats(Guid shelterId, PetType type);
        public Task<IEnumerable<Pet>> GetAllShelterPets(Guid shelterId);
        public Task<Pet> GetShelterPetById(Guid shelterId, Guid petId);
        public Task<IEnumerable<Pet>> GetAllAdoptedPets(Guid shelterId);
        public Task<TempHouse> GetTempHouseById(Guid shelterId, Guid tempHouseId);
        public Task<TempHouse> GetTempHouse(Guid tempHouseId);
        public Task<IEnumerable<TempHouse>> GetAllTempHouses(Guid shelterId);
        public Task<IEnumerable<Pet>> GetAllShelterTempHousesPets(Guid shelterId);
        public Task<Pet> GetTempHousePetById(Guid shelterId, Guid petId, Guid tempHouseId);
        public Task<IEnumerable<Activity>> GetShelterActivities(Guid shelterId);
        public Task<Activity> GetShelterActivityById(Guid shelterId, Guid activityId);
        public Task<IEnumerable<Disease>> GetAllPetDiseases(Guid shelterId, Guid petId);
        public Task<Disease> GetPetDiseaseById(Guid shelterId, Guid petId, Guid diseaseId);
        public Task<IEnumerable<Vaccination>> GetAllPetVaccinations(Guid shelterId, Guid petId);
        public Task<Vaccination> GetPetVaccinationById(Guid shelterId, Guid petId, Guid vaccinationId);
        public Task<IEnumerable<Activity>> GetAllPetActivities(Guid shelterId, Guid petId);
        public Task<Activity> GetPetActivityById(Guid shelterId, Guid activityId, Guid petId);
        public Task<IEnumerable<Adoption>> GetAllShelterAdoptions(Guid shelterId);
        public Task<Adoption> GetShelterAdoptionById(Guid shelterId, Guid adoptionId);
        public Task<IEnumerable<Pet>> GetAllAvaiblePets(Guid shelterId);
        public Task<Adoption> GetAdoptionFromDataBaseById(Guid adoptionId);
        public Task<BasicHealthInfo> GetPetBasicHealthInfo(Guid shelterId, Guid petId);
        public Task<BasicHealthInfo> GetPetBasicHealthInfoById(Guid shelterId, Guid petId, Guid basicHealtInfoId);



        #endregion

        #region //POST
        public Task<Shelter> CreateShelter(Shelter shelter);
        public Task<TempHouse> InitializeTempHouseForPet(Guid shelterId, Guid userId, Guid petId, TempHouse tempHouse);
        public Task<TempHouse> ChooseMeetingDatesForTempHouseProcess(Guid petId, Guid tempHouseId, Guid activityId);
        public Task<TempHouse> ConfirmYourChooseForTempHouse(Guid tempHouseId, Guid petId);
        public Task<TempHouse> ConfirmToAddAnotherPetToTempHouse(Guid tempHouseId, Guid petId);
        public Task<TempHouse> ChooseMeetingDatesForKnowAnotherPet(Guid shelterId, Guid petId, Guid userId, Guid tempHouseId, Guid activityId);
        public Task<Pet> AddPet(Guid shelterId, Pet pet);
        public Task<Activity> AddActivityToCalendar(Guid shelterId, Activity activity);
        public Task<Activity> AddPetActivityToCalendar(Guid shelterId, Guid petId, Activity activity);
        public Task<Vaccination> AddPetVaccination(Guid shelterId, Guid petId, Vaccination vaccination);
        public Task<Disease> AddPetDisease(Guid shelterId, Guid petId, Disease disease);
        public Task<Adoption> InitializePetAdoption(Guid shelterId, Guid petId, Guid userId, Adoption adoption);
        public Task<Adoption> ChooseMeetingDatesForAdoption(Guid shelterId, Guid petId, Guid userId, Guid adoptionId, Guid activityId);
        public Task<Adoption> PetAdoptionMeetingsDone(Guid adoptionId);
        public Task<Adoption> ContractForPetAdoption(Guid shelterId, Guid petId, Guid userId, Guid adoptionId, string contractAdoption);
        public Task<bool> AddShelterUser(Guid shelterId, Guid userId, Role role);

        #endregion

        #region //PUT
        public Task<bool> UpdateShelter(Guid shelterId, string name, string description, string street, string houseNumber, string postalCode, string city, string phone, string bankNumber, IFormFile image);
        public Task<bool> UpdateShelterPet(Guid shelterId, Guid petId, PetGender gender, PetType type, string description, PetStatus status, bool avaibleForAdoption, IFormFile image);
        public Task<bool> UpdateShelterActivity(Guid shelterId, Activity activity);
        public Task<bool> UpdatePetActivity(Guid shelterId, Guid petId, Activity activity);
        public Task<bool> UpdateAdoption(Guid shelterId, Guid userId, Adoption adoption);
        public Task<bool> UpdateTempHouse(TempHouse tempHouse);
        public Task<bool> UpdatePetDisease(Guid shelterId, Guid petId, Disease disease);
        public Task<bool> UpdatePetVaccination(Guid shelterId, Guid petId, Vaccination vaccination);
        public Task<bool> UpdatePetBasicHealthInfo(Guid shelterId, Guid petId, string name, int age, Size size, bool isNeutred);

        #endregion

        #region //DELETE
        public Task<bool> DeleteShelterPet(Guid shelterId, Guid petId);
        public Task<bool> DeleteActivity(Guid callendarId, Guid activityId);
        public Task<bool> DeleteTempHouse(Guid tempHouseId, Guid shelterId, Guid petId, Guid userId);
        public Task<bool> DeleteShelter(Guid shelterId);
        public Task<bool> DeleteShelterUser(Guid shelterId, Guid userId);
        public Task<bool> DeletePetActivity(Guid shelterId, Guid petId, Guid activityId);
        public Task<bool> DeleteAdoption(Guid shelterId, Guid adoptionId, Guid petId, Guid userId);
        public Task<bool> DeletePetDisease(Guid shelterId, Guid petId, Guid diseaseId);
        public Task<bool> DeletePetVaccination(Guid shelterId, Guid petId, Guid vaccinationId);



        #endregion

        #region //UTILITY
        public Task<User> FindUserById(Guid userId);
        #endregion
    }
}
