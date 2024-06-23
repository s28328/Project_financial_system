using Version = Project_financial_system.Models.Domain.Version;

namespace Project_financial_system.Repositories.Abstraction;

public interface IVersionRepository:IBaseRepository
{
    Task<Version?> GetVersionWithSoftwareByIdAsync(int contractIdVersion, CancellationToken cancellationToken);
}