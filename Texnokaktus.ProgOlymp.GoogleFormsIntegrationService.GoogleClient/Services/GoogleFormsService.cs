using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Models;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services;

internal class GoogleFormsService(IGoogleServiceAsyncFactory googleServiceFactory) : IGoogleFormsService
{
    public async Task<FormResponse> GetResponseAsync(string formId, string responseId)
    {
        var formsService = await googleServiceFactory.GetFormsServiceAsync();
        var response = await formsService.Forms.Responses.Get(formId, responseId).ExecuteAsync();
        return response.MapFormResponse();
    }

    public async Task<FormResponsesModel> GetResponsesAsync(string formId)
    {
        var formsService = await googleServiceFactory.GetFormsServiceAsync();
        var response = await formsService.Forms.Responses.List(formId).ExecuteAsync();

        return new(from formResponse in response.Responses
                   select formResponse.MapFormResponse());
    }
}

file static class MappingExtensions
{
    public static FormResponse MapFormResponse(this Google.Apis.Forms.v1.Data.FormResponse formResponse)
    {
        var answers = formResponse.Answers.ToDictionary(pair => pair.Key, pair => pair.Value.MapAnswer());
        return new(formResponse.ResponseId,
                   formResponse.CreateTimeDateTimeOffset!.Value.UtcDateTime,
                   formResponse.LastSubmittedTimeDateTimeOffset!.Value.UtcDateTime,
                   answers);
    }

    private static Answer MapAnswer(this Google.Apis.Forms.v1.Data.Answer answer) =>
        new(answer.QuestionId,
            new(from textAnswer in answer.TextAnswers.Answers
                select new TextAnswer(textAnswer.Value)));
}
