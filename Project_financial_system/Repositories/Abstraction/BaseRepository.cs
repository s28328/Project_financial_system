using Microsoft.EntityFrameworkCore.Storage;
using Project_financial_system.Context;

namespace Project_financial_system.Repositories.Abstraction;

public abstract class BaseRepository : IBaseRepository
{
    protected readonly FinancialContext _context;

    protected BaseRepository(FinancialContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}