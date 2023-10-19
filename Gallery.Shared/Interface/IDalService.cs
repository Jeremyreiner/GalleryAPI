using Gallery.Shared.Entities;
using Gallery.Shared.Results;

namespace Gallery.Shared.Interface;

public interface IDalService
{
    Task<Result<GitHubData>> GitHubRepositoryQuery(string query);

    Task<IEnumerable<GitHubItem>> GetUserGallery(string name);

    Task UpdateGallery(GitHubItem item, string name);
}