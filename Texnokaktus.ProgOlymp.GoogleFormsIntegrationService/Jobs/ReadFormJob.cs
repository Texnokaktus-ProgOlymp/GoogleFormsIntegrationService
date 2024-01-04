using Microsoft.Extensions.Options;
using Quartz;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic.Services.Abstractions;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Models.Configuration;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Jobs;

public class ReadFormJob(IFormsService formsService, IOptions<FormSettings> options, ILogger<ReadFormJob> logger) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var applications = await formsService.GetParticipantApplicationsAsync(options.Value.FormId);
        logger.LogInformation("Got applications: {@Applications}", applications);
    }
}
