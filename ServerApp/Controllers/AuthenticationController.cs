using GalleryAPI.Entities;
using GalleryAPI.Interface;
using Microsoft.AspNetCore.Mvc;
using Publify.Services.IdentifyTokenService;

namespace GalleryAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _AuthenticationService;
    private readonly IIdentifyTokenService _IdentifyTokenService;

    public AuthenticationController(IAuthenticationService authenticationService, IIdentifyTokenService identifyTokenService)
    {
        _AuthenticationService = authenticationService;
        _IdentifyTokenService = identifyTokenService;
    }

    [HttpPost(nameof(Login))]
    public async Task<ActionResult<string>> Login([FromBody] string user)
    {
        var name = _IdentifyTokenService.GetEmailFromToken();

        if (!string.IsNullOrEmpty(name))
            return Ok(name);

        var token = await _AuthenticationService.GenerateToken(user);

        return Ok(token);
    }

    [HttpPost(nameof(LogOut))]
    public async Task<ActionResult> LogOut()
    {
        // TODO: Implement this action to bookmark a repository for the current user.
        return Ok();
    }
}