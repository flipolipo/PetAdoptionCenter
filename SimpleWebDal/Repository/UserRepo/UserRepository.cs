using Microsoft.EntityFrameworkCore;
using SimpleWebDal.Data;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.WebUser;


namespace SimpleWebDal.Repository.UserRepo;

public class UserRepository : IUserRepository
{
    private readonly PetAdoptionCenterContext _dbContext;

    public UserRepository(PetAdoptionCenterContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> GetUserById(string userId)
    {
        var foundUser = await _dbContext.Users
            .Include(b => b.BasicInformation).ThenInclude(c => c.Address)
            .Include(d => d.Roles)
            .Include(e => e.UserCalendar).ThenInclude(f => f.Activities)
            .Include(g => g.Adoptions)
            .Include(h => h.Pets).FirstOrDefaultAsync(z => z.Id == userId);
        return foundUser;
    }

    //public async Task<User> AddUser(User user)
    //{
    //    if (user == null)
    //    {
    //        throw new ArgumentNullException(nameof(user));
    //    }

    //    bool existingUser = await CheckIfUserExistInDataBase(user);
    //    if (!existingUser)
    //    {
    //        var existingAddress = await GetExistingAddressFromDataBase(user);
    //        if (existingAddress == null)
    //        {
    //            _dbContext.Addresses.Add(user.BasicInformation.Address);
    //        }
    //        else
    //        {
    //            user.BasicInformation.Address = existingAddress;
    //            user.BasicInformation.AddressId = existingAddress.Id;
    //        }

    //        _dbContext.Users.Add(user);
    //        await _dbContext.SaveChangesAsync();

    //    }
    //    return user;

    //}
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _dbContext.Users
            .Include(b => b.BasicInformation).ThenInclude(c => c.Address)
            .Include(d => d.Roles)
            .Include(e => e.UserCalendar).ThenInclude(f => f.Activities)
            .Include(g => g.Adoptions)
            .Include(h => h.Pets).ToListAsync();
    }
    public async Task<bool> UpdateUser(User user)
    {
        var foundUser = await GetUserById(user.Id);

        if (foundUser != null)
        {
            var existingAddress = await GetExistingAddressFromDataBase(user);
            if (existingAddress == null)
            {
                _dbContext.Addresses.Add(user.BasicInformation.Address);
            }
            else
            {
                user.BasicInformation.Address = existingAddress;
                user.BasicInformation.AddressId = existingAddress.Id;
            }

            foundUser.BasicInformation.Name = user.BasicInformation.Name;
            foundUser.BasicInformation.Surname = user.BasicInformation.Surname;
            foundUser.BasicInformation.Phone = user.BasicInformation.Phone;

            await _dbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteUser(string userId)
    {
        var foundUser = await GetUserById(userId);
        if (foundUser != null)
        {
            var userAddress = foundUser.BasicInformation.Address;
            var countUsersWithSameAddress = await _dbContext.Users.CountAsync(u => u.BasicInformation.AddressId == userAddress.Id);

            if (countUsersWithSameAddress == 1)
            {
                _dbContext.Addresses.Remove(userAddress);
            }

            _dbContext.Users.Remove(foundUser);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }
    public async Task<IEnumerable<Activity>> GetUserActivities(string userId)
    {
        var foundUser = await GetUserById(userId);

        if (foundUser != null && foundUser.UserCalendar != null && foundUser.UserCalendar.Activities != null)
        {
            return foundUser.UserCalendar.Activities.ToList();
        }

        return Enumerable.Empty<Activity>();
    }
    public async Task<Activity> GetUserActivityById(string userId, Guid activityId)
    {
        var foundUser = await GetUserById(userId);

        if (foundUser != null && foundUser.UserCalendar != null)
        {
            var activity = foundUser.UserCalendar.Activities.FirstOrDefault(e => e.Id == activityId);
            return activity;
        }

        return null;
    }

    public async Task<Activity> AddActivity(string userId, Activity activity)
    {
        var foundUser = await GetUserById(userId);
        if (foundUser != null && foundUser.UserCalendar != null)
        {
            if (!foundUser.UserCalendar.Activities.Contains(activity))
            {
                foundUser.UserCalendar.Activities.Add(activity);
                await _dbContext.SaveChangesAsync();
            }
        }
        return activity;
    }
    public async Task<bool> UpdateActivity(string userId, Activity activity)
    {
        var foundUser = await GetUserById(userId);
        var foundActivity = foundUser.UserCalendar.Activities.FirstOrDefault(e => e.Id == activity.Id);

        if (foundActivity != null)
        {
            foundActivity.Name = activity.Name;
            foundActivity.ActivityDate = activity.ActivityDate.ToUniversalTime();
            await _dbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteActivity(string userId, Guid activityId)
    {
        var foundUser = await GetUserById(userId);
        var foundActivity = foundUser.UserCalendar.Activities.FirstOrDefault(e => e.Id == activityId);

        if (foundUser != null && foundActivity != null)
        {
            foundUser.UserCalendar.Activities.Remove(foundActivity);
            _dbContext.SaveChanges();
            return true;
        }

        return false;
    }

    public async Task<IEnumerable<Pet>> GetAllPets()
    {
        return await _dbContext.Pets.ToListAsync();
    }

    public async Task<Pet> GetPetById(Guid id)
    {
        return await _dbContext.Pets.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<string> AddFavouritePet(string userId, Guid petId)
    {
        var foundUser = await GetUserById(userId);
        var foundPet = await GetPetById(petId);
        throw new NotImplementedException();
    }
    public async Task<IEnumerable<Pet>> GetAllFavouritePets(string id)
    {
        var foundUser = await GetUserById(id);
        if (foundUser != null)
        {
            return foundUser.Pets;
        }

        return Enumerable.Empty<Pet>();
    }

    public async Task<Pet> GetFavouritePetById(string userId, Guid petId)
    {
        var foundUser = await GetUserById(userId);

        if (foundUser != null && foundUser.Pets != null)
        {
            var pet = foundUser.Pets.FirstOrDefault(p => p.Id == petId);
            return pet;
        }
        return null;
    }

    public async Task<bool> DeleteFavouritePet(string id, Guid petId)
    {
        var foundUser = await GetUserById(id);
        var foundPetForUser = await GetFavouritePetById(id, petId);
        if (foundUser != null && foundPetForUser != null)
        {
            foundUser.Pets.Remove(foundPetForUser);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }
    public async Task<IEnumerable<Pet>> GetAllVirtualAdoptedPets()
    {
        throw new NotImplementedException();
    }


    public async Task<Pet> GetShelterPetById(Guid shelterId, Guid petId)
    {
        var foundShelter = await FindShelter(shelterId);
        return foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId);
    }

    private async Task<Shelter> FindShelter(Guid shelterId)
    {
        return await _dbContext.Shelters.FirstOrDefaultAsync(e => e.Id == shelterId);
    }

    public async Task<Pet> GetVirtualAdoptedPetById(int favouriteId)
    {
        throw new NotImplementedException();
    }




    public async Task<Pet> GetVirtualAdoptedPetById(Guid favouriteId)
    {
        throw new NotImplementedException();
    }

    private async Task<bool> CheckIfUserExistInDataBase(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.BasicInformation.Name == user.BasicInformation.Name
        && u.BasicInformation.Surname == user.BasicInformation.Surname && u.BasicInformation.Phone == user.BasicInformation.Phone
        && u.BasicInformation.Address.Street == user.BasicInformation.Address.Street
        && u.BasicInformation.Address.HouseNumber == user.BasicInformation.Address.HouseNumber
        && u.BasicInformation.Address.FlatNumber == user.BasicInformation.Address.FlatNumber
        && u.BasicInformation.Address.PostalCode == user.BasicInformation.Address.PostalCode
        && u.BasicInformation.Address.City == user.BasicInformation.Address.City);
        if (existingUser == null)
        {
            return false;
        }
        return true;
    }

    private async Task<Address> GetExistingAddressFromDataBase(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        var existingAddress = await _dbContext.Addresses.FirstOrDefaultAsync(a => a.Street == user.BasicInformation.Address.Street &&
                   a.HouseNumber == user.BasicInformation.Address.HouseNumber && a.FlatNumber == user.BasicInformation.Address.FlatNumber &&
                   a.PostalCode == user.BasicInformation.Address.PostalCode && a.City == user.BasicInformation.Address.City);

        return existingAddress;
    }


}


