using Gallery.Shared.Interface;

namespace GalleryAPI.IdentifyTokenService;

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
            var claim = _HttpContextAccessor?.HttpContext?.User.Identities.First();//.Claims.First().Value;

            return claim.Claims.First().Value;
        }
        catch
        {
            return string.Empty;
        }
    }

    public string GetEmailFromTokenThrow()
    {
        var email= GetNameFromToken();

        return email ?? throw new ArgumentException("User is not authenticated");
    }
}