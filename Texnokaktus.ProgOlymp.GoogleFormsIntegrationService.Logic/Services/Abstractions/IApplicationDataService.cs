using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Models;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services.Abstractions;

public interface IApplicationDataService
{
    Task<IEnumerable<ParticipantApplication>> GetParticipantApplicationsAsync(ContestStageModel contestStage);
    Task SetStatusMessageAsync(ApplicationStatusMessage message, string sheetId, int rowIndex);
}
