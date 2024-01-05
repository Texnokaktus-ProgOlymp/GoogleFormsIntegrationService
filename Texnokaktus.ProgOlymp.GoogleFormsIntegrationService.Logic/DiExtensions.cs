using Microsoft.Extensions.DependencyInjection;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic;

public static class DiExtensions
{
    public static IServiceCollection AddLogicServices(this IServiceCollection services) =>
        services.AddScoped<IApplicationService, ApplicationService>()
                .AddScoped<IContestStageService, ContestStageService>()
                .AddScoped<IFormsService, FormsService>()
                .AddScoped<IMessageService, MessageService>();
}
