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
                     .Where(stage => stage.SheetId != null && stage.IsActive)
                     .ToListAsync();

    public async Task<int?> GetLastRowIndex(int id) =>
        await context.ContestStages
                     .Where(stage => stage.Id == id)
                     .Select(stage => stage.LastRowIndex)
                     .FirstOrDefaultAsync();

    public async Task SetLastRowIndex(int id, int lastRowIndex) =>
        await UpdateAsync(id, stage => stage.LastRowIndex = lastRowIndex);

    public void Add(int id, bool isActive) => context.ContestStages.Add(new() { Id = id, IsActive = isActive });

    public void Add(ContestStage contestStage) => context.ContestStages.Add(contestStage);

    public async Task SetSheetIdAsync(int id, string sheetId)
    {
        var stage = await context.ContestStages.FirstOrDefaultAsync(stage => stage.Id == id) ?? throw new("Not Found"); // TODO Typed exception
        stage.SheetId = sheetId;
        context.ContestStages.Update(stage);
    }

    public async Task SetActiveAsync(int id, bool isActive) =>
        await UpdateAsync(id, stage => stage.IsActive = isActive);

    private async Task UpdateAsync(int id, Action<ContestStage> updateAction)
    {
        var stage = await context.ContestStages.FirstOrDefaultAsync(stage => stage.Id == id) ?? throw new("Not Found"); // TODO Typed exception
        updateAction.Invoke(stage);
        context.ContestStages.Update(stage);
    }
}
