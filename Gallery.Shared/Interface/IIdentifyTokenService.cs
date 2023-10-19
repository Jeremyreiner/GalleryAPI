namespace Gallery.Shared.Interface;

public interface IIdentifyTokenService
{
    string? GetNameFromToken();
    string GetEmailFromTokenThrow();
}