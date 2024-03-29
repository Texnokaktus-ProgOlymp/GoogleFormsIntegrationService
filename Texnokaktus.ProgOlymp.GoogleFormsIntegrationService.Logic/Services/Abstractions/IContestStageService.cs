using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Models;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services.Abstractions;

public interface IContestStageService
{
    Task CreateContestStage(int contestStageId);
    Task<IEnumerable<ContestStageModel>> GetAvailableContestStagesAsync();
}
