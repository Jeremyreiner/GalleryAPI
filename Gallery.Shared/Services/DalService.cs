using Gallery.Shared.Entities;
using Gallery.Shared.Interface;

namespace Gallery.Shared.Services
{
    public class DalService : IDalService
    {
        private readonly IGitHubService _GitHubService;
        
        private readonly IGalleryRepository _GalleryRepository;
        
        private readonly IUserRepository _UserRepository;
        
        public DalService(IGitHubService gitHubService, IGalleryRepository galleryRepository, IUserRepository userRepository)
        {
            _GitHubService = gitHubService;
            _GalleryRepository = galleryRepository;
            _UserRepository = userRepository;
        }

        public async Task<GitHubData?> GitHubRepositoryQuery(string query) =>
            await _GitHubService.QueryGitHub(query);

        public async Task<IEnumerable<GitHubItem>> GetUserGallery(string name)
        {
            var user = await _UserRepository.GetUserByName(u => u.Name == name);

            var data = await _GalleryRepository.GetListByAsync(user.Id);

            return data
                .Select(d => new GitHubItem
                {
                    full_name = d.FullName,
                    owner = new GitHubOwner
                    {
                        avatar_url = d.AvatarUrl,
                    }
                });
        }

        public async Task UpdateGallery(GitHubItem item, string name)
        {
            var user = await _UserRepository.GetUserByName(u => u.Name == name);

            var exists = await _GalleryRepository.GetItem(g => g.UserId == user.Id && g.FullName == item.full_name);

            if (exists is null)
            {
                await _GalleryRepository.AddAsync(new GalleryModel
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    FullName = item.full_name,
                    AvatarUrl = item.owner.avatar_url
                });
            }
            else
                await _GalleryRepository.DeleteAsync(exists);
        }
    }
}
