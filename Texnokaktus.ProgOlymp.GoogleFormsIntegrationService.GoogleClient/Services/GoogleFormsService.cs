using RestSharp;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Models;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services;

internal class GoogleFormsService(IRestClient client) : IGoogleFormsService
{
    public async Task<FormResponsesModel> GetResponses(string formId)
    {
        var request = new RestRequest("v1/forms/{formId}/responses").AddUrlSegment("formId", formId);

        var response = await client.ExecuteGetAsync<FormResponsesModel>(request);

        if (!response.IsSuccessful)
        {
            if (response.ErrorException is not null)
                throw new("An error occurred while requesting the form responses", response.ErrorException);
            throw new("An error occurred while requesting the form responses");
        }

        if (response.Data is null)
            throw new("Invalid data from server");

        return response.Data;
    }
}
