using Gallery.Shared.Entities;
using Gallery.Shared.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GalleryAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RepositoriesController : ControllerBase
{
    private readonly IIdentifyTokenService _IdentifyTokenService;

    private readonly IDalService _DalService;

    private readonly ILogger<RepositoriesController> _Logger;

    public RepositoriesController(IIdentifyTokenService identifyTokenService, IDalService dalService, ILogger<RepositoriesController> logger)
    {
        _IdentifyTokenService = identifyTokenService;
        _DalService = dalService;
        _Logger = logger;
    }

    [HttpGet("search/{query}")]
    public async Task<ActionResult<IEnumerable<GitHubItem>>> Get(string query)
    {
        var response = await _DalService.GitHubRepositoryQuery(query);

        return Ok(response);
    }

    [HttpGet(nameof(GetUserGallery))]
    public async Task<IEnumerable<GitHubItem>> GetUserGallery()
    {
        var name = _IdentifyTokenService.GetNameFromToken();

        _Logger.LogInformation($"Name received from token: {name}");

        return await _DalService.GetUserGallery(name);
    }

    [HttpPost(nameof(UpdateGallery))]
    public async Task UpdateGallery([FromBody] GitHubItem item)
    {
        var name = _IdentifyTokenService.GetNameFromToken();

        _Logger.LogInformation($"Name received from token: {name}");

        await _DalService.UpdateGallery(item, name);
    }
}