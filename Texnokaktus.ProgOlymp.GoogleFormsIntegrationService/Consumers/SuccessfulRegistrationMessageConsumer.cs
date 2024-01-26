using MassTransit;
using Texnokaktus.ProgOlymp.Common.Contracts.Messages.GoogleSheets.Notifications;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Consumers;

public class SuccessfulRegistrationMessageConsumer(IApplicationService applicationService) : IConsumer<SuccessfulRegistrationMessage>
{
    public async Task Consume(ConsumeContext<SuccessfulRegistrationMessage> context) =>
        await applicationService.WriteApplicationStatusAsync(context.Message.ApplicationId,
                                                             new(context.Message.YandexIdLogin,
                                                                 "Registered"));
}
