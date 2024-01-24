namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Models;

public record FormResponsesModel
{
    public FormResponsesModel(IEnumerable<FormResponse> responses)
    {
        Responses = responses as FormResponse[] ?? responses.ToArray();
    }

    public FormResponse[] Responses { get; init; }

    public void Deconstruct(out FormResponse[] responses)
    {
        responses = Responses;
    }
}
