using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SimpleWebDal.Data;
using SimpleWebDal.Models.WebUser;
using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class TokenService : ITokenService
{
    private const int ExpirationMinutes = 30;

    private readonly IConfiguration _configuration;
   

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        
    }

    public (string AccessToken, string RefreshToken) CreateToken(User user, string role)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);

        var token = CreateJwtToken(
            CreateClaims(user, role),
            CreateSigningCredentials(),
            expiration
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        var refreshToken = GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiration = DateTime.UtcNow.AddDays(7);

 

        return (tokenHandler.WriteToken(token), refreshToken);
    }

    private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials, DateTime expiration)
    {
        if (claims == null || !claims.Any()) throw new ArgumentException("Claims are required", nameof(claims));
        if (credentials == null) throw new ArgumentNullException(nameof(credentials));

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

    private List<Claim> CreateClaims(User user, string role)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, "TokenForTheApiWithAuth"),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email)
        };

        if (!string.IsNullOrEmpty(role))
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }

    private SigningCredentials CreateSigningCredentials()
    {
        var jwt = _configuration.GetValue<string>("IssuerSigningKey");
        return new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt)),
            SecurityAlgorithms.HmacSha256
        );
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

  

}