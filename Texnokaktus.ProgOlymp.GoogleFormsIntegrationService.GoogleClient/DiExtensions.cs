using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using RestSharp.Authenticators;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient;

public static class DiExtensions
{
    public static IServiceCollection AddGoogleClientServices(this IServiceCollection services) =>
        services.AddScoped<IGoogleAuthenticationService, GoogleAuthenticationService>()
                .AddScoped<ITokenService, TokenService>()
                .AddScoped<IGoogleFormsService, GoogleFormsService>()
                .AddScoped<IAuthenticator>(provider =>
                 {
                     var tokenService = provider.GetRequiredService<ITokenService>();
                     var accessToken = tokenService.GetAccessTokenAsync()
                                                   .GetAwaiter()
                                                   .GetResult()
                                    ?? throw new("No token");
                     return new JwtAuthenticator(accessToken);
                 })
                .AddScoped<IRestClient>(provider =>
                 {
                     var authenticator = provider.GetRequiredService<IAuthenticator>();
                     return new RestClient("https://forms.googleapis.com",
                                           options => options.Authenticator = authenticator);
                 });
}
