using Gallery.Shared.Entities;

namespace Gallery.Shared.Interface;

public interface IGitHubService
{
    Task<GitHubData?> QueryGitHub(string query);
}