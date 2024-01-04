using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Models;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services.Abstractions;

public interface IMessageService
{
    Task SendParticipantApplicationAsync(ParticipantApplication application);
}
