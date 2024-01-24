using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient;

public static class DiExtensions
{
    public static IServiceCollection AddGoogleClientServices(this IServiceCollection services) =>
        services.AddScoped<IGoogleAuthenticationService, GoogleAuthenticationService>()
                .AddScoped<ITokenService, TokenService>()
                .AddScoped<IGoogleFormsService, GoogleFormsService>()
                .AddScoped<IGoogleSheetsService, GoogleSheetsService>()
                .AddKeyedScoped<IRestClient>("OAuth2", (_, _) => new RestClient("https://oauth2.googleapis.com"))
                .AddScoped<IGoogleServiceAsyncFactory, GoogleServiceAsyncFactory>();
}
