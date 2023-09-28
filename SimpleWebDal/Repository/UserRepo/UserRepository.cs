using Microsoft.EntityFrameworkCore;
using SimpleWebDal.Data;
using SimpleWebDal.Models.AdoptionProccess;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.Animal.Enums;
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

    public async Task<User> GetUserById(Guid userId)
    {
        var foundUser = await _dbContext.Users.Include(a => a.Credentials)
            .Include(b => b.BasicInformation).ThenInclude(c => c.Address)
            .Include(d => d.Roles)
            .Include(e => e.UserCalendar).ThenInclude(f => f.Activities)
            .Include(g => g.Adoptions)
            .Include(h => h.PetList).FirstOrDefaultAsync(z => z.Id == userId);
        return foundUser;
    }
    public async Task<User> AddUser(User user)
    {
        var existingAddress = await _dbContext.Addresses.FirstOrDefaultAsync(a => a.Street == user.BasicInformation.Address.Street &&
        a.HouseNumber == user.BasicInformation.Address.HouseNumber && a.FlatNumber == user.BasicInformation.Address.FlatNumber &&
        a.PostalCode == user.BasicInformation.Address.PostalCode && a.City == user.BasicInformation.Address.City);
        if (existingAddress == null)
        {
            _dbContext.Addresses.Add(user.BasicInformation.Address);
        }
        else
        {
            user.BasicInformation.Address = existingAddress;
            user.BasicInformation.AddressId = existingAddress.Id;
        }

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        return user;
    }
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _dbContext.Users.Include(a => a.Credentials)
            .Include(b => b.BasicInformation).ThenInclude(c => c.Address)
            .Include(d => d.Roles)
            .Include(e => e.UserCalendar).ThenInclude(f => f.Activities)
            .Include(g => g.Adoptions)
            .Include(h => h.PetList).ToListAsync();
    }
    public async Task<bool> DeleteUser(Guid userId)
    {
        var foundUser = await GetUserById(userId);
        if (foundUser != null)
        {
            var userAddress = foundUser.BasicInformation.Address;
            var countUsersWithSameAddress = _dbContext.Users.Count(u => u.BasicInformation.AddressId == userAddress.Id);

            if (countUsersWithSameAddress == 1)
            {
                _dbContext.Addresses.Remove(userAddress);
            }

            _dbContext.Users.Remove(foundUser);
            _dbContext.SaveChanges();
            return true;
        }
        return false;
    }

    private async Task<Shelter> FindShelter(Guid shelterId)
    {
        return await _dbContext.Shelters.FirstOrDefaultAsync(e => e.Id == shelterId);
    }

    public async Task<bool> UpdateUser(User user)
    {
        var foundUser = await GetUserById(user.Id);

        if (foundUser != null)
        {
            foundUser.Credentials.Username = user.Credentials.Username;
            foundUser.Credentials.Password = user.Credentials.Password;
            foundUser.BasicInformation.Name = user.BasicInformation.Name;
            foundUser.BasicInformation.Surname = user.BasicInformation.Surname;
            foundUser.BasicInformation.Phone = user.BasicInformation.Phone;
            foundUser.BasicInformation.Email = user.BasicInformation.Email;
            foundUser.BasicInformation.Address.Street = user.BasicInformation.Address.Street;
            foundUser.BasicInformation.Address.HouseNumber = user.BasicInformation.Address.HouseNumber;
            foundUser.BasicInformation.Address.FlatNumber = user.BasicInformation.Address.FlatNumber;
            foundUser.BasicInformation.Address.PostalCode = user.BasicInformation.Address.PostalCode;
            foundUser.BasicInformation.Address.City = user.BasicInformation.Address.City;
            await _dbContext.SaveChangesAsync();
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
    public async Task<IEnumerable<Activity>> GetUserActivities(Guid userId)
    {
        var foundUser = await GetUserById(userId);

        if (foundUser != null && foundUser.UserCalendar != null && foundUser.UserCalendar.Activities != null)
        {
            return foundUser.UserCalendar.Activities.ToList();
        }

        return Enumerable.Empty<Activity>(); 
    }
    public async Task<Activity> GetUserActivityById(Guid userId, Guid activityId)
    {
        var foundUser = await GetUserById(userId);

        if (foundUser != null && foundUser.UserCalendar != null)
        {
            var activity = foundUser.UserCalendar.Activities.FirstOrDefault(e => e.Id == activityId);
            return activity;
        }

        return null;
    }

    public async Task<Activity> AddActivity(Guid userId, Activity activity)
    {
        var foundUser = await GetUserById(userId);
       if(foundUser != null && foundUser.UserCalendar != null)
        {
            if (!foundUser.UserCalendar.Activities.Contains(activity))
            {
                foundUser.UserCalendar.Activities.Add(activity);
                _dbContext.SaveChanges();
            }
        }
        return activity;
    }
    public async Task<bool> UpdateActivity(Guid userId, Activity activity)
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

    public async Task<bool> DeleteActivity(Guid userId, Guid activityId)
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

    //DO SKONCZENIA
    public Task<Pet> AddFavouritePet(Guid userId, Guid petId)
    {
        throw new ();
    }
    public Task<Pet> DeleteFavouritePet(int petId)
    {
        throw new NotImplementedException();
    }


    public Task<IEnumerable<Pet>> GetAllFavouritePets()
    {
        throw new NotImplementedException();
    }


   
    public Task<IEnumerable<Pet>> GetAllVirtualAdoptedPets()
    {
        throw new NotImplementedException();
    }


    public async Task<Pet> GetShelterPetById(Guid shelterId, Guid petId)
    {
        var foundShelter = await FindShelter(shelterId);
        return foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId);
    }



    public Task<Pet> GetVirtualAdoptedPetById(int favouriteId)
    {
        throw new NotImplementedException();
    }

   
   

    public Task<Pet> GetVirtualAdoptedPetById(Guid favouriteId)
    {
        throw new NotImplementedException();
    }

 
}


