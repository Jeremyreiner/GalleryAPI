using Carter;
using Gallery.Shared.Interface;
using Gallery.Shared.Results;
using Microsoft.AspNetCore.Mvc;

namespace GalleryAPI.Controllers;

public class Authentication : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/Authentication/");

        group.MapGet("Login/{user}", Login);
    }

    public static async Task<Result<string>> Login(
        string user, 
        IAuthenticationService authenticationService)
    {
        var token = await authenticationService.GenerateToken(user);

        return Result<string>.Success(token);
    }
}