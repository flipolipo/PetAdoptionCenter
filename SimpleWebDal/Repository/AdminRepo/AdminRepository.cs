using SimpleWebDal.Models.AdoptionProccess;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.Animal.Enums;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.TemporaryHouse;
using SimpleWebDal.Models.WebUser;
using SImpleWebLogic.Repository.AdminRepo;

namespace SimpleWebDal.Repository.AdminRepo;

public class AdminRepository : IAdminRepository
{
    public Task<CalendarActivity> AddCallendar(int shelterId)
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

    public Task<IEnumerable<TempHouse>> GetAllShelterTempHouses(int shelterId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public Task<Pet> GetPetHealthInfoById(int shelterId, int petId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Adoption>> GetShelterAdoptions(int shelterId)
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

    public Task<IEnumerable<User>> GetShelterContributorsById(int shelterId, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Adoption>> GetShelterPetAdoptions(int shelterId, int petId)
    {
        throw new NotImplementedException();
    }

    public Task<Pet> GetShelterPetById(int shelterId, int petId)
    {
        throw new NotImplementedException();
    }

    public Task<TempHouse> GetShelterTempHouseByPetId(int shelterId, int petId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetShelterWorkerById(int shelterId, int workerId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetShelterWorkers(int shelterId)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserById(int userId)
    {
        throw new NotImplementedException();
    }

    public bool SaveChange()
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