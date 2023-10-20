using Gallery.Shared.Entities;
using Gallery.Shared.Interface;
using Gallery.Shared.Results;
using Microsoft.Extensions.Logging;

namespace Gallery.Shared.Services
{
    public class DalService : IDalService
    {
        private readonly IGitHubService _GitHubService;
        
        private readonly IGalleryRepository _GalleryRepository;
        
        private readonly IUserRepository _UserRepository;
        
        readonly ILogger<DalService> _Logger;


        public DalService(IGitHubService gitHubService, IGalleryRepository galleryRepository, IUserRepository userRepository, ILogger<DalService> logger) 
        {
            _GitHubService = gitHubService;
            _GalleryRepository = galleryRepository;
            _UserRepository = userRepository;
            _Logger = logger;
        }

        public async Task<Result<GitHubData>> GitHubRepositoryQuery(string query)
        {
            var data = await _GitHubService.QueryGitHub(query);

            return data is null 
                ? Result<GitHubData>.Failed(Error.None) 
                : Result<GitHubData>.Success(data);
        }

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
                _Logger.LogInformation($"Adding to gallery: {item.full_name}");
                
                await _GalleryRepository.AddAsync(new GalleryModel
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    FullName = item.full_name,
                    AvatarUrl = item.owner.avatar_url
                });
            }
            else
            {
                _Logger.LogInformation($"Removing from gallery: {item.full_name}");
                
                await _GalleryRepository.DeleteAsync(exists);
            }
        }
    }
}
