using MassTransit;
using Texnokaktus.ProgOlymp.Common.Contracts.Messages.GoogleSheets.Notifications;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Consumers;

public class YandexIdLoginDuplicatedConsumer(IApplicationService applicationService) : IConsumer<YandexIdLoginDuplicated>
{
    public async Task Consume(ConsumeContext<YandexIdLoginDuplicated> context) =>
        await applicationService.WriteApplicationStatusAsync(context.Message.ApplicationId,
                                                             new(context.Message.YandexIdLogin,
                                                                 "Login conflict"));
}
