using Gallery.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using GalleryAPI.Interface;

namespace GalleryAPI.Controllers;

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
}