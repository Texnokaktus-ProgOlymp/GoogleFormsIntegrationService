using Microsoft.EntityFrameworkCore;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Context;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Entities;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Repositories.Abstractions;

internal class ContestStageRepository(AppDbContext context) : IContestStageRepository
{
    public async Task<ContestStage?> GetAsync(int id) =>
        await context.ContestStages
                     .AsNoTracking()
                     .FirstOrDefaultAsync(stage => stage.Id == id);

    public async Task<IList<ContestStage>> GetAllAsync() =>
        await context.ContestStages
                     .AsNoTracking()
                     .ToListAsync();

    public async Task<IList<ContestStage>> GetWithFormIdsAsync() =>
        await context.ContestStages
                     .AsNoTracking()
                     .Where(stage => stage.FormId != null)
                     .ToListAsync();

    public void Add(int id) => context.ContestStages.Add(new() { Id = id });

    public void Add(ContestStage contestStage) => context.ContestStages.Add(contestStage);

    public async Task SetFormIdAsync(int id, string formId)
    {
        var stage = await context.ContestStages.FirstOrDefaultAsync(stage => stage.Id == id) ?? throw new("Not Found"); // TODO Typed exception
        stage.FormId = formId;
        context.ContestStages.Update(stage);
    }
}
