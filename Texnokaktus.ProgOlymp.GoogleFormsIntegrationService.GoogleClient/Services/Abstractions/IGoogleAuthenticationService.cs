using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Models;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services.Abstractions;

public interface IGoogleAuthenticationService
{
    string GetGoogleOAuthUrl(string localRedirectUri);
    Task<TokenResponse> GetAccessTokenAsync(string code, string localRedirectUrl);
    Task<TokenResponse> RefreshAuthenticationCodeAsync(string refreshToken);
}
