using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Models;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services.Abstractions;

public interface IGoogleFormsService
{
    Task<FormResponse> GetResponseAsync(string formId, string responseId);
    Task<FormResponsesModel> GetResponsesAsync(string formId);
}
