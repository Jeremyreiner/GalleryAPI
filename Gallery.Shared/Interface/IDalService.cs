using Gallery.Shared.Entities;

namespace Gallery.Shared.Interface;

public interface IDalService
{
    Task<GitHubData?> GitHubRepositoryQuery(string query);

    Task<IEnumerable<GitHubItem>> GetUserGallery(string name);

    Task UpdateGallery(GitHubItem item, string name);
}