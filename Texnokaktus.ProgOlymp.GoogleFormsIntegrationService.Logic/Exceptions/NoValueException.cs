namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Exceptions;

public class NoValueException(string propertyName) : Exception($"No value for {propertyName}");
