using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Repositories.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Services.Abstractions;

public interface IUnitOfWork
{
    IContestStageRepository ContestStageRepository { get; }
    Task SaveChangesAsync();
}
