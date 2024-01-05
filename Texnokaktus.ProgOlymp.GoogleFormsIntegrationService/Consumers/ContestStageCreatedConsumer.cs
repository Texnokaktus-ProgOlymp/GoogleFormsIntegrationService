using MassTransit;
using Texnokaktus.ProgOlymp.Common.Contracts.Messages;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Consumers;

public class ContestStageCreatedConsumer(IContestStageService contestStageService) : IConsumer<ContestStageCreated>
{
    public async Task Consume(ConsumeContext<ContestStageCreated> context) =>
        await contestStageService.CreateContestStage(context.Message.ContestStageId);
}
