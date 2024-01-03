namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Models;

public record FormResponse(string ResponseId,
                           DateTime CreateTime,
                           DateTime LastSubmittedTime,
                           Dictionary<string, Answer> Answers);
