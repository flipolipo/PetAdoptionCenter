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
        public Task<Shelter> GetShelterById(int shelterId); 
        public Task<IEnumerable<Pet>> GetAllShelterDogsOrCats(int shelterId, PetType type);
        public Task<IEnumerable<Pet>> GetAllShelterPets(int shelterId);
        public Task<Pet> GetShelterPetById(int shelterId, int petId);
        public Task<IEnumerable<User>> GetShelterWorkers(int shelterId);
        public Task<IEnumerable<User>> GetShelterContributors(int shelterId);
        public Task<IEnumerable<User>> GetShelterWorkersById(int shelterId, int workerId);
        public Task<IEnumerable<User>> GetShelterContributorsById(int shelterId, int workerId);
        public Task<IEnumerable<Pet>> GetAllAdoptedPets(int shelterId);
        public Task<Pet> GetAdoptedPetsById(int shelterId, int petId);
        public Task<TempHouse> GetTempHouseById(int shelterId,int tempHouseId);
        public Task<IEnumerable<TempHouse>> GetAllTempHouses(int shelterId);
        public Task<IEnumerable<Pet>> GetAllShelterTempHousesPets(int shelterId);
        public Task<IEnumerable<Pet>> GetAllTempHousePetsById(int shelterId, int tempHouseId); 
        public Task<Pet> GetTempHousePetById(int shelterId, int petId, int tempHouseId);

        //POST
        public Task<Shelter> CreateShelter();
        public Task<Pet> AddPet(int shelterId);
        public Task<CalendarActivity> AddCallendar(int shelterId);
        public Task<User> AddWorker(int shelterId);
        public Task<User> AddContributor(int shelterId);
        public Task<TempHouse> AddTempHouse(int shelterId);

        //PUT
        public void UpdateShelter(int shelterId);
        public void UpdateShelterPet(int shelterId, int petId);
        public void UpdateCallendar(int shelterId, int callendarId);

        //DELETE
        public void DeleteShelterPet(int shelterId, int petId);
        public void DeleteCallendar(int callendarId, int shelterId);
        public void DeleteWorker(int shelterId, int userId);
        public void DeleteContributor(int shelterId, int userId);
        public void DeleteTempHouse(int tempHouseId, int shelterId);
        public void DeleteShelter(int shelterId);
    }
}
