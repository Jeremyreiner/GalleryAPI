using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GalleryAPI.Services
{
    public class JwtAuthenticationService
    {
        private readonly string securityKey;

        public JwtAuthenticationService(IConfiguration config)
        {
            securityKey = config.GetSection("SecurityKey").Value;
        }

        public ClaimsPrincipal? Authenticate(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };

                var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

                if (validatedToken is not JwtSecurityToken jwtSecurityToken)
                {
                    return null;
                }

                // Check if the token is expired.
                return jwtSecurityToken.ValidTo <= DateTime.UtcNow ? null : claimsPrincipal;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}