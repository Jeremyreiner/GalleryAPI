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
    private readonly ILogger<AuthenticationController> _Logger;

    public AuthenticationController(IAuthenticationService authenticationService, IIdentifyTokenService identifyTokenService, ILogger<AuthenticationController> logger)
    {
        _AuthenticationService = authenticationService;
        _IdentifyTokenService = identifyTokenService;
        _Logger = logger;
    }

    [HttpGet("Login/{user}")]
    public async Task<string> Login(string user)
    {
        var name = _IdentifyTokenService.GetNameFromToken();

        _Logger.LogInformation($"Name recieved from token: {name}");
        _Logger.LogInformation($"Name recieved from input: {user}");

        if (!string.IsNullOrEmpty(name))
            return name;

        var token = await _AuthenticationService.GenerateToken(user);

        return token;
    }
}