using Microsoft.EntityFrameworkCore;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Context;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Entities;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Models;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Repositories.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Repositories;

internal class ApplicationRepository(AppDbContext context) : IApplicationRepository
{
    public async Task<IList<Application>> GetApplications(int contestStageId) =>
        await context.Applications
                     .AsNoTracking()
                     .Where(application => application.ContestStageId == contestStageId)
                     .ToListAsync();

    public void AddApplication(ApplicationInsertModel model)
    {
        var application = new Application
        {
            RowIndex = model.RowIndex,
            ContestStageId = model.ContestStageId,
            Submitted = model.Submitted,
            ParticipantEmail = model.ParticipantEmail
        };

        context.Applications.Add(application);
        context.Applications.AddRange();
    }
}

file static class QueryableExtensions
{
    public static async Task<HashSet<TSource>> ToHashSetAsync<TSource>(this IQueryable<TSource> source,
                                                                       CancellationToken cancellationToken = default)
    {
        var set = new HashSet<TSource>();
        await foreach (var element in source.AsAsyncEnumerable().WithCancellation(cancellationToken))
            set.Add(element);

        return set;
    }
}
