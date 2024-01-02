using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Models;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services.Abstractions;

public interface ITokenService
{
    Task RegisterTokenAsync(TokenResponse tokenResponse);
    Task<string?> GetAccessTokenAsync();
}
