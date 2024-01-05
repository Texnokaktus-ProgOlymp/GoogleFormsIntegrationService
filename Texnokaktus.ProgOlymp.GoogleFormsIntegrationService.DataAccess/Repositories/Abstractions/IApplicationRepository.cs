using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Entities;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Models;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Repositories.Abstractions;

public interface IApplicationRepository
{
    Task<ISet<string>> GetResponseIds(int contestStageId);
    Task<IList<Application>> GetApplications(int contestStageId);
    void AddApplication(ApplicationInsertModel model);
}