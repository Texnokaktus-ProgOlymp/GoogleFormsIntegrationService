using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RestSharp;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Exceptions;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Models;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services.Abstractions;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Options.Models;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services;

internal class GoogleAuthenticationService(IOptions<GoogleAppParameters> options,
                                           [FromKeyedServices("OAuth2")] IRestClient oauthClient)
    : IGoogleAuthenticationService
{
    private static readonly string Scope = string.Join(' ',
                                                       "https://www.googleapis.com/auth/forms.responses.readonly",
                                                       "https://www.googleapis.com/auth/forms.body.readonly",
                                                       "https://www.googleapis.com/auth/spreadsheets");

    public string GetGoogleOAuthUrl(string localRedirectUri)
    {
        using var restClient = new RestClient("https://accounts.google.com");
        var request = new RestRequest("o/oauth2/v2/auth").AddQueryParameter("client_id", options.Value.ClientId)
                                                         .AddQueryParameter("redirect_uri", localRedirectUri)
                                                         .AddQueryParameter("response_type", "code")
                                                         .AddQueryParameter("scope", Scope)
                                                         .AddQueryParameter("access_type", "offline");

        return restClient.BuildUri(request).ToString();
    }

    public async Task<TokenResponse> GetAccessTokenAsync(string code, string localRedirectUrl)
    {
        var request = new RestRequest("token").AddParameter("client_id", options.Value.ClientId)
                                              .AddParameter("client_secret", options.Value.ClientSecret)
                                              .AddParameter("code", code)
                                              .AddParameter("grant_type", "authorization_code")
                                              .AddParameter("redirect_uri", localRedirectUrl);

        var response = await oauthClient.ExecutePostAsync<TokenResponse>(request);
        
        if (!response.IsSuccessful)
        {
            if (response.ErrorException is not null)
                throw new GoogleAuthenticationException("An error occurred while requesting the access token", response.ErrorException);
            throw new GoogleAuthenticationException("An error occurred while requesting the access token");
        }

        if (response.Data is null)
            throw new GoogleAuthenticationException("Invalid data from OAuth server");

        return response.Data;
    }

    public async Task<TokenResponse> RefreshAccessTokenAsync(string refreshToken)
    {
        var request = new RestRequest("token").AddParameter("client_id", options.Value.ClientId)
                                              .AddParameter("client_secret", options.Value.ClientSecret)
                                              .AddParameter("grant_type", "refresh_token")
                                              .AddParameter("refresh_token", refreshToken);

        var response = await oauthClient.ExecutePostAsync<TokenResponse>(request);

        if (!response.IsSuccessful)
        {
            if (response.ErrorException is not null)
                throw new GoogleAuthenticationException("An error occurred while requesting the access token", response.ErrorException);
            throw new GoogleAuthenticationException("An error occurred while requesting the access token");
        }

        if (response.Data is null)
            throw new GoogleAuthenticationException("Invalid data from OAuth server");

        return response.Data;
    }

    public async Task<TokenResponse> RevokeTokenAsync(string accessToken)
    {
        var request = new RestRequest("revoke").AddParameter("token", accessToken);

        var response = await oauthClient.ExecutePostAsync<TokenResponse>(request);

        if (!response.IsSuccessful)
        {
            if (response.ErrorException is not null)
                throw new GoogleAuthenticationException("An error occurred while requesting the access token", response.ErrorException);
            throw new GoogleAuthenticationException("An error occurred while requesting the access token");
        }

        if (response.Data is null)
            throw new GoogleAuthenticationException("Invalid data from OAuth server");

        return response.Data;
    }
}
