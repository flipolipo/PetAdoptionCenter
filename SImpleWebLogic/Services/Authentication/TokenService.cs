using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SimpleWebDal.Models.WebUser;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class TokenService : ITokenService
{
    private const int ExpirationMinutes = 30;

    private readonly IConfiguration _configuration;
    public TokenService(IConfiguration configuration) 
    {
        _configuration = configuration;
    }

    public string CreateToken(User user, string role)
    {
        var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
        var token = CreateJwtToken(
            CreateClaims(user, role),
            CreateSigningCredentials(),
            expiration
        );
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials,
        DateTime expiration)
    {
        var audience = _configuration.GetValue<string>("ValidAudience");
        var issuer = _configuration.GetValue<string>("ValidIssuer");

        return new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
        );
    }

    private List<Claim> CreateClaims(User user, string? role)
    {
        try
        {
            var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, "TokenForTheApiWithAuth"),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email),
        };

            if (role != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private SigningCredentials CreateSigningCredentials()
    {
        var jwt = _configuration.GetValue<string>("IssuerSigningKey");


        return new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwt)
            ),
            SecurityAlgorithms.HmacSha256
        );
    }
}