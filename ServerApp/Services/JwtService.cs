using Microsoft.AspNetCore.Authentication.JwtBearer;
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
            //Expires = DateTime.UtcNow.AddSeconds(10), // DEBUG
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }


    public string GenerateNewJwtTokenChatGptLol(string currentToken)
    {
        // Define the secret key used to sign the token
        var secretKey = Encoding.ASCII.GetBytes(_secret);

        // Create a token handler
        var tokenHandler = new JwtSecurityTokenHandler();

        // Decode the current token to get the claims
        var decodedToken = tokenHandler.ReadJwtToken(currentToken);
        var claims = decodedToken.Claims;

        // Define the expiration time for the new token
        var expires = DateTime.UtcNow.AddMinutes(30);

        // Define the token descriptor
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expires,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
        };

        // Create the token based on the descriptor
        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

        // Write the token as a string
        var tokenString = tokenHandler.WriteToken(token);

        // Return the new token string
        return tokenString;
    }
}

public static class AuthenticationExtension
{
    public static IServiceCollection AddTokenAuthentication(this IServiceCollection services, IConfiguration config)
    {
        var secret = config.GetSection("JwtConfig").GetSection("secret").Value;

        var key = Encoding.ASCII.GetBytes(secret);
        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });

        return services;
    }
}
