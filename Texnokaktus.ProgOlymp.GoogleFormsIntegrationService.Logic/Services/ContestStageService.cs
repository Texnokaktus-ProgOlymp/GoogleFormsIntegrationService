using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Services.Abstractions;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Models;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services;

internal class ContestStageService(IUnitOfWork unitOfWork) : IContestStageService
{
    public async Task CreateContestStage(int contestStageId)
    {
        unitOfWork.ContestStageRepository.Add(contestStageId, false);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<ContestStageModel>> GetAvailableContestStagesAsync()
    {
        var stages = await unitOfWork.ContestStageRepository.GetActiveWithFormIdsAsync();
        return stages.Select(stage => new ContestStageModel(stage.Id, stage.SheetId!, stage.LastRowIndex));
    }
}
