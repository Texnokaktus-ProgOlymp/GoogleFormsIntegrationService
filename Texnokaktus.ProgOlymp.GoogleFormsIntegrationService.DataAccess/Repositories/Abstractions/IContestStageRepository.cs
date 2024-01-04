using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Entities;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Repositories.Abstractions;

public interface IContestStageRepository
{
    Task<ContestStage?> GetAsync(int id);
    Task<IList<ContestStage>> GetAllAsync();
    Task<IList<ContestStage>> GetWithFormIdsAsync();
    void Add(int id);
    void Add(ContestStage contestStage);
    Task SetFormIdAsync(int id, string formId);
}
