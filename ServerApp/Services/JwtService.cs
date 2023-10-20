using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GalleryAPI.Services;

public class JwtService
{
    //https://www.c-sharpcorner.com/article/implement-jwt-in-asp-net-core-3-1/

    readonly string _secret;
    readonly string _expDate;

    public JwtService(IConfiguration config)
    {
        _secret = config.GetSection("JwtConfig").GetSection("secret").Value;
        _expDate = config.GetSection("JwtConfig").GetSection("expirationInMinutes").Value;
    }

    public string? GenerateSecurityToken(string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, email)
            }),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(_expDate)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}