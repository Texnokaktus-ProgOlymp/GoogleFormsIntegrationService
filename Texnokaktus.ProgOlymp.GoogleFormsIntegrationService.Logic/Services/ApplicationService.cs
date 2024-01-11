using Microsoft.Extensions.Logging;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Models;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Repositories.Abstractions;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Services.Abstractions;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Models;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services;

internal class ApplicationService(IApplicationRepository applicationRepository,
                                  IFormsService formsService,
                                  ILogger<ApplicationService> logger,
                                  IMessageService messageService,
                                  IUnitOfWork unitOfWork) : IApplicationService
{
    public async Task ProcessNewApplicationsAsync(ContestStageModel contestStage)
    {
        var processedCount = 0;
        foreach (var application in await GetNewApplicationsAsync(contestStage))
        {
            await messageService.SendParticipantApplicationAsync(application);
            var model = new ApplicationInsertModel(contestStage.Id,
                                                   application.Id,
                                                   application.CreateTime,
                                                   application.YandexIdLogin);
            unitOfWork.ApplicationRepository.AddApplication(model);
            processedCount++;
        }

        if (processedCount > 0)
        {
            await unitOfWork.SaveChangesAsync();
            logger.LogInformation("Processed {Count} new applications for contest stage {ContestStageId}", processedCount, contestStage.Id);
        }
    }

    private async Task<IEnumerable<ParticipantApplication>> GetNewApplicationsAsync(ContestStageModel contestStage)
    {
        var applications = await formsService.GetParticipantApplicationsAsync(contestStage);
        var responseIds = await applicationRepository.GetResponseIds(contestStage.Id);
        return applications.Where(application => !responseIds.Contains(application.Id));
    }
}
