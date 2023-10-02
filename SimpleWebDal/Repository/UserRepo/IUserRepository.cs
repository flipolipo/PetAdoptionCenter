using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.Animal.Enums;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.WebUser;

namespace SimpleWebDal.Repository.UserRepo;

public interface IUserRepository
{
    //GET
    public Task<IEnumerable<User>> GetAllUsers();
    public Task<User> GetUserById(string userId);
    public Task<IEnumerable<Pet>> GetAllPets();
    public Task<Pet> GetPetById(Guid id);
    
    //public Task<IEnumerable<Pet>> GetAllShelters();
    //public Task<IEnumerable<Pet>> GetAllShelterPets(Guid shelterId);
    // public Task<IEnumerable<Pet>> GetAllShelterDogsOrCats(Guid shelterId, PetType type);
    public Task<Pet> GetShelterPetById(Guid shelterId, Guid petId);
    //public Task<CalendarActivity> GetCalendarForUser(Guid userId);
    public Task<IEnumerable<Activity>> GetUserActivities(string userId);
    public Task<Activity> GetUserActivityById(string userId, Guid activityId);
    public Task<IEnumerable<Pet>> GetAllFavouritePets(string id);
    public Task<Pet> GetFavouritePetById(string userId, Guid petId);
    public Task<IEnumerable<Pet>> GetAllVirtualAdoptedPets();
    public Task<Pet> GetVirtualAdoptedPetById(Guid favouriteId);

    //POST
    public Task<Activity> AddActivity(string userId, Activity activity);
   // public Task<User> AddUser(User user);
    public Task<Pet> AddFavouritePet(string userId, Guid petId);


    //PUT or PATCH
    public Task<bool> UpdateUser(User user);
   // public Task<bool> PartialUpdateUser(User user);

    public Task<bool> UpdateActivity(string userId, Activity activity);


    //DELETE
    public Task<bool> DeleteUser(string userId);
    public Task<bool> DeleteActivity(string userId, Guid activityId);
    public Task<bool> DeleteFavouritePet(string id, Guid petId);
    


}