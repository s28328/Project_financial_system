using Microsoft.EntityFrameworkCore;
using Project_financial_system.Context;
using Project_financial_system.Models.Domain;
using Project_financial_system.Repositories.Abstraction;

namespace Project_financial_system.Repositories;

public class AppUserRepository:BaseRepository,IAppUserRepository
{
    public AppUserRepository(FinancialContext context) : base(context)
    {
    }

    public async Task<int> AddUser(AppUser user, CancellationToken cancellationToken)
    {
        _context.AppUsers.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
        return 1;
    }

    public async Task<AppUser> GetUserByLogin(string login, CancellationToken cancellationToken)
    {
        var user = await _context.AppUsers.FirstOrDefaultAsync(u => u.Login == login, cancellationToken: cancellationToken);
        return user;
    }

    public async Task<AppUser> GetUserByRefreshToken(string refreshToken, CancellationToken cancellationToken)
    {
        var user = await _context.AppUsers.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken, cancellationToken: cancellationToken);
        return user;
    }
}