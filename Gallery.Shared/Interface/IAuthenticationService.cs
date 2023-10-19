using Gallery.Shared.Entities;

namespace Gallery.Shared.Interface
{
    public interface IAuthenticationService
    {
        Task<string> GenerateToken(string name);
    }
}
