using Gallery.Shared.Entities;
using Gallery.Shared.Interface;

namespace GalleryAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        readonly JwtService _JwtService;

        readonly IDalService _DalService;

        public AuthenticationService(JwtService jwtService, IDalService dalService)
        {
            _JwtService = jwtService;
            _DalService = dalService;
        }

        public async Task<string> GenerateToken(string name)
        {
            await _DalService.CreateUser(name);

            var token = _JwtService.GenerateSecurityToken(name);

            return token ?? string.Empty;
        }
    }
}
