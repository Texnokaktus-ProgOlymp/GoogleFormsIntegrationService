using System.Globalization;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Services.Abstractions;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Models;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services.Abstractions;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Exceptions;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Models;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services;

internal class SheetsApplicationDataService(IGoogleSheetsService sheetsService,
                                            IUnitOfWork unitOfWork) : IApplicationDataService
{
    private const int ColumnCount = 18;
    private const int BatchSize = 250;

    public async Task<IEnumerable<ParticipantApplication>> GetParticipantApplicationsAsync(ContestStageModel contestStage)
    {
        var startRowIndex = contestStage.LastRowIndex + 1;

        var valueRange = await sheetsService.GetRangeAsync(contestStage.FormId, $"R{startRowIndex}C1:R{startRowIndex + BatchSize}C{ColumnCount}");

        if (valueRange.Values.Length == 0)
            return Enumerable.Empty<ParticipantApplication>();

        var tuples = valueRange.Values.Zip(Enumerable.Range(startRowIndex, valueRange.Values.Length)).ToArray();

        await unitOfWork.ContestStageRepository.SetLastRowIndex(contestStage.Id, tuples.Max(tuple => tuple.Second));
        await unitOfWork.SaveChangesAsync();
        
        return from item in tuples
               let row = item.First
               where row.Length > 0
               let rowIndex = item.Second
               let timestamp = row.GetDateTime(0) ?? throw new("No value for Timestamp")
               select new ParticipantApplication(rowIndex, timestamp, timestamp, contestStage.Id)
               {
                   AgeCategory = row.GetString(1) ?? throw new NoValueException(nameof(ParticipantApplication.AgeCategory)),
                   ParticipantName = row.GetString(2) ?? throw new NoValueException(nameof(ParticipantApplication.ParticipantName)),
                   ParticipantGrade = row.GetString(5) ?? throw new NoValueException(nameof(ParticipantApplication.ParticipantGrade)),
                   ParticipantEmail = row.GetString(6) ?? throw new NoValueException(nameof(ParticipantApplication.ParticipantEmail)),
                   School = row.GetString(8) ?? throw new NoValueException(nameof(ParticipantApplication.School)),
                   SchoolRegion = row.GetString(9) ?? throw new NoValueException(nameof(ParticipantApplication.SchoolRegion)),
               };
    }

    public async Task SetStatusMessageAsync(ApplicationStatusMessage message, string sheetId, int rowIndex)
    {
        var valueRange = new ValueRange
        {
            MajorDimension = "ROWS",
            Range = $"R{rowIndex}C{ColumnCount + 1}:R{rowIndex}C{ColumnCount + 2}",
            Values =
            [
                [
                    message.YandexIdLogin ?? string.Empty,
                    message.StatusMessage
                ]
            ]
        };
        await sheetsService.UpdateRangeAsync(sheetId, valueRange);
    }
}

file static class RowExtensions
{
    public static string? GetString(this IReadOnlyList<string> row, int index) =>
        row.Count >= index - 1 && !string.IsNullOrWhiteSpace(row[index])
            ? row[index]
            : null;

    public static DateTime? GetDateTime(this IReadOnlyList<string> row, int index) =>
        row.GetString(index) is { } str && DateTime.TryParseExact(str, "dd.MM.yyyy hh:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var dateTime)
            ? dateTime.ToUniversalTime()
            : null;
}
