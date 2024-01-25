using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Entities;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Repositories.Abstractions;

public interface IContestStageRepository
{
    Task<ContestStage?> GetAsync(int id);
    Task<IList<ContestStage>> GetAllAsync();
    Task<IList<ContestStage>> GetActiveWithFormIdsAsync();
    Task<int?> GetLastRowIndex(int id);
    Task SetLastRowIndex(int id, int lastRowIndex);
    void Add(int id, bool isActive);
    void Add(ContestStage contestStage);
    Task SetSheetIdAsync(int id, string sheetId);
    Task SetActiveAsync(int id, bool isActive);
}
