namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Exceptions;

public class GoogleAuthenticationException : Exception
{
    public GoogleAuthenticationException(string? message) : base(message)
    {
    }

    public GoogleAuthenticationException(string? message, Exception innerException) : base(message, innerException)
    {
    }
}
