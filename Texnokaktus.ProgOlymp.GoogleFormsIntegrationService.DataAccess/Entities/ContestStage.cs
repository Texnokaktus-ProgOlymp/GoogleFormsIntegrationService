namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Entities;

public record ContestStage
{
    public required int Id { get; init; }
    public required bool IsActive { get; set; }
    public string? SheetId { get; set; }
    public int LastRowIndex { get; set; }
}
