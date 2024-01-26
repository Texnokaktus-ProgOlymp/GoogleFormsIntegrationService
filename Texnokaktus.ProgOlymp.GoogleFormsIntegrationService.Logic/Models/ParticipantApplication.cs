namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Models;

public record ParticipantApplication(int RowIndex,
                                     DateTime CreateTime,
                                     DateTime LastSubmittedTime,
                                     int ContestStageId)
{
    public required string AgeCategory { get; init; }
    public required string ParticipantName { get; init; }
    // public required string ContestLocation { get; init; }
    public required string ParticipantGrade { get; init; }
    public required string ParticipantEmail { get; init; }
    public required string School { get; init; }
    public required string SchoolRegion { get; init; }
}
