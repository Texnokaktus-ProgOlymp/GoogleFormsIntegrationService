using MassTransit;
using Texnokaktus.ProgOlymp.Common.Contracts.Messages.GoogleSheets.Notifications;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Consumers;

public class InvalidEmailMessageConsumer(IApplicationService applicationService) : IConsumer<InvalidEmailMessage>
{
    public async Task Consume(ConsumeContext<InvalidEmailMessage> context) =>
        await applicationService.WriteApplicationStatusAsync(context.Message.ApplicationId,
                                                             new(null,
                                                                 "Invalid email"));
}
