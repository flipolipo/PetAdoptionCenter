using Microsoft.EntityFrameworkCore;
using SimpleWebDal.Data;
using SimpleWebDal.Exceptions.UserRepository;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.WebUser;
using System.Data;

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
        if (userId == Guid.Empty)
        {
            throw new UserValidationException("User ID cannot be empty.");
        }
        var foundUser = await _dbContext.Users
            .Include(b => b.BasicInformation).ThenInclude(c => c.Address)
            .Include(d => d.Roles)
            .Include(e => e.UserCalendar).ThenInclude(f => f.Activities)
            .Include(g => g.Adoptions)
            .Include(h => h.Pets)
            .FirstOrDefaultAsync(z => z.Id == userId);
        return foundUser;
    }
    /*
    public async Task<User> AddUser(User user)
    {
        if (user == null)
        {
            throw new UserValidationException("User object cannot be null.");
        }

        bool existingUser = await CheckIfUserExistInDataBase(user);
        if (!existingUser)
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

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

        } else
        {
            throw new UserValidationException("A user with the data provided already exists");
        }
        return user;
    }
    */
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
        if (user.Id == Guid.Empty)
        {
            throw new UserValidationException("User ID cannot be empty.");
        }
        var foundUser = await GetUserById(user.Id) ?? throw new UserValidationException("User object cannot be null.");
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

    public async Task<bool> DeleteUser(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            throw new UserValidationException("User ID cannot be empty.");
        }
        var foundUser = await GetUserById(userId) ?? throw new UserValidationException("User object cannot be null.");
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
    public async Task<IEnumerable<Activity>> GetUserActivities(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            throw new UserValidationException("User ID cannot be empty.");
        }
        var foundUser = await GetUserById(userId) ?? throw new UserValidationException("User object cannot be null.");
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
        if (foundUser != null && foundUser.UserCalendar != null)
        {
            var foundActivity = foundUser.UserCalendar.Activities.FirstOrDefault(a => a.Name == activity.Name && a.StartActivityDate == activity.StartActivityDate && a.EndActivityDate == activity.EndActivityDate);
            if (!foundUser.UserCalendar.Activities.Contains(foundActivity))
            {
                foundUser.UserCalendar.Activities.Add(activity);
                await _dbContext.SaveChangesAsync();
            } else
            {
                throw new Exception("Activity is already exist");
            }
          
        }
        return activity;
    }
    public async Task<bool> UpdateActivity(Guid userId, Activity activity)
    {
        var foundUser = await GetUserById(userId);
        var foundActivity = foundUser.UserCalendar.Activities.FirstOrDefault(e => e.Id == activity.Id);

        if (foundUser != null && foundActivity != null)
        {
            foundActivity.Name = activity.Name;
            foundActivity.StartActivityDate = activity.StartActivityDate.ToUniversalTime();
            foundActivity.EndActivityDate = activity.EndActivityDate.ToUniversalTime();
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
    public async Task<IEnumerable<Role>> GetAllUserRoles(Guid id)
    {
        var foundUser = await GetUserById(id);
        if (foundUser != null && foundUser.Roles != null)
        {
            return foundUser.Roles;
        }
        return Enumerable.Empty<Role>();
    }
    public async Task<Role> GetUserRoleById(Guid id, Guid roleId)
    {
        var foundUser = await GetUserById(id);
        var role = foundUser.Roles.FirstOrDefault(r => r.Id == roleId);
        if (foundUser != null && role != null)
        {
            return role;
        }
        return null;
    }

    public async Task<Role> AddRole(Guid id, Role role)
    {
        var foundUser = await GetUserById(id);
        var userContainsRole = foundUser.Roles.Contains(role);
        if (foundUser != null && !userContainsRole)
        {
            foundUser.Roles.Add(role);
            await _dbContext.SaveChangesAsync();
        }
        return role;
    }

    public async Task<bool> DeleteUserRole(Guid userId, Guid roleId)
    {
        var foundUser = await GetUserById(userId);
        var foundRole = foundUser.Roles.FirstOrDefault(r => r.Id == roleId);

        if (foundUser != null && foundRole != null)
        {
            foundUser.Roles.Remove(foundRole);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }
    public async Task<bool> UpdateUserRole(Guid userId, Role role)
    {
        var foundUser = await GetUserById(userId);
        var foundRole = foundUser.Roles.FirstOrDefault(r => r.Id == role.Id);


        if (foundUser != null && foundRole != null)
        {
            foundRole.Title = role.Title;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }
    public async Task<IEnumerable<Pet>> GetAllPets()
    {
        return await _dbContext.Pets.Include(p => p.BasicHealthInfo).ThenInclude(p => p.Vaccinations)
            .Include(p => p.BasicHealthInfo).ThenInclude(p => p.MedicalHistory)
            .Include(p => p.Calendar).ThenInclude(p => p.Activities)
            .Include(p => p.Users).ToListAsync();
    }

    public async Task<Pet> GetPetById(Guid id)
    {
        return await _dbContext.Pets.Include(p => p.BasicHealthInfo).ThenInclude(p => p.Vaccinations)
            .Include(p => p.BasicHealthInfo).ThenInclude(p => p.MedicalHistory)
            .Include(p => p.Calendar).ThenInclude(p => p.Activities)
            .Include(p => p.Users)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Pet> AddFavouritePet(Guid userId, Guid petId)
    {
        var foundUser = await GetUserById(userId);
        var foundPet = await GetPetById(petId);
        if (foundUser != null && foundPet != null)
        {
            if (!foundUser.Pets.Contains(foundPet))
            {
                foundUser.Pets.Add(foundPet);
                foundPet.Users.Add(foundUser);
                await _dbContext.SaveChangesAsync();
                return foundPet;
            }
        }
        return null;
    }
    public async Task<IEnumerable<Pet>> GetAllFavouritePets(Guid id)
    {
        var foundUser = await GetUserById(id);
        if (foundUser != null)
        {
            return foundUser.Pets;
        }

        return Enumerable.Empty<Pet>();
    }

    public async Task<Pet> GetFavouritePetById(Guid userId, Guid petId)
    {
        var foundUser = await GetUserById(userId);

        if (foundUser != null && foundUser.Pets != null)
        {
            var pet = foundUser.Pets.FirstOrDefault(p => p.Id == petId);
            return pet;
        }
        return null;
    }

    public async Task<bool> DeleteFavouritePet(Guid id, Guid petId)
    {
        var foundUser = await GetUserById(id);
        var foundPetForUser = await GetFavouritePetById(id, petId);
        if (foundUser != null && foundPetForUser != null)
        {
            foundUser.Pets.Remove(foundPetForUser);
            foundPetForUser.Users.Remove(foundUser);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }


    public async Task<IEnumerable<Pet>> GetAllAdoptedPet()
    {
        var pets = await GetAllPets();
        return pets.Where(pet => pet.Status == Models.Animal.Enums.PetStatus.Adopted);
    }
    public async Task<Pet> GetAdoptedPetById(Guid id)
    {
        var foundPets = await GetAllAdoptedPet();
        return foundPets.FirstOrDefault(pet => pet.Id == id);
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

    public async Task<IEnumerable<Pet>> GetAllPetsAvailableForAdoption()
    {
        var pets = await GetAllPets();
        return pets.Where(pet => pet.AvaibleForAdoption == true);
    }
}


