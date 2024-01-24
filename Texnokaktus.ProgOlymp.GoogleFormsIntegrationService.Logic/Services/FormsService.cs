using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Models;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services.Abstractions;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Models;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services;

internal class FormsService(IGoogleFormsService formsService) : IFormsService
{
    public async Task<IEnumerable<ParticipantApplication>> GetParticipantApplicationsAsync(ContestStageModel contestStage)
    {
        var responsesModel = await formsService.GetResponsesAsync(contestStage.FormId);

        return responsesModel.Responses.Select(response => new ParticipantApplication(response.ResponseId,
                                                        response.CreateTime,
                                                        response.LastSubmittedTime,
                                                        contestStage.Id)
                                               {
                                                   ContestLocation = response.Answers.GetString("4faf6956")
                                                                  ?? throw new($"No value for {nameof(ParticipantApplication.ContestLocation)}"),
                                                   YandexIdLogin = response.Answers.GetString("2a4f01be")
                                                                ?? throw new($"No value for {nameof(ParticipantApplication.YandexIdLogin)}"),
                                                   ParticipantName = response.Answers.GetString("6150340d")
                                                                  ?? throw new($"No value for {nameof(ParticipantApplication.ParticipantName)}"),
                                                   BirthDate = response.Answers.GetDate("3027c6cf")
                                                            ?? throw new($"No value for {nameof(ParticipantApplication.BirthDate)}"),
                                                   ParticipantGrade = response.Answers.GetString("7bf9a8f4")
                                                                   ?? throw new($"No value for {nameof(ParticipantApplication.ParticipantGrade)}"),
                                                   ParticipantEmail = response.Answers.GetString("0bfc42b7")
                                                                   ?? throw new($"No value for {nameof(ParticipantApplication.ParticipantEmail)}"),
                                                   School = response.Answers.GetString("51c46109")
                                                         ?? throw new($"No value for {nameof(ParticipantApplication.School)}"),
                                                   SchoolRegion = response.Answers.GetString("11b6a547")
                                                               ?? throw new($"No value for {nameof(ParticipantApplication.SchoolRegion)}"),
                                                   ParentName = response.Answers.GetString("00331f45")
                                                             ?? throw new($"No value for {nameof(ParticipantApplication.ParentName)}"),
                                                   ParentEmail = response.Answers.GetString("4459e89b")
                                                              ?? throw new($"No value for {nameof(ParticipantApplication.ParentEmail)}"),
                                                   ParentPhone = response.Answers.GetString("43112d1d"),
                                                   PersonalDataConsent = response.Answers.GetBool("72de3724"),
                                                   TeacherName = response.Answers.GetString("2ccafef8"),
                                                   TeacherSchool = response.Answers.GetString("64813242"),
                                                   TeacherEmail = response.Answers.GetString("0d7f0575"),
                                                   TeacherPhone = response.Answers.GetString("28c3ee03")
                                               });
    }
}

file static class DictionaryExtensions
{
    public static string? GetString(this IDictionary<string, Answer> answers, string key) =>
        answers.TryGetValue(key, out var value)
            ? value.TextAnswers.Answers.SingleOrDefault()?.Value
            : null;

    public static DateOnly? GetDate(this IDictionary<string, Answer> answers, string key) =>
        answers.GetString(key) is { } str 
            ? DateOnly.Parse(str)
            : null;

    public static bool GetBool(this IDictionary<string, Answer> answers, string key) =>
        answers.TryGetValue(key, out var value)
     && value.TextAnswers.Answers.Length != 0;
}
