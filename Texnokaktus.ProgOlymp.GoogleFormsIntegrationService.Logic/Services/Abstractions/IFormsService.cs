using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Models;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services.Abstractions;

public interface IFormsService
{
    Task<IEnumerable<ParticipantApplication>> GetParticipantApplicationsAsync(string formId);
}
