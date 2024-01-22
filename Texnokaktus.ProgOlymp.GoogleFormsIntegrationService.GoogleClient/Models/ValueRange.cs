using System.Text.Json.Serialization;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Models;

public record ValueRange
{
    [JsonPropertyName("range")]
    public string Range { get; set; }

    [JsonPropertyName("majorDimension")]
    public string MajorDimension { get; set; }

    [JsonPropertyName("values")]
    public string[][] Values { get; set; }
}
