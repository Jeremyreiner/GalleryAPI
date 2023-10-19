using Gallery.Shared.Entities;
using Gallery.Shared.Interface;
using Microsoft.AspNetCore.Mvc;
using GalleryAPI.IdentifyTokenService;

namespace GalleryAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RepositoriesController : ControllerBase
{
    private readonly IIdentifyTokenService _IdentifyTokenService;

    private readonly IDalService _DalService;

    public RepositoriesController(IIdentifyTokenService identifyTokenService, IDalService dalService)
    {
        _IdentifyTokenService = identifyTokenService;
        _DalService = dalService;
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

        return await _DalService.GetUserGallery("Jimmy");
    }

    [HttpPost(nameof(UpdateGallery))]
    public async Task UpdateGallery([FromBody] GitHubItem item)
    {
        var name = _IdentifyTokenService.GetNameFromToken();

        await _DalService.UpdateGallery(item, "Jimmy");
    }
}