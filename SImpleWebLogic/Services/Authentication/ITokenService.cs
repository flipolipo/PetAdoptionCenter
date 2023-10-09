
using SimpleWebDal.Models.WebUser;

public interface ITokenService
{
    (string AccessToken, string RefreshToken) CreateToken(User user, string role);
    string GenerateRefreshToken();
}