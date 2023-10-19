using Carter;
using Gallery.Shared.Entities;
using Gallery.Shared.Interface;
using Gallery.Shared.Results;

namespace GalleryAPI.Controllers;

public class Repository : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/Repositories/");

        group.MapGet("search/{query}", GetQuery)
            .RequireAuthorization();

        group.MapGet("GetUserGallery", GetUserGallery)
            .RequireAuthorization();

        group.MapPost("UpdateGallery", UpdateGallery)
            .RequireAuthorization();
    }

    public static async Task<Result<IEnumerable<GitHubItem>>> GetQuery(
        string query, 
        IDalService dalService)
    {
        var result = await dalService.GitHubRepositoryQuery(query);

        return result.IsSuccess 
            ? Result<IEnumerable<GitHubItem>>.Success(result.Value.items) 
            : Result<IEnumerable<GitHubItem>>.Success(Enumerable.Empty<GitHubItem>());
    }

    public static async Task<Result<IEnumerable<GitHubItem>>> GetUserGallery( 
        IIdentifyTokenService identifyTokenService,
        IDalService dalService,
        ILogger<Repository> logger)
    {
        var name = identifyTokenService.GetNameFromToken();

        var gallery = await dalService.GetUserGallery(name);

        logger.LogInformation($"Gallery of user: {name}");
        
        return Result<IEnumerable<GitHubItem>>.Success(gallery);
    }

    public static async Task UpdateGallery(GitHubItem item,
        IIdentifyTokenService identifyTokenService,
        IDalService dalService) =>
        await dalService.UpdateGallery(item, identifyTokenService.GetNameFromToken());
}