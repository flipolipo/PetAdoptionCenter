using Microsoft.EntityFrameworkCore;
using SimpleWebDal.Models.AdoptionProccess;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.TemporaryHouse;
using SimpleWebDal.Models.WebUser;
using SimpleWebDal.Models.Animal.Enums;
using System;
using System.Collections.Generic;
using SimpleWebDal.Models.WebUser.Enums;
using System.Collections;

public class DbInitializer
{
    private readonly ModelBuilder modelBuilder;

    public DbInitializer(ModelBuilder modelBuilder)
    {
        this.modelBuilder = modelBuilder;
    }

    public void Seed()
    {
        // Create some sample addresses
        var addresses = new List<Address>
        {
            new Address { Id = Guid.NewGuid(), Street = "123 Main St", City = "City1", PostalCode = "12345", HouseNumber= "2" },
            new Address { Id = Guid.NewGuid(), Street = "456 Elm St", City = "City2", PostalCode = "67890",HouseNumber= "21" }
        };
        modelBuilder.Entity<Address>().HasData(addresses);

        // Create some sample basic information
        var basicInformations = new List<BasicInformation>
        {
            new BasicInformation { Id = Guid.NewGuid(), Name = "John", Surname = "Doe", Phone = "123-456-7890", AddressId = addresses[0].Id },
            new BasicInformation { Id = Guid.NewGuid(), Name = "Jane", Surname = "Smith", Phone = "987-654-3210", AddressId = addresses[1].Id }
        };
        modelBuilder.Entity<BasicInformation>().HasData(basicInformations);

        // Create some sample roles
        var roles = new List<Role>
        {
           new Role(){ Id = Guid.NewGuid(), Title = RoleName.ShelterOwner },
           new Role(){ Id = Guid.NewGuid(), Title = RoleName.User },
           new Role(){ Id = Guid.NewGuid(), Title = RoleName.Adopter },
           new Role(){ Id = Guid.NewGuid(), Title = RoleName.Contributor },
           new Role(){ Id = Guid.NewGuid(), Title = RoleName.ShelterWorker }
        };
        modelBuilder.Entity<Role>().HasData(roles);

        // Create some sample vaccines
        var vaccines = new List<Vaccination>
        {
            new Vaccination { Id = Guid.NewGuid(), VaccinationName = "Vaccine 1", Date = DateTime.UtcNow.AddDays(-30) },
            new Vaccination { Id = Guid.NewGuid(), VaccinationName = "Vaccine 2", Date = DateTime.UtcNow.AddDays(-60) }
        };
        modelBuilder.Entity<Vaccination>().HasData(vaccines);

        // Create some sample diseases
        var diseases = new List<Disease>
        {
            new Disease { Id = Guid.NewGuid(), NameOfdisease = "Disease 1", IllnessStart = DateTime.UtcNow.AddDays(-90), IllnessEnd = DateTime.UtcNow.AddDays(-60) },
            new Disease { Id = Guid.NewGuid(), NameOfdisease = "Disease 2", IllnessStart = DateTime.UtcNow.AddDays(-120), IllnessEnd = DateTime.UtcNow.AddDays(-90) }
        };
        modelBuilder.Entity<Disease>().HasData(diseases);

        // Create some sample basic health information
        var basicHealthInfos = new List<BasicHealthInfo>
        {
            new BasicHealthInfo { Id = Guid.NewGuid(), Name = "Cat 1", Age = 2, Size = Size.Small, IsNeutered = true , },
            new BasicHealthInfo { Id = Guid.NewGuid(), Name = "Dog 1", Age = 3, Size = Size.Medium, IsNeutered = false }
        };
        modelBuilder.Entity<BasicHealthInfo>().HasData(basicHealthInfos);

        // Create some sample shelters
        var shelters = new List<Shelter>
        {
            new Shelter { Id = Guid.NewGuid(), Name = "Shelter 1", AddressId = addresses[0].Id, ShelterDescription = "A shelter for animals" },
            new Shelter { Id = Guid.NewGuid(), Name = "Shelter 2",  AddressId = addresses[1].Id, ShelterDescription = "Another shelter for animals" }
        };
        modelBuilder.Entity<Shelter>().HasData(shelters);

        // Create some sample pets
        var pets = new List<Pet>
        {
            new Pet
            {
                Id = Guid.NewGuid(),
                Gender = PetGender.Male,
                Type = PetType.Cat,
                BasicHealthInfoId = basicHealthInfos[0].Id,
                Description = "Friendly cat",
                Status = PetStatus.Adopted,
                AvaibleForAdoption = true,
                ShelterId = shelters[0].Id,
               
            },
            new Pet
            {
                Id = Guid.NewGuid(),
                Gender = PetGender.Female,
                Type = PetType.Dog,
                BasicHealthInfoId = basicHealthInfos[1].Id,
                Description = "Playful dog",
                Status = PetStatus.AtShelter,
                AvaibleForAdoption = true,
                ShelterId = shelters[1].Id
            }
        };

        modelBuilder.Entity<Pet>().HasData(pets);


        // Create some sample users
        var users = new List<User>
        {
            new User { Id = Guid.NewGuid(), RefreshToken = "refreshJWTTokenstring1", RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(1) ,UserName = "user1", Email = "user1@example.com", BasicInformationId = basicInformations[0].Id },
            new User { Id = Guid.NewGuid(), RefreshToken = "refreshJWTTokenstring2", RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(5), UserName = "user2", Email = "user2@example.com", BasicInformationId = basicInformations[1].Id}
        };

        modelBuilder.Entity<User>().HasData(users);

        /*
        // Create some sample temporary houses
        var tempHouses = new List<TempHouse>
        {
            new TempHouse { Id = Guid.NewGuid(), UserId = users[0].Id, AddressId = addresses[0].Id, StartOfTemporaryHouseDate = DateTime.UtcNow.AddDays(-10), PetsInTemporaryHouse = pets },
            new TempHouse { Id = Guid.NewGuid(), UserId = users[1].Id, AddressId = addresses[1].Id, StartOfTemporaryHouseDate = DateTime.UtcNow.AddDays(-20), PetsInTemporaryHouse = null}
        };

        modelBuilder.Entity<TempHouse>().HasData(tempHouses);



        // Create some sample adoptions
        var adoptions = new List<Adoption>
        {
            new Adoption
            {
                Id = Guid.NewGuid(),
                PetId = pets[0].Id,
                UserId = users[0].Id,
                PreAdoptionPoll = false,
                ContractAdoption = false,
                Meetings = false,
                DateOfAdoption = DateTime.UtcNow
            },
            new Adoption
            {
                Id = Guid.NewGuid(),
                PetId = pets[1].Id,
                UserId = users[1].Id,
                PreAdoptionPoll = true,
                ContractAdoption = true,
                Meetings = true,
                DateOfAdoption = DateTime.UtcNow
            }
        };

        modelBuilder.Entity<Adoption>().HasData(adoptions);

        */
    }
}
