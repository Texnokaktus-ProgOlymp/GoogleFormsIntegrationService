using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Models;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services.Abstractions;

public interface IGoogleFormsService
{
    Task<FormResponsesModel> GetResponses(string formId);
}
