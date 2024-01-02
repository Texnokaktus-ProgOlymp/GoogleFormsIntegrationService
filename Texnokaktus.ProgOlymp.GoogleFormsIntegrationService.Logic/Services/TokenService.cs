using Microsoft.Extensions.Caching.Distributed;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Models;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services.Abstractions;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services;

internal class TokenService(IDistributedCache cache,
                            IGoogleAuthenticationService googleAuthenticationService) : ITokenService
{
    private const string AccessTokenKey = "Auth:Google:AccessToken";
    private const string RefreshTokenKey = "Auth:Google:RefreshToken";

    public async Task RegisterTokenAsync(TokenResponse tokenResponse)
    {
        await cache.SetStringAsync(AccessTokenKey,
                                   tokenResponse.AccessToken,
                                   new DistributedCacheEntryOptions
                                   {
                                       AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(tokenResponse.ExpiresIn)
                                   });

        if (tokenResponse.RefreshToken is not null)
            await cache.SetStringAsync(RefreshTokenKey, tokenResponse.RefreshToken);
    }

    public async Task<string?> GetAccessTokenAsync()
    {
        var accessToken = await cache.GetStringAsync(AccessTokenKey);
        if (accessToken is not null) return accessToken;

        var refreshToken = await cache.GetStringAsync(RefreshTokenKey);
        if (refreshToken is null) return null;

        var response = await googleAuthenticationService.RefreshAuthenticationCodeAsync(refreshToken);

        await RegisterTokenAsync(response);
        return response.AccessToken;
    }
}
