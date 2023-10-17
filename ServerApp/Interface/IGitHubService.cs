using GalleryAPI.Entities;

namespace GalleryAPI.Interface;

public interface IGitHubService
{
    Task<GitHubData?> QueryGitHub(string query);
}