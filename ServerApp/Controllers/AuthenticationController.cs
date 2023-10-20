using Gallery.Shared.Interface;
using Gallery.Shared.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GalleryAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _AuthenticationService;
    private readonly ILogger<AuthenticationController> _Logger;
    public AuthenticationController(IAuthenticationService authenticationService, ILogger<AuthenticationController> logger)
    {
        _AuthenticationService = authenticationService;
        _Logger = logger;
    }

    [AllowAnonymous]
    [HttpGet("Login/{user}")]
    public async Task<Result<string>> Login(string user)
    {
        _Logger.LogInformation($"Name recieved from input: {user}");

        var token = await _AuthenticationService.GenerateToken(user);

        return Result<string>.Success(token);
    }
}