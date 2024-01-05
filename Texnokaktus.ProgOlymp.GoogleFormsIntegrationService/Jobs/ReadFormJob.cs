using Quartz;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Jobs;

public class ReadFormJob(IApplicationService applicationService,
                         IContestStageService contestStageService) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        foreach (var stageModel in await contestStageService.GetAvailableContestStagesAsync())
            await applicationService.ProcessNewApplicationsAsync(stageModel);
    }
}
