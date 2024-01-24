using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Models;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services;

internal class GoogleSheetsService(IGoogleServiceAsyncFactory googleServiceFactory) : IGoogleSheetsService
{
    public async Task<ValueRange> GetRangeAsync(string sheetId, string range)
    {
        var sheetsService = await googleServiceFactory.GetSheetsServiceAsync();
        var valueRange = await sheetsService.Spreadsheets.Values.Get(sheetId, range).ExecuteAsync();
        return new()
        {
            Range = valueRange.Range,
            MajorDimension = valueRange.MajorDimension,
            Values = valueRange.Values
                               .Select(list => list.Select(o => o as string ?? o.ToString() ?? string.Empty)
                                                   .ToArray())
                               .ToArray()
        };
    }

    public async Task UpdateRangeAsync(string sheetId, ValueRange range)
    {
        var sheetsService = await googleServiceFactory.GetSheetsServiceAsync();
        var valueRange = new Google.Apis.Sheets.v4.Data.ValueRange
        {
            Range = range.Range,
            MajorDimension = range.MajorDimension,
            Values = range.Values
                          .Select(strings => strings.Select(s => (object)s)
                                                    .ToArray() as IList<object>)
                          .ToList()
        };
        await sheetsService.Spreadsheets.Values.Update(valueRange, sheetId, range.Range).ExecuteAsync();
    }
}
