namespace Publify.Services.IdentifyTokenService;

public interface IIdentifyTokenService
{
    string? GetEmailFromToken();
    string GetEmailFromTokenThrow();
}