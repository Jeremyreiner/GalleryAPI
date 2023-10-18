using Gallery.Shared.Entities;
using GalleryAPI.Interface;

namespace GalleryAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        readonly JwtService _JwtService;

        public AuthenticationService(JwtService jwtService)
        {
            _JwtService = jwtService;
        }

        public async Task<string> GenerateToken(string name)
        {
            var token = _JwtService.GenerateSecurityToken(name);

            return token;
        }

        public Task Logout(User user)
        {
            throw new NotImplementedException();
        }
    }
}
