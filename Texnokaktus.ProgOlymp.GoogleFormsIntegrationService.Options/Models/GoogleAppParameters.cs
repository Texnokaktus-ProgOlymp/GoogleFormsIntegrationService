namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Options.Models;

public record GoogleAppParameters
{
    public required string ClientId { get; init; }
    public required string ClientSecret { get; init; }

    public void Deconstruct(out string clientId, out string clientSecret)
    {
        clientId = ClientId;
        clientSecret = ClientSecret;
    }
}
