using System.Drawing;
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
using Size = SimpleWebDal.Models.Animal.Enums.Size;

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
                    Street = "street1"
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
            var repository = new ShelterRepository(_context);
            var result = await repository.GetAllAdoptedPets(shelter.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count()); // Expecting 3 adopted pets
        }

        [Test]
        public async Task DeleteShelterPet_ShouldDeletePetFromShelter()
        {
            // Arrange:
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

            var pet = new Pet
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
            };

            shelter.ShelterPets = new List<Pet> { pet };

            _context.Add(shelter);
            _context.SaveChanges();

            // Act:
            var repository = new ShelterRepository(_context);
            var result = await repository.DeleteShelterPet(shelter.Id, pet.Id);

            // Assert:
            Assert.That(result, Is.True);

            var shelterWithPet = _context.Shelters
                .Include(s => s.ShelterPets)
                .SingleOrDefault(s => s.Id == shelter.Id);

            Assert.That(shelterWithPet, Is.Not.Null);
            Assert.That(shelterWithPet.ShelterPets, Is.Empty);
        }

        [Test]
        public async Task GetShelterAdoptionById_ShelterNotFound_ShouldReturnNull()
        {
            // Arrange:

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

            var adoption = new Adoption
            {
                Id = Guid.NewGuid(),
                PetId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                PreAdoptionPoll = true,
                ContractAdoption = true,
                Meetings = true
            };

            // Act:
            var repository = new ShelterRepository(_context);
            var retrievedAdoption = await repository.GetShelterAdoptionById(shelter.Id, adoption.Id);

            // Assert:
            Assert.That(retrievedAdoption, Is.Null);
        }

        [Test]
        public void GetShelterAdoptionById_PassValidAdoption_ShouldReturnAdoption()
        {
            // Arrange:
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

            var adoption = new Adoption
            {
                Id = Guid.NewGuid(),
                PetId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                PreAdoptionPoll = true,
                ContractAdoption = true,
                Meetings = true
            };

            shelter.Adoptions = new List<Adoption> { adoption };
            _context.Add(shelter);
            _context.SaveChanges();

            // Act:
            var repository = new ShelterRepository(_context);
            var retrievedAdoption = repository.GetShelterAdoptionById(shelter.Id, adoption.Id);

            // Assert:
            Assert.That(retrievedAdoption, Is.Not.Null);
        }

        [Test]
        public async Task FindUserById_PassValidUserId_ShouldReturnUser()
        {
            // Arrange:
            var user = new User
            {
                Id = Guid.NewGuid(),
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            // Act:
            var repository = new ShelterRepository(_context);
            var foundUser = await repository.FindUserById(user.Id);

            // Assert:
            Assert.That(foundUser, Is.Not.Null);
        }

        [Test]
        public async Task FindShelterById_PassValidShelterId_ShouldReturnShelter()
        {
            // Arrange:
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

            _context.Shelters.Add(shelter);
            _context.SaveChanges();

            // Act:
            var repository = new ShelterRepository(_context);
            var foundShelter = await repository.FindShelter(shelter.Id);

            // Assert:
            Assert.That(foundShelter, Is.Not.Null);
        }

        //[Test]
        //public async Task AddShelterUser_PassValidData_ShouldAddUserToShelter()
        //{
        //    // Arrange:
        //    var shelter = new Shelter
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "OriginalName",
        //        CalendarId = Guid.NewGuid(),
        //        ShelterCalendar = new CalendarActivity(),
        //        AddressId = Guid.NewGuid(),
        //        ShelterAddress = new Address
        //        {
        //            Street = "OriginalStreet",
        //            HouseNumber = "OriginalHouseNumber",
        //            PostalCode = "OriginalPostalCode",
        //            City = "OriginalCity"
        //        },
        //        ShelterDescription = "Original Description"
        //    };

        //    var roleName = RoleName.ShelterWorker;

        //    _context.Shelters.Add(shelter);

        //    var user = new User
        //    {
        //        BasicInformation = new BasicInformation
        //        {
        //            Id = Guid.NewGuid(),
        //            Name = "name1",
        //            Surname = "surname1",
        //            Phone = "12345",
        //            Address = new Address
        //            {
        //                Id = Guid.NewGuid(),
        //                Street = "street1",
        //                HouseNumber = "1",
        //                PostalCode = "11-111",
        //                City = "city1"
        //            }
        //        }
        //    };
        //    _context.Users.Add(user);

        //    // Act:
        //    var repository = new ShelterRepository(_context);
        //    var result = await repository.AddShelterUser(shelter.Id, user.BasicInformation.Id, roleName);

        //    // Assert:
        //    Assert.That(result, Is.True);

        //    var foundShelter = _context.Shelters.Find(shelter.Id);
        //    var foundUser = _context.Users.Find(user.Id);
        //    Assert.Multiple(() =>
        //    {
        //        Assert.That(foundShelter, Is.Not.Null);
        //        Assert.That(foundUser, Is.Not.Null);
        //    });
        //    Assert.Multiple(() =>
        //    {
        //        Assert.That(foundUser.Roles.Any(role => role.Title == roleName), Is.True);
        //        Assert.That(foundShelter.ShelterUsers.Contains(foundUser), Is.True);
        //    });
        //}


        //[Test]
        //public async Task AddPet_PassValidData_ShouldAddPetToShelter()
        //{
        //    // Arrange:
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

        //    _context.Shelters.Add(shelter);
        //    _context.SaveChanges();

        //    var pet = new Pet
        //    {
        //        Id = Guid.NewGuid(),
        //        Gender = PetGender.Male,
        //        Type = PetType.Dog,
        //        BasicHealthInfoId = Guid.NewGuid(),
        //        Description = "PetDescription1",
        //        CalendarId = Guid.NewGuid(),
        //        Status = PetStatus.AtShelter,
        //        AvaibleForAdoption = true,
        //        Image = new byte[] { 0x12, 0x34, 0x56 }
        //    };

        //    // Act:
        //    var repository = new ShelterRepository(_context);
        //    var addedPet = await repository.AddPet(shelter.Id, pet);

        //    // Assert:
        //    var foundShelt = _context.Shelters.Find(shelter.Id);
        //    Assert.Multiple(() =>
        //    {
        //        Assert.That(addedPet, Is.Not.Null);
        //        Assert.That(foundShelt.ShelterPets.Contains(addedPet), Is.True);
        //    });
        //}

        [Test]
        public async Task AddBasicHealthInfoToAPet_PassValidData_ShouldAddHealthInfoToPet()
        {
            // Arrange:
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

            var pet = new Pet
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
            };

            _context.Shelters.Add(shelter);
            _context.Pets.Add(pet);
            _context.SaveChanges();

            var name = "Pet Health Info";
            var age = 3;
            var size = Size.Medium;
            var isNeutered = true;

            // Act:
            var repository = new ShelterRepository(_context);
            var result = await repository.AddBasicHelathInfoToAPet(shelter.Id, pet.Id, name, age, size, isNeutered);

            // Assert:
            Assert.That(result, Is.Not.Null);

            var foundShelter = _context.Shelters.Find(shelter.Id);
            var foundPet = foundShelter.ShelterPets.FirstOrDefault(p => p.Id == pet.Id);
            Assert.That(foundPet.BasicHealthInfo, Is.EqualTo(result));
            Assert.Multiple(() =>
            {
                Assert.That(result.Name, Is.EqualTo(name));
                Assert.That(result.Age, Is.EqualTo(age));
                Assert.That(result.Size, Is.EqualTo(size));
                Assert.That(result.IsNeutered, Is.EqualTo(isNeutered));
            });
        }

        //[Test]
        //public async Task AddVaccinationToAPet_PassValidData_ShouldAddVaccinationToPet()
        //{
        //    // Arrange:
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
        //        ShelterId = shelter.Id,
        //        BasicHealthInfo = new BasicHealthInfo
        //        {
        //                Id = Guid.NewGuid(),
        //                Name = "Pet Health Info",
        //                Age = 3,
        //                Size = Size.Medium,
        //                IsNeutered = true
        //        },
        //        Description = "PetDescription4",
        //        CalendarId = Guid.NewGuid(),
        //        Status = PetStatus.TemporaryHouse,
        //        AvaibleForAdoption = true,
        //        Image = new byte[] { 0x12, 0x34, 0x56 },
        //    };

        //    _context.Shelters.Add(shelter);
        //    _context.Pets.Add(pet);
        //    _context.SaveChanges();

        //    var vaccName = "Vaccination Name";
        //    var date = DateTime.UtcNow;

        //    var repository = new ShelterRepository(_context);
        //    var result = await repository.AddVaccinationToAPet(shelter.Id, pet.Id, vaccName, date);

        //    // Assert:
        //    Assert.That(result, Is.Not.Null);

        //    var foundShelter = _context.Shelters.Find(shelter.Id);
        //    var foundPet = foundShelter.ShelterPets.FirstOrDefault(p => p.Id == pet.Id);

        //    Assert.That(foundPet, Is.Not.Null);
        //    Assert.That(foundPet.BasicHealthInfo.Vaccinations, Has.Member(result));
        //    Assert.Multiple(() =>
        //    {
        //        Assert.That(result.VaccinationName, Is.EqualTo(vaccName));
        //        Assert.That(result.Date, Is.EqualTo(date).Within(1).Seconds);
        //    });
        //}

        [Test]
        public async Task DeleteShelter_ShelterExists_ShouldDeleteShelter()
        {
            // Arrange:
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

            _context.Shelters.Add(shelter);
            _context.SaveChanges();

            // Act:
            var repository = new ShelterRepository(_context);
            var result = await repository.DeleteShelter(shelter.Id);

            // Assert:
            Assert.That(result, Is.True);

            var deletedShelter = _context.Shelters.Find(shelter.Id);
            Assert.That(deletedShelter, Is.Null);
        }

        [Test]
        public async Task DeleteShelter_ShelterNotExists_ShouldReturnFalse()
        {
            // Arrange:
            var shelterId = Guid.NewGuid();

            // Act:
            var repository = new ShelterRepository(_context);
            var result = await repository.DeleteShelter(shelterId);

            // Assert:
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task UpdateShelter_ShelterExists_ShouldUpdateShelter()
        {
            // Arrange:
            var shelter = new Shelter
            {
                Id = Guid.NewGuid(),
                Name = "OriginalName",
                CalendarId = Guid.NewGuid(),
                ShelterCalendar = new CalendarActivity(),
                AddressId = Guid.NewGuid(),
                ShelterAddress = new Address
                {
                    Street = "OriginalStreet",
                    HouseNumber = "OriginalHouseNumber",
                    PostalCode = "OriginalPostalCode",
                    City = "OriginalCity"
                },
                ShelterDescription = "Original Description"
            };

            _context.Shelters.Add(shelter);
            _context.SaveChanges();

            // Act:
            var repository = new ShelterRepository(_context);
            var result = await repository.UpdateShelter(shelter.Id, "NewName", "NewDescription", "NewStreet", "NewHouseNumber", "NewPostalCode", "NewCity");

            // Assert:
            Assert.That(result, Is.True);

            var updatedShelter = _context.Shelters.Find(shelter.Id);
            Assert.Multiple(() =>
            {
                Assert.That(updatedShelter.Name, Is.EqualTo("NewName"));
                Assert.That(updatedShelter.ShelterDescription, Is.EqualTo("NewDescription"));
                Assert.That(updatedShelter.ShelterAddress.Street, Is.EqualTo("NewStreet"));
                Assert.That(updatedShelter.ShelterAddress.HouseNumber, Is.EqualTo("NewHouseNumber"));
                Assert.That(updatedShelter.ShelterAddress.PostalCode, Is.EqualTo("NewPostalCode"));
                Assert.That(updatedShelter.ShelterAddress.City, Is.EqualTo("NewCity"));
            });
        }

        [Test]
        public async Task UpdateShelter_ShelterNotExists_ShouldReturnFalse()
        {
            // Arrange:
            var shelterId = Guid.NewGuid();

            // Act:
            var repository = new ShelterRepository(_context);
            var result = await repository.UpdateShelter(shelterId, "NewName", "NewDescription", "NewStreet", "NewHouseNumber", "NewPostalCode", "NewCity");

            // Assert:
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetShelterUsers_ShelterExists_ShouldReturnUsers()
        {
            // Arrange:
            var shelter = new Shelter
            {
                Id = Guid.NewGuid(),
                Name = "OriginalName",
                CalendarId = Guid.NewGuid(),
                ShelterCalendar = new CalendarActivity(),
                AddressId = Guid.NewGuid(),
                ShelterAddress = new Address
                {
                    Street = "OriginalStreet",
                    HouseNumber = "OriginalHouseNumber",
                    PostalCode = "OriginalPostalCode",
                    City = "OriginalCity"
                },
                ShelterDescription = "Original Description"
            };

            var users = new List<User>
            {
                new User
                {
                    BasicInformation = new BasicInformation
                        {
                            Id = Guid.NewGuid(),
                            Name = "name1",
                            Surname = "surname1",
                            Phone = "12345",
                            Address = new Address
                            {
                                Id = Guid.NewGuid(),
                                Street = "street1",
                                HouseNumber = "1",
                                PostalCode = "11-111",
                                City = "city1"
                            }
                        }
                },
                new User
                {
                    BasicInformation = new BasicInformation
                        {
                            Id = Guid.NewGuid(),
                            Name = "name2",
                            Surname = "surname2",
                            Phone = "12345",
                            Address = new Address
                            {
                                Id = Guid.NewGuid(),
                                Street = "street2",
                                HouseNumber = "2",
                                PostalCode = "11-222",
                                City = "city2"
                            }
                        }
                }
            };

            shelter.ShelterUsers = users;

            _context.Shelters.Add(shelter);
            _context.SaveChanges();

            // Act:
            var repository = new ShelterRepository(_context);
            var retrievedUsers = await repository.GetShelterUsers(shelter.Id);

            // Assert:
            Assert.That(retrievedUsers, Is.Not.Null);
            Assert.That(retrievedUsers, Is.EquivalentTo(users));
        }

        [Test]
        public async Task GetShelterUsers_ShelterNotExists_ShouldReturnEmptyList()
        {
            // Arrange:
            var shelterId = Guid.NewGuid();

            // Act:
            var repository = new ShelterRepository(_context);
            var retrievedUsers = await repository.GetShelterUsers(shelterId);

            // Assert:
            Assert.That(retrievedUsers, Is.Empty);
        }

        [Test]
        public async Task DeleteActivity_PassValidData_ShouldDeleteActivity()
        {
            // Arrange:
            var calendarActivityId = Guid.NewGuid();
            var shelter = new Shelter
            {
                Id = Guid.NewGuid(),
                Name = "OriginalName",
                ShelterCalendar = new CalendarActivity
                {
                    Id = calendarActivityId,
                    Activities = new List<Activity>
                    {
                        new Activity
                        {
                            Id = Guid.NewGuid(),
                            Name = "ActivityName",
                            ActivityDate = new DateTimeOffset(),
                            CalendarActivityId = calendarActivityId
                        },
                    }

                },
                AddressId = Guid.NewGuid(),
                ShelterAddress = new Address
                {
                    Street = "OriginalStreet",
                    HouseNumber = "OriginalHouseNumber",
                    PostalCode = "OriginalPostalCode",
                    City = "OriginalCity"
                },
                ShelterDescription = "Original Description"
            };

            _context.Shelters.Add(shelter);
            _context.SaveChanges();

            var activityId = shelter.ShelterCalendar.Activities.First().Id;

            // Act:
            var repository = new ShelterRepository(_context);
            var result = await repository.DeleteActivity(shelter.Id, activityId);

            // Assert:
            Assert.That(result, Is.True);

            var foundShelter = _context.Shelters.Find(shelter.Id);
            Assert.Multiple(() =>
            {
                Assert.That(foundShelter, Is.Not.Null);
                Assert.That(foundShelter.ShelterCalendar.Activities.Any(a => a.Id == activityId), Is.False);
            });
        }

        [Test]
        public async Task DeleteShelterPet_PetExists_ShouldDeletePet()
        {
            // Arrange:
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

            var pet = new Pet
            {
                Id = Guid.NewGuid(),
                Gender = PetGender.Female,
                Type = PetType.Cat,
                ShelterId = shelter.Id,
                BasicHealthInfo = new BasicHealthInfo
                {
                    Id = Guid.NewGuid(),
                    Name = "Pet Health Info",
                    Age = 3,
                    Size = Size.Medium,
                    IsNeutered = true
                },
                Description = "PetDescription4",
                CalendarId = Guid.NewGuid(),
                Status = PetStatus.TemporaryHouse,
                AvaibleForAdoption = true,
                Image = new byte[] { 0x12, 0x34, 0x56 },
            };

            _context.Shelters.Add(shelter);
            _context.Pets.Add(pet);
            _context.SaveChanges();

            // Act:
            var repository = new ShelterRepository(_context);
            var result = await repository.DeleteShelterPet(shelter.Id, pet.Id);

            // Assert:
            Assert.That(result, Is.True);

            var foundShelter = _context.Shelters.Find(shelter.Id);
            var foundPet = _context.Pets.Find(pet.Id);

            Assert.Multiple(() =>
            {
                Assert.That(foundPet, Is.Null);
                Assert.That(foundShelter.ShelterPets, Has.None.EqualTo(pet));
            });
        }

        [Test]
        public async Task GetShelterPetById_PetExists_ShouldReturnPet()
        {
            // Arrange:
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

            var pet = new Pet
            {
                Id = Guid.NewGuid(),
                Gender = PetGender.Female,
                Type = PetType.Cat,
                ShelterId = shelter.Id,
                BasicHealthInfo = new BasicHealthInfo
                {
                    Id = Guid.NewGuid(),
                    Name = "Pet Health Info",
                    Age = 3,
                    Size = Size.Medium,
                    IsNeutered = true
                },
                Description = "PetDescription4",
                CalendarId = Guid.NewGuid(),
                Status = PetStatus.TemporaryHouse,
                AvaibleForAdoption = true,
                Image = new byte[] { 0x12, 0x34, 0x56 },
            };

            _context.Shelters.Add(shelter);
            _context.Pets.Add(pet);
            _context.SaveChanges();

            // Act:
            var repository = new ShelterRepository(_context);
            var retrievedPet = await repository.GetShelterPetById(shelter.Id, pet.Id);

            // Assert:
            Assert.Multiple(() =>
            {
                Assert.That(retrievedPet, Is.Not.Null);
                Assert.That(retrievedPet.Id, Is.EqualTo(pet.Id));
            });
        }

        [Test]
        public async Task UpdateShelterPet_PetExists_ShouldUpdatePet()
        {
            // Arrange:
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

            var pet = new Pet
            {
                Id = Guid.NewGuid(),
                Gender = PetGender.Male,
                Type = PetType.Dog,
                ShelterId = shelter.Id,
                BasicHealthInfo = new BasicHealthInfo
                {
                    Id = Guid.NewGuid(),
                    Name = "Pet Health Info",
                    Age = 2,
                    Size = Size.Small,
                    IsNeutered = true
                },
                Description = "OriginalDescription",
                CalendarId = Guid.NewGuid(),
                Status = PetStatus.TemporaryHouse,
                AvaibleForAdoption = true,
                Image = new byte[] { 0x12, 0x34, 0x56 },
            };

            _context.Shelters.Add(shelter);
            _context.Pets.Add(pet);
            _context.SaveChanges();

            var updatedGender = PetGender.Female;
            var updatedType = PetType.Cat;
            var updatedDescription = "UpdatedDescription";
            var updatedStatus = PetStatus.Adopted;
            var updatedAvaibleForAdoption = false;

            // Act:
            var repository = new ShelterRepository(_context);
            var result = await repository.UpdateShelterPet(shelter.Id, pet.Id, updatedGender, updatedType, updatedDescription, updatedStatus, updatedAvaibleForAdoption);

            // Assert:
            Assert.That(result, Is.True);

            var foundShelter = _context.Shelters.Find(shelter.Id);
            var foundPet = foundShelter.ShelterPets.FirstOrDefault(p => p.Id == pet.Id);

            Assert.Multiple(() =>
            {
                Assert.That(foundPet, Is.Not.Null);
                Assert.That(foundPet.Gender, Is.EqualTo(updatedGender));
                Assert.That(foundPet.Type, Is.EqualTo(updatedType));
                Assert.That(foundPet.Description, Is.EqualTo(updatedDescription));
                Assert.That(foundPet.Status, Is.EqualTo(updatedStatus));
                Assert.That(foundPet.AvaibleForAdoption, Is.EqualTo(updatedAvaibleForAdoption));
            });
        }

        //[Test]
        //public async Task AddTempHouse_PassValidData_ShouldAddTempHouse()
        //{
        //    // Arrange:
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

        //    var user = new User
        //    {
        //        Id = Guid.NewGuid(),
        //        BasicInformation = new BasicInformation
        //        {
        //            Id = Guid.NewGuid(),
        //            Name = "UserName",
        //            Surname = "UserSurname",
        //            Phone = "12345",
        //            AddressId = Guid.NewGuid(),
        //            Address = new Address
        //            {
        //                Id = Guid.NewGuid(),
        //                Street = "UserStreet",
        //                HouseNumber = "UserHouseNumber",
        //                PostalCode = "UserPostalCode",
        //                City = "UserCity"
        //            }
        //        }
        //    };

        //    var pet = new Pet
        //    {
        //        Id = Guid.NewGuid(),
        //        Gender = PetGender.Female,
        //        Type = PetType.Cat,
        //        ShelterId = shelter.Id,
        //        BasicHealthInfo = new BasicHealthInfo
        //        {
        //            Id = Guid.NewGuid(),
        //            Name = "Pet Health Info",
        //            Age = 3,
        //            Size = Size.Medium,
        //            IsNeutered = true
        //        },
        //        Description = "PetDescription4",
        //        CalendarId = Guid.NewGuid(),
        //        Status = PetStatus.TemporaryHouse,
        //        AvaibleForAdoption = true,
        //        Image = new byte[] { 0x12, 0x34, 0x56 },
        //    };

        //    _context.Shelters.Add(shelter);
        //    _context.Users.Add(user);
        //    _context.Pets.Add(pet);
        //    _context.SaveChanges();

        //    var tempHouse = new TempHouse
        //    {
        //        Id = Guid.NewGuid(),
        //        TemporaryOwner = user,
        //        TemporaryHouseAddress = user.BasicInformation.Address,
        //        PetsInTemporaryHouse = new List<Pet> { pet }
        //    };

        //    // Act:
        //    var repository = new ShelterRepository(_context);
        //    var result = await repository.AddTempHouse(shelter.Id, user.Id, pet.Id, tempHouse);

        //    // Assert:
        //    Assert.That(result, Is.Not.Null);
        //}

        //[Test]
        //public async Task AddTempHouse_MissingEntities_ShouldNotAddTempHouse()
        //{
        //    // Arrange:

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

        //    var user = new User
        //    {
        //        Id = Guid.NewGuid(),
        //        BasicInformation = new BasicInformation
        //        {
        //            Id = Guid.NewGuid(),
        //            Name = "UserName",
        //            Surname = "UserSurname",
        //            Phone = "12345",
        //            AddressId = Guid.NewGuid(),
        //            Address = new Address
        //            {
        //                Id = Guid.NewGuid(),
        //                Street = "UserStreet",
        //                HouseNumber = "UserHouseNumber",
        //                PostalCode = "UserPostalCode",
        //                City = "UserCity"
        //            }
        //        }
        //    };

        //    var pet = new Pet
        //    {
        //        Id = Guid.NewGuid(),
        //        Gender = PetGender.Female,
        //        Type = PetType.Cat,
        //        ShelterId = shelter.Id,
        //        BasicHealthInfo = new BasicHealthInfo
        //        {
        //            Id = Guid.NewGuid(),
        //            Name = "Pet Health Info",
        //            Age = 3,
        //            Size = Size.Medium,
        //            IsNeutered = true
        //        },
        //        Description = "PetDescription4",
        //        CalendarId = Guid.NewGuid(),
        //        Status = PetStatus.TemporaryHouse,
        //        AvaibleForAdoption = true,
        //        Image = new byte[] { 0x12, 0x34, 0x56 },
        //    };
        //    var tempHouse = new TempHouse
        //    {
        //        Id = Guid.NewGuid(),
        //        TemporaryOwner = user,
        //        TemporaryHouseAddress = user.BasicInformation.Address,
        //        PetsInTemporaryHouse = new List<Pet> { pet }
        //    };

        //    // Act:
        //    var repository = new ShelterRepository(_context);
        //    var result = await repository.AddTempHouse(shelter.Id, Guid.NewGuid(), Guid.NewGuid(), tempHouse);

        //    // Assert:
        //    Assert.That(result, Is.Not.Null);
        //    Assert.That(result, Is.False);

        //    var foundTempHouse = _context.TempHouses.Find(tempHouse.Id);
        //    Assert.That(foundTempHouse, Is.Null);
        //}

        [Test]
        public async Task GetAllShelterAdoptions_ShouldReturnAdoptionsForShelter()
        {
            // Arrange:
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

            var adoption1 = new Adoption
            {
                Id = Guid.NewGuid(),
                PetId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                PreAdoptionPoll = true,
                ContractAdoption = true,
                Meetings = true
            };

            var adoption2 = new Adoption
            {
                Id = Guid.NewGuid(),
                PetId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                PreAdoptionPoll = true,
                ContractAdoption = true,
                Meetings = true
            };

            _context.Shelters.Add(shelter);
            _context.Adoptions.Add(adoption1);
            _context.Adoptions.Add(adoption2);
            _context.SaveChanges();

            // Act:
            var repository = new ShelterRepository(_context);
            var adoptions = await repository.GetAllShelterAdoptions(shelter.Id);

            // Assert:
            Assert.That(adoptions, Is.Not.Null);
            //Assert.That(adoptions.All(adoption => adoption.ShelterId == shelter.Id), Is.True);
        }

        //[Test]
        //public async Task DeleteTempHouse_ExistingTempHouse_ShouldDeleteTempHouse()
        //{
        //    // Arrange:
        //    var shelter = new Shelter
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "ShelterName1",
        //    };

        //    var tempHouse = new TempHouse
        //    {
        //        Id = Guid.NewGuid(),
        //        ShelterId = shelter.Id,
        //    };

        //    _context.Shelters.Add(shelter);
        //    _context.TempHouses.Add(tempHouse);
        //    _context.SaveChanges();

        //    // Act:
        //    var repository = new ShelterRepository(_context);
        //    var result = await repository.DeleteTempHouse(tempHouse.Id, shelter.Id);

        //    // Assert:
        //    Assert.That(result, Is.True);

        //    var foundShelter = _context.Shelters.Find(shelter.Id);
        //    var foundTempHouse = _context.TempHouses.Find(tempHouse.Id);
        //    Assert.Multiple(() =>
        //    {
        //        Assert.That(foundShelter.TempHouses, Has.No.Member(foundTempHouse));
        //        Assert.That(foundTempHouse, Is.Null);
        //    });
        //}

        //[Test]
        //public async Task DeleteTempHouse_NonExistentTempHouse_ShouldNotDeleteTempHouse()
        //{
        //    // Arrange:
        //    var shelter = new Shelter
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "ShelterName1",
        //    };

        //    _context.Shelters.Add(shelter);
        //    _context.SaveChanges();

        //    // Act:
        //    var repository = new ShelterRepository(_context);
        //    var result = await repository.DeleteTempHouse(Guid.NewGuid(), shelter.Id);

        //    // Assert:
        //    Assert.That(result, Is.False);
        //}

    }
}



