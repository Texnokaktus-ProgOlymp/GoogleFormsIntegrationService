using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Models;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services.Abstractions;

public interface IApplicationService
{
    Task ProcessNewApplicationsAsync(ContestStageModel contestStage);
}
