using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PetAdoptionCenter.Controllers;
using SimpleWebDal.Models.WebUser;
using SimpleWebDal.Models.WebUser.Enums;

namespace PetAdoptionCenterTests
{
    [TestFixture]
    public class AdminControllerTests
    {
        private AdminController _adminController;
        private Mock<RoleManager<IdentityRole>> _mockRoleManager;

        [SetUp]
        public void Setup()
        {
            // Mock RoleManager using Moq
            _mockRoleManager = new Mock<RoleManager<IdentityRole>>(
       new Mock<IRoleStore<IdentityRole>>().Object, // Mock lub obiekt IRoleStore<IdentityRole>
       new IRoleValidator<IdentityRole>[] { new RoleValidator<IdentityRole>() }, // Przykład walidatora
       new Mock<ILookupNormalizer>().Object, // Mock lub obiekt ILookupNormalizer
       new IdentityErrorDescriber(),
       new Mock<ILogger<RoleManager<IdentityRole>>>().Object // Mock lub obiekt ILogger
   );


            // Create AdminController with the mock RoleManager
            _adminController = new AdminController(_mockRoleManager.Object, null);
        }

        [Test]
        public async Task CreateRole_WhenRoleDoesNotExist_ReturnsOk()
        {
            // Arrange
            RoleName roleName = RoleName.ShelterOwner; // Choose the appropriate role

            // Set up the RoleManager mock to return false when RoleExistsAsync is called
            _mockRoleManager.Setup(m => m.RoleExistsAsync(roleName.ToString())).ReturnsAsync(false);

            // Act
            var result = await _adminController.CreateRole(roleName.ToString()) as OkResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public async Task CreateRole_WhenRoleExists_ReturnsOk()
        {
            // Arrange
            RoleName roleName = RoleName.Adopter; // Choose the appropriate role

            // Set up the RoleManager mock to return true when RoleExistsAsync is called
            _mockRoleManager.Setup(m => m.RoleExistsAsync(roleName.ToString())).ReturnsAsync(true);

            // Act
            var result = await _adminController.CreateRole(roleName.ToString()) as OkResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }
    }
}