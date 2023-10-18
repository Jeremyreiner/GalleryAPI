using Gallery.DataBase.Repositories;
using Gallery.Shared.Entities;
using Gallery.Shared.Interface;
using GalleryAPI.Interface;
using System.Xml.Linq;

namespace GalleryAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        readonly JwtService _JwtService;

        readonly IUserRepository _userRepository;

        public AuthenticationService(JwtService jwtService, IUserRepository userRepository)
        {
            _JwtService = jwtService;
            _userRepository = userRepository;
        }

        private async Task CreateUser(string name)
        {
            var user = await _userRepository.GetUserByName(u => u.Name == name);

            if (user is not null) return;

            await _userRepository.CreateUser(new User
            {
                Id = Guid.NewGuid(),
                Name = name
            });
        }

        public async Task<string> GenerateToken(string name)
        {
            await CreateUser(name);

            var token = _JwtService.GenerateSecurityToken(name);

            return token ?? string.Empty;
        }

        public Task Logout(User user)
        {
            throw new NotImplementedException();
        }
    }
}
