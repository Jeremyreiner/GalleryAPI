using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Services;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using GalleryAPI.Entities;
using GalleryAPI.Interface;
using GalleryAPI.Generators;

namespace GalleryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepositoriesController : ControllerBase
    {
        private readonly IGitHubService _GitHubService;

        public RepositoriesController(IGitHubService gitHubService)
        {
            _GitHubService = gitHubService;
        }

        [HttpGet("search/{query}")]
        public async Task<ActionResult<IEnumerable<GitHubItem>>> Get(string query)
        {   
            var response = await _GitHubService.QueryGitHub(query);

            return Ok(response);
        }

        [HttpPost("bookmark/{repositoryId}")]
        public async Task<ActionResult> BookmarkRepository(int repositoryId)
        {
            // TODO: Implement this action to bookmark a repository for the current user.
            return Ok();
        }
    }
}
