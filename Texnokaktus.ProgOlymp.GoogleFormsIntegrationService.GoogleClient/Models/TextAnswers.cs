namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Models;

public record TextAnswers
{
    public TextAnswers(IEnumerable<TextAnswer> Answers)
    {
        this.Answers = Answers as TextAnswer[] ?? Answers.ToArray();
    }

    public TextAnswer[] Answers { get; init; }

    public void Deconstruct(out IEnumerable<TextAnswer> answers)
    {
        answers = Answers;
    }
}
