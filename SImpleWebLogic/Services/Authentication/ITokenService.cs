using Microsoft.AspNetCore.Identity;

public interface ITokenService
{
    public string CreateToken(IdentityUser user);
}