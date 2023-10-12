using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework.Internal;
using SimpleWebDal.Data;
using SimpleWebDal.Models.AdoptionProccess;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.Animal.Enums;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.TemporaryHouse;
using SimpleWebDal.Models.WebUser;
using SimpleWebDal.Models.WebUser.Enums;
using SimpleWebDal.Repository.ShelterRepo;

namespace PetAdoptionCenterIntegrationTests
{
    public class ShelterRepositoryTests
    {
        private PetAdoptionCenterContext _context;
        private IConfiguration _configuration;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<PetAdoptionCenterContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use an in-memory database for testing
                .Options;

            _context = new PetAdoptionCenterContext(options);

            // Optionally, you can use an IConfiguration to configure the database connection string from appsettings.json
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            // Initialize and seed your test database with data if needed.
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public void CreateShelter_PassValidShelter_ShouldCreateShelterInDatabase()
        {
            // Arrange: Create a shelter
            var shelterId = Guid.NewGuid();
            var shelter = new Shelter
            {
                Id = shelterId,
                Name = "ShelterName",
                CalendarId = Guid.NewGuid(),
                ShelterCalendar = new CalendarActivity(),
                AddressId = Guid.NewGuid(),
                ShelterAddress = new Address(),
                ShelterDescription = "Shelter Description"
            };

            // Act: Create the shelter
            var repository = new ShelterRepository(_context); // Replace with your actual repository
            var createdShelter = repository.CreateShelter(shelter);

            // Assert: Verify that the created shelter exists in the database
            var retrievedShelter = _context.Shelters.Find(shelterId);

            Assert.IsNotNull(retrievedShelter);
            // Add more assertions as needed to verify the shelter properties
        }

        [Test]
        public void AddAdoption_PassValidAdoption_ShouldAddAdoptionToShelter()
        {
            // Arrange: Create a shelter, a pet, and a user
            var shelterId = Guid.NewGuid();
            var shelter = new Shelter
            {
                Id = shelterId,
                Name = "ShelterName",
                CalendarId = Guid.NewGuid(),
                ShelterCalendar = new CalendarActivity(),
                AddressId = Guid.NewGuid(),
                ShelterAddress = new Address(),
                ShelterDescription = "Shelter Description"
            };
            var petId = Guid.NewGuid();
            var pet = new Pet
            {
                Id = petId,
                Gender = PetGender.Male,
                Type = PetType.Dog,
                BasicHealthInfoId = Guid.NewGuid(),
                Description = "PetDDescription",
                CalendarId = Guid.NewGuid(),
                Status = PetStatus.AtShelter,
                AvaibleForAdoption = true,
                Image = new byte[] { 0x12, 0x34, 0x56 },
                ShelterId = shelterId
            };
            var userId = Guid.NewGuid();
            var user = new User
            {
                Id = userId,
                UserName = "SampleUser",
                Email = "SampleUser@test.com"
            };

            var adoption = new Adoption
            {
                Id = Guid.NewGuid(),
                PetId = petId,
                UserId = userId,
                PreAdoptionPoll = true,
                ContractAdoption = true,
                Meetings = true,
            };

            // Act: Add the adoption to the shelter
            var repository = new ShelterRepository(_context); // Replace with your actual repository
            var addedAdoption = repository.AddAdoption(shelter.Id, pet.Id, user.Id, adoption);

            // Assert: Verify that the added adoption exists in the shelter
            var shelterWithAdoption = _context.Shelters
                .Include(s => s.Adoptions)
                .SingleOrDefault(s => s.Id == shelter.Id);

            Assert.IsNotNull(shelterWithAdoption);
            //Assert.That(shelterWithAdoption.Adoptions.Contains(addedAdoption), Is.True);
        }
    }
}



