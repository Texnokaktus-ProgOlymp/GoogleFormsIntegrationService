using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Models;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services.Abstractions;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Models;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services;

internal class FormsApplicationDataService(IGoogleFormsService formsService) : IApplicationDataService
{
    public async Task<IEnumerable<ParticipantApplication>> GetParticipantApplicationsAsync(ContestStageModel contestStage)
    {
        var responsesModel = await formsService.GetResponsesAsync(contestStage.FormId);

        return responsesModel.Responses.Select(response => new ParticipantApplication(0,
                                                        response.CreateTime,
                                                        response.LastSubmittedTime,
                                                        contestStage.Id)
                                               {
                                                   AgeCategory = response.Answers.GetString("5250abc7")
                                                                  ?? throw new($"No value for {nameof(ParticipantApplication.AgeCategory)}"),
                                                   ParticipantName = response.Answers.GetString("6150340d")
                                                                  ?? throw new($"No value for {nameof(ParticipantApplication.ParticipantName)}"),
                                                   ParticipantGrade = response.Answers.GetString("7bf9a8f4")
                                                                   ?? throw new($"No value for {nameof(ParticipantApplication.ParticipantGrade)}"),
                                                   ParticipantEmail = response.Answers.GetString("0bfc42b7")
                                                                   ?? throw new($"No value for {nameof(ParticipantApplication.ParticipantEmail)}"),
                                                   School = response.Answers.GetString("51c46109")
                                                         ?? throw new($"No value for {nameof(ParticipantApplication.School)}"),
                                                   SchoolRegion = response.Answers.GetString("11b6a547")
                                                               ?? throw new($"No value for {nameof(ParticipantApplication.SchoolRegion)}"),
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
