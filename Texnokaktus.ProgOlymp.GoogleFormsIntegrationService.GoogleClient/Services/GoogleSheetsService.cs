using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Models;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services;

internal class GoogleSheetsService([FromKeyedServices("Forms")] IRestClient client) : IGoogleSheetsService
{
    public async Task<ValueRange> GetRange(string sheetId, string range)
    {
        var request = new RestRequest("v4/spreadsheets/{formId}/values/{range}").AddUrlSegment("formId", sheetId)
                                                                                .AddUrlSegment("range", range);

        var response = await client.ExecuteGetAsync<ValueRange>(request);

        if (!response.IsSuccessful)
        {
            if (response.ErrorException is not null)
                throw new("An error occurred while requesting the sheet range", response.ErrorException);
            throw new("An error occurred while requesting the sheet range");
        }

        if (response.Data is null)
            throw new("Invalid data from server");

        return response.Data;
    }
}
