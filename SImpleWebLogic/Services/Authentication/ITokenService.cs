using Microsoft.AspNetCore.Identity;

public interface ITokenService
{
    string CreateToken(IdentityUser user, string role);
}