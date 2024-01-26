using Google.Apis.Sheets.v4;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Models;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services;

internal class GoogleSheetsService(IGoogleServiceAsyncFactory googleServiceFactory) : IGoogleSheetsService
{
    public async Task<ValueRange> GetRangeAsync(string sheetId, string range)
    {
        using var sheetsService = await googleServiceFactory.GetSheetsServiceAsync();
        var valueRange = await sheetsService.Spreadsheets.Values.Get(sheetId, range).ExecuteAsync();
        return new()
        {
            Range = valueRange.Range,
            MajorDimension = valueRange.MajorDimension,
            Values = valueRange.Values is { } values
                         ? values.Select(list => list.Select(o => o as string ?? o.ToString() ?? string.Empty)
                                                     .ToArray())
                                 .ToArray()
                         : Array.Empty<string[]>()
        };
    }

    public async Task UpdateRangeAsync(string sheetId, ValueRange range)
    {
        using var sheetsService = await googleServiceFactory.GetSheetsServiceAsync();
        var valueRange = new Google.Apis.Sheets.v4.Data.ValueRange
        {
            Range = range.Range,
            MajorDimension = range.MajorDimension,
            Values = range.Values
                          .Select(strings => strings.Select(s => (object)s)
                                                    .ToArray() as IList<object>)
                          .ToList()
        };
        var updateRequest = sheetsService.Spreadsheets.Values.Update(valueRange, sheetId, range.Range);
        updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
        await updateRequest.ExecuteAsync();
    }
}
