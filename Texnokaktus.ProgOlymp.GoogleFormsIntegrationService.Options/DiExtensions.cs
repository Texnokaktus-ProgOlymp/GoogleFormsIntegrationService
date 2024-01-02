using Microsoft.Extensions.DependencyInjection;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Options.Models;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Options;

public static class DiExtensions
{
    public static IServiceCollection AddServiceOptions(this IServiceCollection services)
    {
        services.AddOptions<GoogleAppParameters>().BindConfiguration(nameof(GoogleAppParameters));

        return services;
    }
}
