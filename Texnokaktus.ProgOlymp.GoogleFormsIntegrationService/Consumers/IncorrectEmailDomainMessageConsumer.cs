using MassTransit;
using Texnokaktus.ProgOlymp.Common.Contracts.Messages.GoogleSheets.Notifications;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Consumers;

public class IncorrectEmailDomainMessageConsumer(IApplicationService applicationService) : IConsumer<IncorrectEmailDomainMessage>
{
    public async Task Consume(ConsumeContext<IncorrectEmailDomainMessage> context) =>
        await applicationService.WriteApplicationStatusAsync(context.Message.ApplicationId,
                                                             new(null,
                                                                 "Incorrect email domain"));
}
