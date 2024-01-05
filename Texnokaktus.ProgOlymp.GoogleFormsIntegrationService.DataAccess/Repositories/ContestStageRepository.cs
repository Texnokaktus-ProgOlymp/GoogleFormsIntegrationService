using Microsoft.EntityFrameworkCore;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Context;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Entities;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Repositories.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Repositories;

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

    public async Task<IList<ContestStage>> GetActiveWithFormIdsAsync() =>
        await context.ContestStages
                     .AsNoTracking()
                     .Where(stage => stage.FormId != null && stage.IsActive)
                     .ToListAsync();

    public void Add(int id, bool isActive) => context.ContestStages.Add(new() { Id = id, IsActive = isActive });

    public void Add(ContestStage contestStage) => context.ContestStages.Add(contestStage);

    public async Task SetFormIdAsync(int id, string formId)
    {
        var stage = await context.ContestStages.FirstOrDefaultAsync(stage => stage.Id == id) ?? throw new("Not Found"); // TODO Typed exception
        stage.FormId = formId;
        context.ContestStages.Update(stage);
    }

    public async Task SetActiveAsync(int id, bool isActive)
    {
        var stage = await context.ContestStages.FirstOrDefaultAsync(stage => stage.Id == id) ?? throw new("Not Found"); // TODO Typed exception
        stage.IsActive = isActive;
        context.ContestStages.Update(stage);
    }
}
