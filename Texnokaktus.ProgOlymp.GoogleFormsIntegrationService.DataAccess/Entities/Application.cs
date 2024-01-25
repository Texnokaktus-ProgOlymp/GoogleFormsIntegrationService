namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Entities;

public record Application
{
    public int Id { get; init; }
    public required int RowIndex { get; init; }
    public required int ContestStageId { get; init; }
    public required DateTime Submitted { get; init; }
    public required string ParticipantEmail { get; init; }
    public ContestStage ContestStage { get; init; }
}
