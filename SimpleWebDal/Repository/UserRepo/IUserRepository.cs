using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.Animal.Enums;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.WebUser;

namespace SimpleWebDal.Repository.UserRepo;

public interface IUserRepository
{
    bool SaveChange();
    //GET
    public Task<IEnumerable<User>> GetAllUsers();
    public Task<User> GetUserById(Guid userId);
    public Task<IEnumerable<Pet>> GetAllPets();
    public Task<IEnumerable<Pet>> GetAllShelters();
    public Task<IEnumerable<Pet>> GetAllShelterPets(Guid shelterId);
    public Task<IEnumerable<Pet>> GetAllShelterDogsOrCats(Guid shelterId, PetType type);
    public Task<Pet> GetShelterPetById(Guid shelterId, Guid petId);
    public Task<CalendarActivity> GetCalendarForUser(int userId);
    public Task<IEnumerable<Activity>> GetUserActivities(Guid userId);
    public Task<Activity> GetUserActivityById(Guid userId, Guid activityId);
    public Task<IEnumerable<Pet>> GetAllFavouritePets();
    public Task<Pet> GetFavouritePetById(int favouriteId);
    public Task<IEnumerable<Pet>> GetAllVirtualAdoptedPets();
    public Task<Pet> GetVirtualAdoptedPetById(int favouriteId);

    //POST
    public Task<Activity> AddActivity(Guid userId, string activityName, DateTime activityDate);
    public Task<User> AddUser(string username, string password, string name, string surname, string phone, string email,
        string street, string houseNumber, int flatNumber, string postalCode, string city);
    public Task<Pet> AddFavouritePet(Guid userId, Guid petId);


    //PUT or PATCH
    public Task<bool> UpdateUser(Guid userId, string username, string password, string name, string surname, string phone, string email,
        string street, string houseNumber, int flatNumber, string postalCode, string city);
    public Task<bool> UpdateActivity(Guid userId, Guid activityId, string name, DateTime date);


    //DELETE
    public Task<bool> DeleteUser(Guid userId);
    public Task<bool> DeleteActivity(Guid userId, Guid activityId);
    public Task<Pet> DeleteFavouritePet(int petId);

}