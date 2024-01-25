using MassTransit;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Models;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services;

internal class MessageService(IPublishEndpoint bus) : IMessageService
{
    public async Task SendParticipantApplicationAsync(ParticipantApplication application)
    {
        var message = new Texnokaktus.ProgOlymp.Common.Contracts.Messages.GoogleForms.ParticipantApplication
        {
            ContestStageId = application.ContestStageId,
            SubmittedTime = application.CreateTime,
            AgeCategory = application.AgeCategory,
            ParticipantName = application.ParticipantName,
            BirthDate = application.BirthDate,
            ParticipantSnils = application.ParticipantSnils,
            ParticipantGrade = application.ParticipantGrade,
            ParticipantEmail = application.ParticipantEmail,
            ParticipantEmailConfirm = application.ParticipantEmailConfirm,
            School = application.School,
            SchoolRegion = application.SchoolRegion,
            ParentName = application.ParentName,
            ParentEmail = application.ParentEmail,
            ParentPhone = application.ParentPhone,
            PersonalDataConsent = application.PersonalDataConsent,
            TeacherName = application.TeacherName,
            TeacherSchool = application.TeacherSchool,
            TeacherEmail = application.TeacherEmail,
            TeacherPhone = application.TeacherPhone
        };
        await bus.Publish(message);
    }
}
