using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Entities;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Models;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Repositories.Abstractions;

public interface IApplicationRepository
{
    Task<Application?> GetApplication(int applicationId);
    Task<IList<Application>> GetApplications(int contestStageId);
    Application AddApplication(ApplicationInsertModel model);
}
