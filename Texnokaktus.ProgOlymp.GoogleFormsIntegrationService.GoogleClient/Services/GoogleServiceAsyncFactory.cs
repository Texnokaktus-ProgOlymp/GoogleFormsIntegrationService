using Google.Apis.Auth.OAuth2;
using Google.Apis.Forms.v1;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services;

internal class GoogleServiceAsyncFactory(ITokenService tokenService) : IGoogleServiceAsyncFactory
{
    public async Task<FormsService> GetFormsServiceAsync() => new(await GetInitializerAsync());

    public async Task<SheetsService> GetSheetsServiceAsync() => new(await GetInitializerAsync());

    private async Task<BaseClientService.Initializer> GetInitializerAsync() =>
        new()
        {
            HttpClientInitializer = await GetCredentialAsync()
        };

    private async Task<ICredential> GetCredentialAsync()
    {
        var tokenAsync = await GetTokenAsync();
        return GoogleCredential.FromAccessToken(tokenAsync);
    }

    private async Task<string> GetTokenAsync() => await tokenService.GetAccessTokenAsync()
                                               ?? throw new("No token");
}
