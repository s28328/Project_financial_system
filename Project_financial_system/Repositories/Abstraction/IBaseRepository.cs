using Microsoft.EntityFrameworkCore.Storage;

namespace Project_financial_system.Repositories.Abstraction;

public interface IBaseRepository
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
}