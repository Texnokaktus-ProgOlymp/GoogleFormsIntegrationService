using MassTransit;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Models;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services;

internal class MessageService(IPublishEndpoint bus) : IMessageService
{
    public async Task SendParticipantApplicationAsync(int applicationId, ParticipantApplication application)
    {
        var message = new Texnokaktus.ProgOlymp.Common.Contracts.Messages.GoogleForms.ParticipantApplication
        {
            ApplicationId = applicationId,
            ContestStageId = application.ContestStageId,
            SubmittedTime = application.CreateTime,
            AgeCategory = application.AgeCategory,
            ParticipantName = application.ParticipantName,
            ParticipantGrade = application.ParticipantGrade,
            ParticipantEmail = application.ParticipantEmail,
            School = application.School,
            SchoolRegion = application.SchoolRegion,
        };
        await bus.Publish(message);
    }
}
