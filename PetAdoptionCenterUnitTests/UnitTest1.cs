using Microsoft.AspNetCore.Mvc;
using Moq;


[TestFixture]
public class AuthControllerTests
{
    private Mock<IAuthService> _mockAuthService;
    private AuthController _controller;

    [SetUp]
    public void SetUp()
    {
        _mockAuthService = new Mock<IAuthService>();
        _controller = new AuthController(_mockAuthService.Object);
    }

    [Test]
    public async Task Register_InvalidModel_ReturnsBadRequest()
    {
        // Arrange
        _controller.ModelState.AddModelError("Email", "Email is required");

        // Act
        var result = await _controller.Register(new RegistrationRequest("test@email.com", "testUsername", "testPassword"));

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
    }

    [Test]
    public async Task Register_ValidModelButAuthServiceFails_ReturnsBadRequestWithErrors()
    {
        // Arrange
        _mockAuthService.Setup(s => s.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new AuthResult(false, "test@email.com", "testUsername", "someToken", "someRefreshToken"));

        // Act
        var result = await _controller.Register(new RegistrationRequest("test@email.com", "testUsername", "testPassword"));

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
       
    }

    [Test]
    public async Task Register_Success_ReturnsCreatedAtActionResult()
    {
        // Arrange
        _mockAuthService.Setup(s => s.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new AuthResult(true, "test@email.com", "testUserName", "someToken", "someRefreshToken"));

        // Act
        var result = await _controller.Register(new RegistrationRequest("test@email.com", "testUsername", "testPassword"));

        // Assert
        Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
    }

    [Test]
    public async Task Authenticate_InvalidModel_ReturnsBadRequest()
    {
        // Arrange
        _controller.ModelState.AddModelError("Email", "Email is required");

        // Act
        var result = await _controller.Authenticate(new AuthRequest("test@email.com", "password123"));

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
    }

    [Test]
    public async Task Authenticate_ValidCredentialsButAuthServiceFails_ReturnsBadRequestWithErrors()
    {
        // Arrange
        _mockAuthService.Setup(s => s.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new AuthResult(false, "test@email.com", "testUserName", "someToken", "someRefreshToken"));

        // Act
        var result = await _controller.Authenticate(new AuthRequest("test@email.com", "password123"));

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
    }

    [Test]
    public async Task Authenticate_ValidCredentials_ReturnsOkResult()
    {
        // Arrange
        _mockAuthService.Setup(s => s.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new AuthResult(true, "test@email.com", "testUserName", "someToken", "someRefreshToken"));

        // Act
        var result = await _controller.Authenticate(new AuthRequest("test@email.com", "password123"));

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
    }

    [Test]
    public async Task RefreshToken_Success_ReturnsOkResult()
    {
        // Arrange
        _mockAuthService.Setup(s => s.RefreshToken(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new AuthResult(true, "test@email.com", "testUserName", "someToken", "someRefreshToken"));

        // Act
        var result = await _controller.RefreshToken(new RefreshTokenRequest { Email = "test@email.com", RefreshToken = "validRefreshToken" });

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
    }

    [Test]
    public async Task RefreshToken_ErrorInAuthService_ThrowsAuthenticationException_ReturnsBadRequest()
    {
        // Arrange
        _mockAuthService.Setup(s => s.RefreshToken(It.IsAny<string>(), It.IsAny<string>()))
            .Throws<AuthenticationException>();

        // Act
        var result = await _controller.RefreshToken(new RefreshTokenRequest { Email = "test@email.com", RefreshToken = "validRefreshToken" });

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
    }
}
