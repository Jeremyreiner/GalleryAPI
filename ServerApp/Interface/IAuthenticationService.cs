using GalleryAPI.Entities;

namespace GalleryAPI.Interface
{
    public interface IAuthenticationService
    {
        Task<string> GenerateToken(string name);

        Task Logout(User user);
    }
}
