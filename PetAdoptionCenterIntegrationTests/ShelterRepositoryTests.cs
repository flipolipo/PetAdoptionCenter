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
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new PetAdoptionCenterContext(options);

            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public void CreateShelter_PassValidShelter_ShouldCreateShelterInDatabase()
        {
            // Arrange
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

            // Act
            var repository = new ShelterRepository(_context);
            var createdShelter = repository.CreateShelter(shelter);

            // Assert
            var retrievedShelter = _context.Shelters.Find(shelterId);

            Assert.That(retrievedShelter, Is.Not.Null);
        }

        [Test]
        public async Task GetAllShelters_ShouldReturnShelters()
        {
            // Arrange
            var shelters = new List<Shelter>
            {
                new Shelter
                {
                    Id = Guid.NewGuid(),
                    Name = "ShelterName1",
                    CalendarId = Guid.NewGuid(),
                    ShelterCalendar = new CalendarActivity(),
                    AddressId = Guid.NewGuid(),
                    ShelterAddress = new Address
                    {
                        City = "city1",
                        FlatNumber = 1,
                        HouseNumber = "1",
                        PostalCode = "11-111",
                        Street ="street1"
                    },
                    ShelterDescription = "Shelter Description1"
                },
                new Shelter
                {
                    Id = Guid.NewGuid(),
                    Name = "ShelterName2",
                    CalendarId = Guid.NewGuid(),
                    ShelterCalendar = new CalendarActivity(),
                    AddressId = Guid.NewGuid(),
                    ShelterAddress = new Address
                      {
                        City = "city12",
                        FlatNumber = 2,
                        HouseNumber = "2",
                        PostalCode = "22-222",
                        Street ="street2"
                    },
                    ShelterDescription = "Shelter Description2"
                }
            };

            _context.AddRange(shelters);
            _context.SaveChanges();

            // Act
            var repository = new ShelterRepository(_context);
            var result = await repository.GetAllShelters();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        public async Task GetAllShelterPets_ShouldReturnShelterPets()
        {
            // Arrange
            var shelterId = Guid.NewGuid();
            var shelter = new Shelter
            { 
                    Id = Guid.NewGuid(),
                    Name = "ShelterName1",
                    CalendarId = Guid.NewGuid(),
                    ShelterCalendar = new CalendarActivity(),
                    AddressId = Guid.NewGuid(),
                    ShelterAddress = new Address
                    {
                        City = "city1",
                        FlatNumber = 1,
                        HouseNumber = "1",
                        PostalCode = "11-111",
                        Street ="street1"
                    },
                    ShelterDescription = "Shelter Description1"
            
            };

            var pets = new List<Pet>
            {
                new Pet
                {
                    Id = Guid.NewGuid(),
                    Gender = PetGender.Male,
                    Type = PetType.Dog,
                    BasicHealthInfoId = Guid.NewGuid(),
                    Description = "PetDescription1",
                    CalendarId = Guid.NewGuid(),
                    Status = PetStatus.AtShelter,
                    AvaibleForAdoption = true,
                    Image = new byte[] { 0x12, 0x34, 0x56 },
                    ShelterId = shelter.Id
                },
                new Pet
                {
                    Id = Guid.NewGuid(),
                    Gender = PetGender.Female,
                    Type = PetType.Cat,
                    BasicHealthInfoId = Guid.NewGuid(),
                    Description = "PetDescription2",
                    CalendarId = Guid.NewGuid(),
                    Status = PetStatus.OnAdoptionProccess,
                    AvaibleForAdoption = false,
                    Image = new byte[] { 0x12, 0x34, 0x56 },
                    ShelterId = shelter.Id
                }
            };

            shelter.ShelterPets = pets;

            _context.Add(shelter);
            _context.SaveChanges();

            // Act
            var repository = new ShelterRepository(_context);
            var result = await repository.GetAllShelterPets(shelter.Id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        public async Task GetAllShelterDogsOrCats_ShouldReturnDogsOrCats()
        {
            // Arrange
            var shelter = new Shelter
            {
                Id = Guid.NewGuid(),
                Name = "ShelterName1",
                CalendarId = Guid.NewGuid(),
                ShelterCalendar = new CalendarActivity(),
                AddressId = Guid.NewGuid(),
                ShelterAddress = new Address
                {
                    City = "city1",
                    FlatNumber = 1,
                    HouseNumber = "1",
                    PostalCode = "11-111",
                    Street = "street1"
                },
                ShelterDescription = "Shelter Description1"
            };

            var pets = new List<Pet>
            {
                new Pet
                {
                    Id = Guid.NewGuid(),
                    Gender = PetGender.Female,
                    Type = PetType.Cat,
                    BasicHealthInfoId = Guid.NewGuid(),
                    Description = "PetDescription1",
                    CalendarId = Guid.NewGuid(),
                    Status = PetStatus.OnAdoptionProccess,
                    AvaibleForAdoption = false,
                    Image = new byte[] { 0x12, 0x34, 0x56 },
                    ShelterId = shelter.Id
                },
                new Pet
                {
                    Id = Guid.NewGuid(),
                    Gender = PetGender.Male,
                    Type = PetType.Dog,
                    BasicHealthInfoId = Guid.NewGuid(),
                    Description = "PetDescription2",
                    CalendarId = Guid.NewGuid(),
                    Status = PetStatus.AtShelter,
                    AvaibleForAdoption = true,
                    Image = new byte[] { 0x12, 0x34, 0x56 },
                    ShelterId = shelter.Id
                },
                new Pet
                {
                    Id = Guid.NewGuid(),
                    Gender = PetGender.Male,
                    Type = PetType.Cat,
                    BasicHealthInfoId = Guid.NewGuid(),
                    Description = "PetDescription3",
                    CalendarId = Guid.NewGuid(),
                    Status = PetStatus.TemporaryHouse,
                    AvaibleForAdoption = true,
                    Image = new byte[] { 0x12, 0x34, 0x56 },
                    ShelterId = shelter.Id
                }
            };

            shelter.ShelterPets = pets;

            _context.Add(shelter);
            _context.SaveChanges();

            // Act
            var repository = new ShelterRepository(_context);
            var resultDogs = await repository.GetAllShelterDogsOrCats(shelter.Id, PetType.Dog);
            var resultCats = await repository.GetAllShelterDogsOrCats(shelter.Id, PetType.Cat);

            // Assert
            Assert.That(resultDogs, Is.Not.Null);
            Assert.That(resultCats, Is.Not.Null);

            Assert.That(resultDogs.Count(), Is.EqualTo(1)); // Expecting 2 dogs
            Assert.That(resultCats.Count(), Is.EqualTo(2)); // Expecting 2 cats
        }

        [Test]
        public async Task GetAllAdoptedPets_ShouldReturnAdoptedPets()
        {
            // Arrange
            var shelter = new Shelter
            {
                Id = Guid.NewGuid(),
                Name = "ShelterName1",
                CalendarId = Guid.NewGuid(),
                ShelterCalendar = new CalendarActivity(),
                AddressId = Guid.NewGuid(),
                ShelterAddress = new Address
                {
                    City = "city1",
                    FlatNumber = 1,
                    HouseNumber = "1",
                    PostalCode = "11-111",
                    Street = "street1"
                },
                ShelterDescription = "Shelter Description1"
            };

            var pets = new List<Pet>
            {
                new Pet
                {
                    Id = Guid.NewGuid(),
                    Gender = PetGender.Female,
                    Type = PetType.Cat,
                    BasicHealthInfoId = Guid.NewGuid(),
                    Description = "PetDescription1",
                    CalendarId = Guid.NewGuid(),
                    Status = PetStatus.Adopted,
                    AvaibleForAdoption = false,
                    Image = new byte[] { 0x12, 0x34, 0x56 },
                    ShelterId = shelter.Id
                },
                new Pet
                {
                    Id = Guid.NewGuid(),
                    Gender = PetGender.Female,
                    Type = PetType.Dog,
                    BasicHealthInfoId = Guid.NewGuid(),
                    Description = "PetDescription2",
                    CalendarId = Guid.NewGuid(),
                    Status = PetStatus.AtShelter,
                    AvaibleForAdoption = true,
                    Image = new byte[] { 0x12, 0x34, 0x56 },
                    ShelterId = shelter.Id
                },
                new Pet
                {
                    Id = Guid.NewGuid(),
                    Gender = PetGender.Female,
                    Type = PetType.Dog,
                    BasicHealthInfoId = Guid.NewGuid(),
                    Description = "PetDescription3",
                    CalendarId = Guid.NewGuid(),
                    Status = PetStatus.Adopted,
                    AvaibleForAdoption = false,
                    Image = new byte[] { 0x12, 0x34, 0x56 },
                    ShelterId = shelter.Id
                },
                new Pet
                {
                    Id = Guid.NewGuid(),
                    Gender = PetGender.Female,
                    Type = PetType.Cat,
                    BasicHealthInfoId = Guid.NewGuid(),
                    Description = "PetDescription4",
                    CalendarId = Guid.NewGuid(),
                    Status = PetStatus.TemporaryHouse,
                    AvaibleForAdoption = true,
                    Image = new byte[] { 0x12, 0x34, 0x56 },
                    ShelterId = shelter.Id
                },
                new Pet
                {
                    Id = Guid.NewGuid(),
                    Gender = PetGender.Female,
                    Type = PetType.Cat,
                    BasicHealthInfoId = Guid.NewGuid(),
                    Description = "PetDescription5",
                    CalendarId = Guid.NewGuid(),
                    Status = PetStatus.Adopted,
                    AvaibleForAdoption = false,
                    Image = new byte[] { 0x12, 0x34, 0x56 },
                    ShelterId = shelter.Id
                },
            };

            shelter.ShelterPets = pets;

            _context.Add(shelter);
            _context.SaveChanges();

            // Act
            var repository = new ShelterRepository(_context); // Replace with your actual repository
            var result = await repository.GetAllAdoptedPets(shelter.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count()); // Expecting 3 adopted pets
        }

        //[Test]
        //public async Task DeleteShelterPet_ShouldDeletePetFromShelter()
        //{
        //    // Arrange: Create and add a shelter with pets to the context
        //    var shelter = new Shelter
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "ShelterName1",
        //        CalendarId = Guid.NewGuid(),
        //        ShelterCalendar = new CalendarActivity(),
        //        AddressId = Guid.NewGuid(),
        //        ShelterAddress = new Address
        //        {
        //            City = "city1",
        //            FlatNumber = 1,
        //            HouseNumber = "1",
        //            PostalCode = "11-111",
        //            Street = "street1"
        //        },
        //        ShelterDescription = "Shelter Description1"
        //    };

        //    var pet = new Pet
        //    {
        //        Id = Guid.NewGuid(),
        //        Gender = PetGender.Female,
        //        Type = PetType.Cat,
        //        BasicHealthInfoId = Guid.NewGuid(),
        //        Description = "PetDescription4",
        //        CalendarId = Guid.NewGuid(),
        //        Status = PetStatus.TemporaryHouse,
        //        AvaibleForAdoption = true,
        //        Image = new byte[] { 0x12, 0x34, 0x56 },
        //        ShelterId = shelter.Id
        //    };

        //    shelter.ShelterPets.Add(pet);

        //    _context.Add(shelter);
        //    _context.SaveChanges();

        //    // Act: Call the DeleteShelterPet method
        //    var repository = new ShelterRepository(_context); // Replace with your actual repository
        //    var result = await repository.DeleteShelterPet(shelter.Id, pet.Id);

        //    // Assert: Verify that the pet has been deleted
        //    Assert.That(result, Is.True);

        //    var shelterWithPet = _context.Shelters
        //        .Include(s => s.ShelterPets)
        //        .SingleOrDefault(s => s.Id == shelter.Id);

        //    Assert.That(shelterWithPet, Is.Not.Null);
        //    Assert.That(shelterWithPet.ShelterPets, Is.Empty); // Expecting no pets in the shelter
        //}

        //[Test]
        //public async Task GetShelterAdoptionById_ShelterNotFound_ShouldReturnNull()
        //{
        //    // Arrange: Create a shelter, but don't add it to the context
        //    var shelterId = Guid.NewGuid();
        //    var adoptionId = Guid.NewGuid();

        //    // Act: Call the GetShelterAdoptionById method
        //    var repository = new ShelterRepository(_context); // Replace with your actual repository
        //    var retrievedAdoption = await repository.GetShelterAdoptionById(shelterId, adoptionId);

        //    // Assert: Verify that the retrieved adoption is null
        //    Assert.That(retrievedAdoption, Is.Null);
        //}


        //[Test]
        //public void GetShelterAdoptionById_PassValidAdoption_ShouldReturnAdoption()
        //{
        //    // Arrange: Create a shelter, a pet, a user, and an adoption
        //    var shelterId = Guid.NewGuid();
        //    var shelter = new Shelter
        //    {
        //        Id = shelterId,
        //        Name = "ShelterName",
        //        CalendarId = Guid.NewGuid(),
        //        ShelterCalendar = new CalendarActivity(),
        //        AddressId = Guid.NewGuid(),
        //        ShelterAddress = new Address(),
        //        ShelterDescription = "Shelter Description"
        //    };

        //    var petId = Guid.NewGuid();
        //    var pet = new Pet
        //    {
        //        Id = petId,
        //        Gender = PetGender.Male,
        //        Type = PetType.Dog,
        //        BasicHealthInfoId = Guid.NewGuid(),
        //        Description = "PetDescription",
        //        CalendarId = Guid.NewGuid(),
        //        Status = PetStatus.AtShelter,
        //        AvaibleForAdoption = true,
        //        Image = new byte[] { 0x12, 0x34, 0x56 },
        //        ShelterId = shelter.Id
        //    };

        //    var userId = Guid.NewGuid();
        //    var user = new User
        //    {
        //        Id = userId,
        //        UserName = "SampleUser",
        //        Email = "SampleUser@test.com"
        //    };

        //    var adoptionId = Guid.NewGuid();
        //    var adoption = new Adoption
        //    {
        //        Id = adoptionId,
        //        PetId = pet.Id,
        //        UserId = user.Id,
        //        PreAdoptionPoll = true,
        //        ContractAdoption = true,
        //        Meetings = true,
        //    };

        //    shelter.Adoptions.Add(adoption);
        //    _context.Add(shelter);
        //    _context.SaveChanges();

        //    // Act: Get the adoption by shelter and adoption IDs
        //    var repository = new ShelterRepository(_context); // Replace with your actual repository
        //    var retrievedAdoption = repository.GetShelterAdoptionById(shelter.Id, adoption.Id);

        //    Assert.That(retrievedAdoption, Is.Not.Null);
        //    // Assert: Verify that the retrieved adoption is not null
        //}
    }
}



