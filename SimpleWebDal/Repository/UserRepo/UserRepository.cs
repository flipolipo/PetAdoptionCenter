using Microsoft.EntityFrameworkCore;
using SimpleWebDal.Data;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.Animal.Enums;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.WebUser;
using SimpleWebDal.Models.ProfileUser;
using SimpleWebDal.Models.ProfileUser.Enums;

namespace SimpleWebDal.Repository.UserRepo;

public class UserRepository : IUserRepository
{
    private readonly PetAdoptionCenterContext _dbContext;

    public UserRepository(PetAdoptionCenterContext dbContext)
    {
        _dbContext = dbContext;
    }

    private async Task<User> FindUser(Guid userId)
    {
        var foundUser = await _dbContext.Users.FirstOrDefaultAsync(e => e.Id == userId);
        if (foundUser == null)
        {
            throw new InvalidOperationException($"User with ID {userId} not found.");
        }
        return foundUser;
    }

    private async Task<Shelter> FindShelter(Guid shelterId)
    {
        var foundShelter = await _dbContext.Shelters.FirstOrDefaultAsync(e => e.Id == shelterId);
        if (foundShelter == null)
        {
            throw new InvalidOperationException($"Shelter with ID {shelterId} not found.");
        }
        return foundShelter;
    }

    public async Task<Activity> AddActivity(Guid userId, string activityName, DateTime activityDate)
    {
        try
        {
            var foundUser = await FindUser(userId);
            if (foundUser != null && foundUser.UserCalendar != null)
            {

                var activity = new Activity()
                {
                    Id = Guid.NewGuid(),
                    Name = activityName,
                    ActivityDate = activityDate
                };
                foundUser.UserCalendar.Activities.Add(activity);
                return activity;
            }
            else
            {
                throw new InvalidOperationException($"Shelter with ID {userId} not found.");
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task<ProfileModel> AddFavouritePet(Guid userId, Guid petId, UserProfilePets favouritePet)
    {
        throw new NotImplementedException();
    }

    public async Task<User> AddUser(string username, string password, string name, string surname, string phone, string email,
        string street, string houseNumber, int flatNumber, string postalCode, string city)
    {
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Credentials = new Credentials()
            {
                Id = Guid.NewGuid(),
                Username = username,
                Password = password
            },
            BasicInformation = new BasicInformation()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Surname = surname,
                Phone = phone,
                Email = email,
                Address = new Address()
                {
                    Id = Guid.NewGuid(),
                    Street = street,
                    HouseNumber = houseNumber,
                    FlatNumber = flatNumber,
                    PostalCode = postalCode,
                    City = city
                }
            },
            UserCalendar = new CalendarActivity() { DateWithTime = DateTime.UtcNow },
        };
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        return user;
    }

    public async Task<bool> DeleteActivity(Guid userId, Guid activityId)
    {
        var foundUser = await FindUser(userId);
        var foundActivity = foundUser.UserCalendar.Activities.FirstOrDefault(e => e.Id == activityId);

        if (foundActivity != null)
        {
            foundUser.UserCalendar.Activities.Remove(foundActivity);
            return true;
        }

        return false;
    }

    public Task<Pet> DeleteFavouritePet(int petId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteUser(Guid userId)
    {
        var foundUser = await FindUser(userId);

        if (foundUser != null)
        {
            _dbContext.Users.Remove(foundUser);
            return true;
        }

        return false;
    }

    public async Task<Activity> GetUserActivityById(Guid userId, Guid activityId)
    {
        var foundUser = await FindShelter(userId);
        var activity = foundUser.ShelterCalendar.Activities.FirstOrDefault(e => e.Id == activityId);
        return activity;
    }

    public async Task<IEnumerable<Activity>> GetUserActivities(Guid userId)
    {
        var foundUser = await FindUser(userId);
        var activities = foundUser.UserCalendar.Activities.ToList();
        return activities;
    }

    public Task<IEnumerable<Pet>> GetAllFavouritePets()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Pet>> GetAllPets()
    {
        return await _dbContext.Pets.ToListAsync();
    }

    public async Task<IEnumerable<Pet>> GetAllShelterDogsOrCats(Guid shelterId, PetType type)
    {
        var foundShelter = await FindShelter(shelterId);
        var pets = new List<Pet>();
        foreach (var pet in foundShelter.ShelterPets)
        {
            if (pet.Type.Equals(type))
                pets.Add(pet);
        }
        return pets;
    }

    public async Task<IEnumerable<Pet>> GetAllShelterPets(Guid shelterId)
    {
        var foundShelter = await FindShelter(shelterId);
        return foundShelter.ShelterPets.ToList();
    }

    public async Task<IEnumerable<Shelter>> GetAllShelters()
    {
        return await _dbContext.Shelters.ToListAsync();
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public Task<IEnumerable<Pet>> GetAllVirtualAdoptedPets()
    {
        throw new NotImplementedException();
    }

    public Task<CalendarActivity> GetCalendarForUser(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<Pet> GetFavouritePetById(int favouriteId)
    {
        throw new NotImplementedException();
    }

    public async Task<Pet> GetShelterPetById(Guid shelterId, Guid petId)
    {
        var foundShelter = await FindShelter(shelterId);
        return foundShelter.ShelterPets.FirstOrDefault(e => e.Id == petId);
    }

    public async Task<User> GetUserById(Guid userId)
    {
        var foundUser = await FindUser(userId);
        return foundUser;
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

    public async Task<bool> UpdateActivity(Guid userId, Guid activityId, string name, DateTime date)
    {
        var foundUser = await FindUser(userId);
        var foundActivity = foundUser.UserCalendar.Activities.FirstOrDefault(e => e.Id == activityId);

        if (foundActivity != null)
        {
            foundActivity.ActivityDate = date;
            foundActivity.Name = name;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> UpdateUser(Guid userId, string username, string password, string name, string surname, string phone, string email,
        string street, string houseNumber, int flatNumber, string postalCode, string city)
    {
        var foundUser = await FindUser(userId);

        if (foundUser != null)
        {
            foundUser.Credentials.Username = username;
            foundUser.Credentials.Password = password;
            foundUser.BasicInformation.Name = name;
            foundUser.BasicInformation.Surname = surname;
            foundUser.BasicInformation.Phone = phone;
            foundUser.BasicInformation.Email = email;
            foundUser.BasicInformation.Address.Street = street;
            foundUser.BasicInformation.Address.HouseNumber = houseNumber;
            foundUser.BasicInformation.Address.FlatNumber = flatNumber;
            foundUser.BasicInformation.Address.PostalCode = postalCode;
            foundUser.BasicInformation.Address.City = city;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }
}


