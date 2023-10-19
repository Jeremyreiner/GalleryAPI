using Gallery.Shared.Interface;
using GalleryAPI.IdentifyTokenService;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("Login/{user}")]
    public async Task<string> Login(string user)
    {
        var name = _IdentifyTokenService.GetNameFromToken();

        if (!string.IsNullOrEmpty(name))
            return name;

        var token = await _AuthenticationService.GenerateToken(user);

        return token;
    }
}