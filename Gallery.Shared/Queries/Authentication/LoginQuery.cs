using Gallery.Shared.Entities;
using Gallery.Shared.Interface;

namespace Gallery.Shared.Queries.Authentication
{
    public class LoginQuery
    {
        private readonly IUserRepository _UserRepository;

        public LoginQuery(IUserRepository userRepository)
        {
            _UserRepository = userRepository;
        }

        public async Task Handle(string name)
        {


            var user = await _UserRepository.GetUserByName(u => u.Name == name);

            if (user is not null) return;

            await _UserRepository.CreateUser(new User
            {
                Id = Guid.NewGuid(),
                Name = name
            });
        }
    }
}
