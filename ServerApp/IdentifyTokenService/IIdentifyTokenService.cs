namespace GalleryAPI.IdentifyTokenService;

public interface IIdentifyTokenService
{
    string? GetNameFromToken();
    string GetEmailFromTokenThrow();
}