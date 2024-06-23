using Microsoft.EntityFrameworkCore;
using Project_financial_system.Context;
using Project_financial_system.Repositories.Abstraction;
using Version = Project_financial_system.Models.Domain.Version;

namespace Project_financial_system.Repositories;

public class VersionRepository:BaseRepository,IVersionRepository
{
    public VersionRepository(FinancialContext context) : base(context)
    {
    }

    public async Task<Version?> GetVersionWithSoftwareByIdAsync(int contractIdVersion,
        CancellationToken cancellationToken)
    {
        return await _context.Versions.Include(ver => ver.Software)
            .FirstOrDefaultAsync(version => version.IdVersion == contractIdVersion, cancellationToken: cancellationToken);
    }
}