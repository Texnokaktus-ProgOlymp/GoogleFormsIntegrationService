namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Entities;

public record ContestStage
{
    public required int Id { get; init; }
    public string? FormId { get; set; }
}