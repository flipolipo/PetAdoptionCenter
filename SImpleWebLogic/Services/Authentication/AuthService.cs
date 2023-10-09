using Microsoft.AspNetCore.Identity;
using SimpleWebDal.Models.WebUser;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IServiceProvider _serviceProvider;

    public AuthService(UserManager<User> userManager, ITokenService tokenService, IServiceProvider serviceProvider)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _serviceProvider = serviceProvider;
    }

    public async Task<AuthResult> RegisterAsync(string email, string username, string password, string role)
    {
        var user = new User { UserName = username, Email = email };
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return FailedRegistration(result, email, username);
        }

        // Assign role to the user
        await _userManager.AddToRoleAsync(user, role);

        // Generate refresh token
        var refreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiration = DateTime.UtcNow.AddDays(7);

        await _userManager.UpdateAsync(user);

        var token = _tokenService.CreateToken(user, role);


        return new AuthResult(true, email, username, token.AccessToken, token.RefreshToken);
    }

    public async Task<AuthResult> LoginAsync(string email, string password)
    {
        var managedUser = await _userManager.FindByEmailAsync(email);

        if (managedUser == null)
        {
            return InvalidEmail(email);
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, password);
        if (!isPasswordValid)
        {
            return InvalidPassword(email, managedUser.UserName);
        }

        var roles = await _userManager.GetRolesAsync(managedUser);

        var token = _tokenService.CreateToken(managedUser, roles.Single());

        
        await _userManager.UpdateAsync(managedUser);

        return new AuthResult(true, managedUser.Email, managedUser.UserName, token.AccessToken, token.RefreshToken);
    }

    public async Task<AuthResult> RefreshToken(string email, string providedRefreshToken)
    {
        var managedUser = await _userManager.FindByEmailAsync(email);

        if (managedUser == null)
        {
            return new AuthResult(false, email, "", "", "");
        }

        if (managedUser.RefreshToken != providedRefreshToken || DateTime.UtcNow > managedUser.RefreshTokenExpiration)
        {
            return new AuthResult(false, email, managedUser.UserName, "", "");
        }

        var roles = await _userManager.GetRolesAsync(managedUser);

        var token = _tokenService.CreateToken(managedUser, roles.Single());

       
        await _userManager.UpdateAsync(managedUser);

        return new AuthResult(true, managedUser.Email, managedUser.UserName, token.AccessToken, managedUser.RefreshToken);
    }

    private static AuthResult FailedRegistration(IdentityResult result, string email, string username)
    {
        var authResult = new AuthResult(false, email, username, "", "");

        foreach (var error in result.Errors)
        {
            authResult.ErrorMessages.Add(error.Code, error.Description);
        }

        return authResult;
    }

    private static AuthResult InvalidEmail(string email)
    {
        var result = new AuthResult(false, email, "", "", "");
        result.ErrorMessages.Add("Bad credentials", "Invalid email");
        return result;
    }

    private static AuthResult InvalidPassword(string email, string userName)
    {
        var result = new AuthResult(false, email, userName, "", "");
        result.ErrorMessages.Add("Bad credentials", "Invalid password");
        return result;
    }
}
