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
    public Task<User> GetUserById(int userId);
    public Task<IEnumerable<Pet>> GetAllPets();
    public Task<IEnumerable<Pet>> GetAllShelters();
    public Task<IEnumerable<Pet>> GetAllShelterPets(int shelterId);
    public Task<IEnumerable<Pet>> GetAllShelterDogsOrCats(int shelterId, PetType petType);
    public Task<Pet> GetShelterPetById(int shelterId, int petId);
    public Task<CalendarActivity> GetCalendarForUser(int userId);
    public Task<CalendarActivity> GetAllActivitiesForUser(int userId);
    public Task<CalendarActivity> GetActivityForUserById(int userId, int activityId);
    public Task<IEnumerable<Pet>> GetAllFavouritePets();
    public Task<Pet> GetFavouritePetById(int favouriteId);
    public Task<IEnumerable<Pet>> GetAllVirtualAdoptedPets();
    public Task<Pet> GetVirtualAdoptedPetById(int favouriteId);

    //POST
    public Task<Activity> AddActivity(Activity activity, int timeTableId);
    public Task<User> AddUser(string name, string userName);
    public Task<Pet> AddFavouritePet(int petId);


    //PUT or PATCH
    public void UpdateUser(User user);
    public void UpdateActivity(Activity activity);
 

    //DELETE
    public void DeleteUser(int userId);
    public void DeleteActivity(int ActivityId);
    public Task<Pet> DeleteFavouritePet(int petId);

}
