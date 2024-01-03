using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Models;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services.Abstractions;

public interface ITokenService
{
    Task RegisterTokenAsync(TokenResponse tokenResponse);
    Task<string?> GetAccessTokenAsync();
    Task RevokeTokenAsync();
}
