namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Models;

public record ParticipantApplication(int RowIndex,
                                     DateTime CreateTime,
                                     DateTime LastSubmittedTime,
                                     int ContestStageId)
{
    public required string AgeCategory { get; init; }
    public required string ParticipantName { get; init; }
    public required DateOnly BirthDate { get; init; }
    // public required string ContestLocation { get; init; }
    public required string ParticipantSnils { get; init; }
    public required string ParticipantGrade { get; init; }
    public required string ParticipantEmail { get; init; }
    public required bool ParticipantEmailConfirm { get; init; }
    public required string School { get; init; }
    public required string SchoolRegion { get; init; }
    public required string ParentName { get; init; }
    public required string? ParentEmail { get; init; }
    public required string? ParentPhone { get; init; }
    public required bool PersonalDataConsent { get; init; }
    public required string? TeacherName { get; init; }
    public required string? TeacherSchool { get; init; }
    public required string? TeacherEmail { get; init; }
    public required string? TeacherPhone { get; init; }
}
