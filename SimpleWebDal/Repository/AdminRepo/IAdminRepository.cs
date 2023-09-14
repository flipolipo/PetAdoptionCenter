using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.WebUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SImpleWebLogic.Repository.AdminRepo
{
    public interface IAdminRepository
    {
        public bool SaveChange();
        //GET
        public Task<IEnumerable<Shelter>> GetAllShelters();
        public Task<Shelter> GetShelterById(int shelterId);
        public Task<IEnumerable<Pet>> GetAllShelterPets(int shelterId);
        public Task<IEnumerable<Pet>> GetAllShelterDogsOrCats(int shelterId, PetType type);
        public Task<Pet> GetShelterPetById(int shelterId, int petId);
        public Task<IEnumerable<User>> GetShelterWorkers(int shelterId);
        public Task<IEnumerable<User>> GetShelterContributors(int shelterId);
        public Task<IEnumerable<User>> GetShelterWorkerById(int shelterId, int workerId);
        public Task<IEnumerable<User>> GetShelterContributorsById(int shelterId, int userId);
        public Task<IEnumerable<Adoption>> GetShelterAdoptions(int shelterId);
        public Task<IEnumerable<Adoption>> GetShelterPetAdoptions(int shelterId, int petId);
        public Task<IEnumerable<TempHouse>> GetAllShelterTempHouses(int shelterId);
        public Task<TempHouse> GetShelterTempHouseByPetId(int shelterId, petId);
        public Task<Pet> GetPetHealthInfoById(int shelterId, int petId);
        public Task<IEnumerable<User>> GetAllUsers();
        public Task<User> GetUserById(int userId);

      //POST
        public Task<Shelter> CreateShelter();
        public Task<Pet> AddPet(int shelterId);
        public Task<TimeTable> AddCallendar(int shelterId);
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