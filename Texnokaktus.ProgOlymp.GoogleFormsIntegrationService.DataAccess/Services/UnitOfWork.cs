using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Context;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Repositories.Abstractions;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Services;

// ReSharper disable once SuggestBaseTypeForParameterInConstructor
internal class UnitOfWork(AppDbContext context,
                          IApplicationRepository applicationRepository,
                          IContestStageRepository contestStageRepository) : IUnitOfWork
{
    public IApplicationRepository ApplicationRepository { get; } = applicationRepository;
    public IContestStageRepository ContestStageRepository { get; } = contestStageRepository;
    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}
