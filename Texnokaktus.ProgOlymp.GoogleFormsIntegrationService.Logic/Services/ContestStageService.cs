using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Services.Abstractions;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services;

internal class ContestStageService(IUnitOfWork unitOfWork) : IContestStageService
{
    public async Task CreateContestStage(int contestStageId)
    {
        unitOfWork.ContestStageRepository.Add(contestStageId, false);
        await unitOfWork.SaveChangesAsync();
    }
}
