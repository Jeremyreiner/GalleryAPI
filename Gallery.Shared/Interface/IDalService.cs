using Gallery.Shared.Entities;
using Gallery.Shared.Results;

namespace Gallery.Shared.Interface;

public interface IDalService
{
    Task CreateUser(string name);

    Task<IEnumerable<GitHubItem>> GetUserGallery(string name);

    Task<Result<GitHubData>> GitHubRepositoryQuery(string query);

    Task UpdateGallery(GitHubItem item, string name);
}