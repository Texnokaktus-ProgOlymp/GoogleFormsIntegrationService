using Microsoft.Extensions.Logging;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Models;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Services.Abstractions;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Models;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services;

internal class ApplicationService(IApplicationDataService applicationDataService,
                                  ILogger<ApplicationService> logger,
                                  IMessageService messageService,
                                  IUnitOfWork unitOfWork) : IApplicationService
{
    public async Task ProcessNewApplicationsAsync(ContestStageModel contestStage)
    {
        var processedCount = 0;
        foreach (var application in await applicationDataService.GetParticipantApplicationsAsync(contestStage))
        {
            var model = new ApplicationInsertModel(contestStage.Id,
                                                   application.RowIndex,
                                                   application.CreateTime,
                                                   application.ParticipantEmail);
            var createdApplication = unitOfWork.ApplicationRepository.AddApplication(model);
            await unitOfWork.SaveChangesAsync();
            await messageService.SendParticipantApplicationAsync(createdApplication.Id, application);
            processedCount++;
        }

        if (processedCount > 0)
        {
            logger.LogInformation("Processed {Count} new applications for contest stage {ContestStageId}", processedCount, contestStage.Id);
        }
    }

    public async Task WriteApplicationStatusAsync(int applicationId, ApplicationStatusMessage message)
    {
        var application = await unitOfWork.ApplicationRepository.GetApplication(applicationId);

        if (application is null)
        {
            logger.LogError("The application with id {ApplicationId} is not found", applicationId);
            return;
        }

        var sheetId = application.ContestStage.SheetId;

        if (sheetId is null)
        {
            logger.LogError("The Sheet id for application {ApplicationId} is not stated", applicationId);
            return;
        }

        await applicationDataService.SetStatusMessageAsync(message, sheetId, application.RowIndex);
    }
}
