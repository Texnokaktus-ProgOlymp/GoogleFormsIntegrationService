namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Entities;

public record Application
{
    public required int Id { get; init; }
    public required string ResponseId { get; init; }
    public required int ContestStageId { get; init; }
    public required DateTime Submitted { get; init; }
    public required string YandexIdLogin { get; init; }
}
