using Microsoft.Extensions.Caching.Distributed;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Models;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services;

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

    private void RegisterToken(TokenResponse tokenResponse)
    {
        cache.SetString(AccessTokenKey,
                        tokenResponse.AccessToken,
                        new()
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(tokenResponse.ExpiresIn)
                        });

        if (tokenResponse.RefreshToken is not null)
            cache.SetString(RefreshTokenKey, tokenResponse.RefreshToken);
    }

    public async Task<string?> GetAccessTokenAsync()
    {
        var accessToken = await cache.GetStringAsync(AccessTokenKey);
        if (accessToken is not null) return accessToken;

        var refreshToken = await cache.GetStringAsync(RefreshTokenKey);
        if (refreshToken is null) return null;

        var response = await googleAuthenticationService.RefreshAccessTokenAsync(refreshToken);

        await RegisterTokenAsync(response);
        return response.AccessToken;
    }

    public async Task RevokeTokenAsync()
    {
        var accessToken = await GetAccessTokenAsync() ?? throw new();
        await googleAuthenticationService.RevokeTokenAsync(accessToken);
        await cache.RemoveAsync(AccessTokenKey);
        await cache.RemoveAsync(RefreshTokenKey);
    }
}
