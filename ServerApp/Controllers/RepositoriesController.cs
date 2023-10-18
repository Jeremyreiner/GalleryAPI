using Gallery.DataBase.Repositories;
using Gallery.Shared.Entities;
using Gallery.Shared.Interface;
using Microsoft.AspNetCore.Mvc;
using GalleryAPI.Interface;
using GalleryAPI.IdentifyTokenService;

namespace GalleryAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RepositoriesController : ControllerBase
{
    private readonly IIdentifyTokenService _IdentifyTokenService;

    private readonly IGitHubService _GitHubService;
    private readonly IGalleryRepository _GalleryRepository;
    private readonly IUserRepository _UserRepository;

    public RepositoriesController(IGitHubService gitHubService, IGalleryRepository galleryRepository, IIdentifyTokenService identifyTokenService, IUserRepository userRepository)
    {
        _GitHubService = gitHubService;
        _GalleryRepository = galleryRepository;
        _IdentifyTokenService = identifyTokenService;
        _UserRepository = userRepository;
    }

    [HttpGet("search/{query}")]
    public async Task<ActionResult<IEnumerable<GitHubItem>>> Get(string query)
    {   
        var response = await _GitHubService.QueryGitHub(query);
        
        return Ok(response);
    }

    [HttpGet(nameof(GetUserGallery))]
    public async Task<IEnumerable<GitHubItem>> GetUserGallery()
    {
        var name = _IdentifyTokenService.GetNameFromToken();

        var user = await _UserRepository.GetUserByName(u => u.Name == "Jimmy");

        var data = await _GalleryRepository.GetListByAsync(user.Id);

        return data
            .Select(d => new GitHubItem
            {
                full_name = d.FullName, 
                owner = new GitHubOwner
                {
                    avatar_url = d.AvatarUrl,
                }
            }).ToList();
    }

    [HttpPost(nameof(UpdateGallery))]
    public async Task UpdateGallery([FromBody] GitHubItem item)
    {
        var name = _IdentifyTokenService.GetNameFromToken();

        var user = await _UserRepository.GetUserByName(u => u.Name == "Jimmy");

        var exists = await _GalleryRepository.GetItem(g => g.UserId == user.Id && g.FullName == item.full_name);

        if (exists is null)
        {
            await _GalleryRepository.AddAsync(new GalleryModel
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                FullName = item.full_name,
                AvatarUrl = item.owner.avatar_url
            });
        }
        else
            await _GalleryRepository.DeleteAsync(exists);
    }
}