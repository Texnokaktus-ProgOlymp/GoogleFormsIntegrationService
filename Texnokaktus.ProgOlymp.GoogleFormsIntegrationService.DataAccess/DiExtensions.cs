using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Context;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Repositories.Abstractions;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Services;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess;

public static class DiExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services,
                                                   Action<DbContextOptionsBuilder>? optionsAction) =>
        services.AddDbContext<AppDbContext>(optionsAction)
                .AddScoped<IContestStageRepository, ContestStageRepository>()
                .AddScoped<IUnitOfWork, UnitOfWork>();
}
