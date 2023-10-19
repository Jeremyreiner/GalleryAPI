using Gallery.Shared.Interface;

namespace GalleryAPI.Services;

public class IdentifyTokenService : IIdentifyTokenService
{
    private readonly IHttpContextAccessor _HttpContextAccessor;

    public IdentifyTokenService(IHttpContextAccessor httpContextAccessor)
    {
        _HttpContextAccessor = httpContextAccessor;
    }

    public string? GetNameFromToken()
    {
        try
        {
            return _HttpContextAccessor?.HttpContext?.User.Identities.First().Claims.First().Value;

        }
        catch
        {
            return string.Empty;
        }
    }
}