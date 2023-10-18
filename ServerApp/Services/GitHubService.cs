using Gallery.Shared.Entities;
using GalleryAPI.Interface;

namespace GalleryAPI.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly IHttpClientFactory _HttpClientFactory;

        public GitHubService(IHttpClientFactory httpClientFactory)
        {
            _HttpClientFactory = httpClientFactory;
        }

        public async Task<GitHubData?> QueryGitHub(string query)
        {
            var client = _HttpClientFactory.CreateClient("GitHub");

            return  await client.GetFromJsonAsync<GitHubData>($"search/repositories?q={query}");

        }
    }
}
