using Microsoft.EntityFrameworkCore;
using SimpleWebDal.Data;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.Animal.Enums;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.TemporaryHouse;
using SimpleWebDal.Models.TimeTable;
using SimpleWebDal.Models.WebUser;
using SImpleWebLogic.Repository.ShelterRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebDal.Repository.ShelterRepo
{
    public class ShelterRepository : IShelterRepository
    {
        public Task<TimeTable> AddCallendar(int shelterId)
        {
            throw new NotImplementedException();
        }

        public Task<User> AddContributor(int shelterId)
        {
            throw new NotImplementedException();
        }

        public Task<Pet> AddPet(int shelterId)
        {
            throw new NotImplementedException();
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

        public Task<Pet> GetAdoptedPetsById(int shelterId, int petId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Pet>> GetAllAdoptedPets(int shelterId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Pet>> GetAllShelterDogsOrCats(int shelterId, PetType type)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Pet>> GetAllShelterPets(int shelterId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Shelter>> GetAllShelters()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Pet>> GetAllShelterTempHousesPets(int shelterId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Pet>> GetAllTempHousePetsById(int shelterId, int tempHouseId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TempHouse>> GetAllTempHouses(int shelterId)
        {
            throw new NotImplementedException();
        }

        public Task<Shelter> GetShelterById(int shelterId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetShelterContributors(int shelterId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetShelterContributorsById(int shelterId, int workerId)
        {
            throw new NotImplementedException();
        }

        public Task<Pet> GetShelterPetById(int shelterId, int petId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetShelterWorkers(int shelterId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetShelterWorkersById(int shelterId, int workerId)
        {
            throw new NotImplementedException();
        }

        public Task<TempHouse> GetTempHouseById(int shelterId, int tempHouseId)
        {
            throw new NotImplementedException();
        }

        public Task<Pet> GetTempHousePetById(int shelterId, int petId, int tempHouseId)
        {
            throw new NotImplementedException();
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
