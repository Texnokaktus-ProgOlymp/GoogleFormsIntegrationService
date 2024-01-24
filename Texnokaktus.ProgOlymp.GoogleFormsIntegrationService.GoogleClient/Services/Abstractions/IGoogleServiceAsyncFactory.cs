using Google.Apis.Forms.v1;
using Google.Apis.Sheets.v4;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services.Abstractions;

public interface IGoogleServiceAsyncFactory
{
    public Task<FormsService> GetFormsServiceAsync();
    public Task<SheetsService> GetSheetsServiceAsync();
}
