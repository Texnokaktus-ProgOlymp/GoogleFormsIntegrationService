using Microsoft.Extensions.DependencyInjection;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient;

public static class DiExtensions
{
    public static IServiceCollection AddGoogleClientServices(this IServiceCollection services) =>
        services.AddScoped<IGoogleAuthenticationService, GoogleAuthenticationService>();
}
