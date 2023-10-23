using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimpleWebDal.Data;
using SimpleWebDal.Models.WebUser;
using SimpleWebDal.Repository.UserRepo;

namespace PetAdoptionCenterIntegrationTests
{
	public class UserRepositoryTests
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

        //[Test]
        //public async Task GetUserById_ExistingUser_ShouldReturnUser()
        //{
        //    // Arrange:

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
        //    _context.SaveChanges();

        //    // Act:
        //    var repository = new UserRepository(_context);
        //    var result = await repository.GetUserById(user.BasicInformation.Id);

        //    // Assert:
        //    Assert.That(result, Is.Not.Null);
        //    Assert.That(result.Id, Is.EqualTo(user.BasicInformation.Id));
        //}

        [Test]
        public async Task GetUserById_NonExistentUser_ShouldReturnNull()
        {
            // Act:
            var repository = new UserRepository(_context);
            var result = await repository.GetUserById(Guid.NewGuid());

            // Assert:
            Assert.That(result, Is.Null);
        }

    }
}

