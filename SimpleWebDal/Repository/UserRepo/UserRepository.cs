using Microsoft.EntityFrameworkCore;
using SimpleWebDal.Data;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.Animal.Enums;
using SimpleWebDal.Models.Calendar;
using SimpleWebDal.Models.WebUser;

namespace SimpleWebDal.Repository.UserRepo;

public class UserRepository : IUserRepository
{
    private readonly PetAdoptionCenterContext _dbContext;

    public UserRepository(PetAdoptionCenterContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Task<Activity> AddActivity(Activity activity, int timeTableId)
    {
        throw new NotImplementedException();
    }

    public Task<Pet> AddFavouritePet(int petId)
    {
        throw new NotImplementedException();
    }

    public Task<User> AddUser(string name, string userName)
    {
        throw new NotImplementedException();
    }

    public void DeleteActivity(int ActivityId)
    {
        throw new NotImplementedException();
    }

    public Task<Pet> DeleteFavouritePet(int petId)
    {
        throw new NotImplementedException();
    }

    public void DeleteUser(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<TimeTable> GetActivityForUserById(int userId, int activityId)
    {
        throw new NotImplementedException();
    }

    public Task<TimeTable> GetAllActivitiesForUser(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Pet>> GetAllFavouritePets()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Pet>> GetAllPets()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Pet>> GetAllShelterCats(int shelterId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Pet>> GetAllShelterDogs(int shelterId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Pet>> GetAllShelterDogsOrCats(int shelterId, PetType petType)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Pet>> GetAllShelterPets(int shelterId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Pet>> GetAllShelters()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public Task<IEnumerable<Pet>> GetAllVirtualAdoptedPets()
    {
        throw new NotImplementedException();
    }

    public Task<TimeTable> GetCalendarForUser(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<Pet> GetFavouritePetById(int favouriteId)
    {
        throw new NotImplementedException();
    }

    public Task<Pet> GetShelterPetById(int shelterId, int petId)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserById(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<Pet> GetVirtualAdoptedPetById(int favouriteId)
    {
        throw new NotImplementedException();
    }

    public bool SaveChange()
    {
        using var dbContext = new PetAdoptionCenterContext();
        return (dbContext.SaveChanges() >= 0);
    }

    public void UpdateActivity(Activity activity)
    {
        throw new NotImplementedException();
    }

    public void UpdateUser(User user)
    {
        throw new NotImplementedException();
    }
}
