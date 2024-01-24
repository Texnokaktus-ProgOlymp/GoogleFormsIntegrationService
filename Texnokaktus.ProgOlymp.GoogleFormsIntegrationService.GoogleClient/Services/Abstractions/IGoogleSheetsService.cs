using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Models;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services.Abstractions;

public interface IGoogleSheetsService
{
    Task<ValueRange> GetRangeAsync(string sheetId, string range);
    Task UpdateRangeAsync(string sheetId, ValueRange range);
}
